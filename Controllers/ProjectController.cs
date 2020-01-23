using AutoMapper;
using GitlabInfo.Code.Helpers;
using GitlabInfo.Code.Repositories;
using GitlabInfo.Code.Repositories.Interfaces;
using GitlabInfo.Models;
using GitlabInfo.Models.EFModels;
using GitlabInfo.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public IGitLabInfoDbRepository DbRepository { get; private set; }
        public IGroupRepository GroupRepository { get; private set; }
        public IProjectRepository ProjectRepository { get; private set; }
        public IStandaloneRepository StandaloneRepository { get; private set; }

        public ProjectController(ILogger<ProjectController> logger, IMapper mapper, IGroupRepository groupRepository, IProjectRepository projectRepository, IStandaloneRepository standaloneRepository, IGitLabInfoDbRepository dbRepository)
        {
            _logger = logger;
            _mapper = mapper;
            GroupRepository = groupRepository;
            ProjectRepository = projectRepository;
            StandaloneRepository = standaloneRepository;
            DbRepository = dbRepository;
        }

        [HttpPost]
        public async Task<ActionResult> RequestProjectCreationAsync([FromBody] ProjectRequestPutDto projectRequest)
        {
            var gitlabUser = new User(User);
            var dbUser = DbRepository.GetUsers(user => user.Id == gitlabUser.Id, true).First();
            var isUserOwner = !(dbUser.UserGroups.FirstOrDefault(g => g.GroupId == projectRequest.ParentGroupId && g.Role == Role.Owner) is null);
            var parentGroup = DbRepository.GetGroup(projectRequest.ParentGroupId);

            var projectRequestModel = new ProjectRequestModel()
            {
                ProjectName = projectRequest.Name,
                ProjectDescription = projectRequest.Description,
                ParentGroup = parentGroup
            };
            DbRepository.Add(projectRequestModel);

            var memberList = new List<UserProjectRequestModel>();
            foreach (var email in projectRequest.MemberEmails)
            {
                var gitlabMember = await StandaloneRepository.GetUserByEmail(email);
                var dbMember = DbRepository.GetUsers(user => user.Email == email || user.Id == gitlabMember?.Id).FirstOrDefault();

                if (dbMember is null)
                {
                    if (gitlabMember is null)
                        continue;
                    else
                    {
                        dbMember = new UserModel(gitlabMember.Id, email, gitlabMember.Name);
                        DbRepository.Add(dbMember);
                    }
                }

                memberList.Add(new UserProjectRequestModel()
                {
                    User = dbMember,
                    UserId = dbMember.Id,
                    IsRequestee = false,
                    ProjectRequest = projectRequestModel,
                    ProjectRequestId = projectRequestModel.Id
                });
            }

            //No need to add owner to members
            if (!isUserOwner)
                memberList.Add(new UserProjectRequestModel()
                {
                    User = dbUser,
                    UserId = dbUser.Id,
                    IsRequestee = true,
                    ProjectRequest = projectRequestModel,
                    ProjectRequestId = projectRequestModel.Id
                });

            DbRepository.AddRange(memberList);

            return Ok();
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        public async Task<ActionResult<IEnumerable<ProjectRequestGetDto>>> GetProjectCreationRequestsAsync()
        {
            var gitlabUser = new User(User);
            var dbUser = DbRepository.GetUsers(user => user.Id == gitlabUser.Id, true).First();

            var ownedGroupIds = DbRepository.Get<UserGroupModel>(ug => ug.UserId == dbUser.Id && ug.Role == Role.Owner, ug => ug.Group)
                .Select(ug => ug.Group.Id)
                .Distinct()
                .ToList();

            var dbProjectRequests = DbRepository.Get<ProjectRequestModel>(prm => ownedGroupIds.Contains(prm.ParentGroup.Id), prm => prm.Members).ToList();
            var projectRequestList = new List<ProjectRequestGetDto>();

            foreach (var pr in dbProjectRequests)
            {
                //var gitLabUserTasks = pr.Members.Select(x => StandaloneRepository.GetUserById(x.UserId)).ToList();

                //await Task.WhenAll(gitLabUserTasks);

                //var gitLabUsers = gitLabUserTasks.Select(x => x.Result);
                //var userDtos = gitLabUsers.Select(x => new UserDto()
                //{
                //    Name = x.Name,
                //    Email = x.Email,
                //    Id = x.Id
                //});
                var prMembers = DbRepository.Get<UserProjectRequestModel>(um => um.ProjectRequestId == pr.Id, um => um.User)
                    .Select(um => um.User);

                var userNames = prMembers.Select(m => new UserDto
                {
                    Email = m.Email,
                    Id = m.Id,
                    Name = m.Name,
                }).ToList();

                projectRequestList.Add(new ProjectRequestGetDto
                {
                    Id = pr.Id,
                    Name = pr.ProjectName,
                    Description = pr.ProjectDescription,
                    ParentGroupId = pr.ParentGroupId,
                    Members = userNames
                });
            }

            return projectRequestList;
        }


        [HttpPut]
        public async Task<ActionResult> ApproveProjectCreationRequest(int requestId)
        {
            var request = DbRepository.GetProjectRequests(pr => pr.Id == requestId).FirstOrDefault();
            if (request is null)
                return NotFound();


            var gitlabUser = new User(User);
            var dbUser = DbRepository.GetUsers(user => user.Id == gitlabUser.Id, true).First();

            //If user approving doesn't own the group, return unauthorized
            if (dbUser.UserGroups.All(g => g.GroupId != request.ParentGroup.Id))
                return Unauthorized();

            //Else create project
            Project project;
            try
            {
                project = await ProjectRepository.CreateProjectFromRequest(request);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while creating a project!");
                return StatusCode(500);
            }

            //if project was created successfully, remove the request
            var projectRequests = DbRepository.GetProjectRequests(pr => pr.Id == requestId);
            DbRepository.RemoveRange(projectRequests);

            //And add project to DB
            DbRepository.Add<ProjectModel>(new ProjectModel()
            {
                Id = project.Id,
                AssignedGroup = DbRepository.Get<GroupModel>(g => g.Id == request.ParentGroup.Id).FirstOrDefault(),
                Name = project.Name
            });

            return Ok();
        }

        [HttpDelete]
        public ActionResult RejectProjectCreationRequest(int requestId)
        {
            var request = DbRepository.GetProjectRequests(pr => pr.Id == requestId).FirstOrDefault();
            if (request is null)
                return NotFound();

            var gitlabUser = new User(User);
            var dbUser = DbRepository.GetUsers(user => user.Id == gitlabUser.Id, true).First();

            //If user approving doesn't own the group, return unauthorized
            if (dbUser.UserGroups.All(g => g.GroupId != request.ParentGroup.Id))
                return Unauthorized();

            //else remove the request
            var projectRequests = DbRepository.GetProjectRequests(pr => pr.Id == requestId);
            DbRepository.RemoveRange(projectRequests);

            return Ok();
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        public async Task<IEnumerable<ProjectGetDto>> GetProjects(int groupId)
        {
            var gitlabUser = new User(User);
            var groupProjects = await GroupRepository.GetProjects(groupId);
            var projects = (await GroupRepository.GetProjects(groupId)).Select(async project =>
            {
                var members = await ProjectRepository.GetMembers(project.Id);
                return new
                {
                    Project = project,
                    Members = members
                };
            });


            var isOwner = PermissionHelper.IsUserGroupOwner(User, groupId, DbRepository);
            var projectsToReturn = new List<ProjectGetDto>();
            foreach (var project in projects)
            {
                var result = await project;
                if (isOwner || result.Members.Any(member => member.Id == gitlabUser.Id && member.AccessLevel >= (int)Role.Developer))
                {
                    projectsToReturn.Add(new ProjectGetDto()
                    {
                        Name = result.Project.Name,
                        CreatedAt = result.Project.CreatedAt,
                        Description = result.Project.Description,
                        Id = result.Project.Id,
                        LastActivityAt = result.Project.LastActivityAt,
                        PathWithNamespace = result.Project.PathWithNamespace,
                        Members = result.Members.Select(member => new UserDto()
                        {
                            Id = member.Id,
                            Name = member.Name,
                            Email = member.Email
                        })
                    });
                }
            }
            return projectsToReturn;
        }

        [HttpPost]
        public async Task<ActionResult> ReportHoursAsync(ReportedTimeDto reportedTimeDto)
        {
            var gitlabUser = new User(User);

            //Check if user is in the project
            var gitLabProject = await ProjectRepository.GetMembers(reportedTimeDto.ProjectId, true);

            if (gitLabProject.FirstOrDefault(u => u.Id == gitlabUser.Id) == null)
                return new NotFoundResult();

            //Report time
            var dbProject = DbRepository.Get<ProjectModel>(p => p.Id == reportedTimeDto.ProjectId).FirstOrDefault();
            var dbUser = DbRepository.Get<UserModel>(u => u.Id == gitlabUser.Id).FirstOrDefault();

            var reportedTimeModel = new ReportedTimeModel()
            {
                Project = dbProject,
                User = dbUser,
                Date = reportedTimeDto.Date,
                TimeInHours = reportedTimeDto.TimeInHours,
                Description = reportedTimeDto.Description,
                IssueId = reportedTimeDto.IssueId,
                ReportedDate = DateTime.UtcNow
            };

            DbRepository.Add(reportedTimeModel);
            return new OkResult();
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        public async Task<List<ReportedTimeDto>> GetReportedHoursInProjectAsync(int projectId)
        {
            var members = (await ProjectRepository.GetMembers(projectId)).ToList();

            var reportedTimes = DbRepository
                .Get<ReportedTimeModel>(rtm => members.Select(m => m.Id).Contains(rtm.User.Id),
                    rtm => rtm.User)
                .Select(rtm => new ReportedTime()
                {
                    User = members.FirstOrDefault(m => m.Id == rtm.User.Id),
                    Date = rtm.Date,
                    TimeInHours = rtm.TimeInHours,
                    IssueId = rtm.IssueId
                });

            return reportedTimes.Select(rt => _mapper.Map<ReportedTimeDto>(rt)).ToList();
        }

        [HttpPut]
        public async Task<ActionResult> GiveEngagementPointsAsync(EngagementPointsPutDto engagementPointsDto)
        {
            var gitlabUser = new User(User);

            //Check if user is in the project
            var projectMembers = (await ProjectRepository.GetMembers(engagementPointsDto.ProjectId)).ToList();

            var receivingUser = projectMembers.FirstOrDefault(u => u.Id == engagementPointsDto.ReceivingUser.Id);
            var awardingUser = projectMembers.FirstOrDefault(u => u.Id == gitlabUser.Id);

            if (receivingUser is null || awardingUser is null)
                return NotFound();

            //Report time
            var dbProject = DbRepository.Get<ProjectModel>(p => p.Id == engagementPointsDto.ProjectId).FirstOrDefault();
            var dbReceivingUser = DbRepository.Get<UserModel>(u => u.Id == receivingUser.Id).FirstOrDefault();
            var dbAwardingUser = DbRepository.Get<UserModel>(u => u.Id == awardingUser.Id).FirstOrDefault();

            var engagementPointsModel = new EngagementPointsModel()
            {
                Project = dbProject,
                ReceivingUser = dbReceivingUser,
                AwardingUser = dbAwardingUser,
                Points = engagementPointsDto.Points,
                ReceivingDate = DateTime.UtcNow,
                Bonus = engagementPointsDto.Bonus,
                Comment = engagementPointsDto.Comment
            };

            DbRepository.Add(engagementPointsModel);
            return Ok();
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        public async Task<List<EngagementPointsGetDto>> GetEngagementPointsInProjectAsync(int projectId)
        {
            var gitlabUser = new User(User);
            var members = (await ProjectRepository.GetMembers(projectId)).ToList();
            var userMember = members.FirstOrDefault(m => m.Id == gitlabUser.Id);

            if (userMember != null && RoleHelpers.GetRoleByValue(userMember.AccessLevel) != Role.Owner)
            {
                return new List<EngagementPointsGetDto>();
            }

            var engagementPoints = DbRepository
                .Get<EngagementPointsModel>(ep => ep.Project.Id == projectId,
                    ep => ep.Project,
                    ep => ep.AwardingUser,
                    ep => ep.ReceivingUser)
                .Select(ep => new EngagementPoints()
                {
                    ReceivingUser = members.FirstOrDefault(m => m.Id == ep.ReceivingUser.Id),
                    AwardingUser = members.FirstOrDefault(m => m.Id == ep.AwardingUser.Id),
                    Points = ep.Points,
                    ReceivingDate = ep.ReceivingDate
                });

            return engagementPoints.Select(ep => _mapper.Map<EngagementPointsGetDto>(ep)).ToList();
        }

        [HttpPost]
        public async Task<ActionResult> PostWorkDescriptionAsync([FromBody] WorkDescriptionPostDto workDescription)
        {
            var gitlabUser = new User(User);

            var project = DbRepository.Get<ProjectModel>(p => p.Id == workDescription.ProjectId).FirstOrDefault();
            if (project is null)
                return new NotFoundResult();

            //Check if user is in the project
            var gitLabProject = await ProjectRepository.GetMembers(workDescription.ProjectId, true);

            if (gitLabProject.FirstOrDefault(u => u.Id == gitlabUser.Id) == null)
                return NotFound();

            DbRepository.Add<WorkDescriptionModel>(new WorkDescriptionModel
            {
                UserId = gitlabUser.Id,
                Project = project,
                Description = workDescription.Description,
                Date = DateTime.UtcNow
            });

            return new OkResult();
        }

        /// <summary>
        /// Gets work descriptions of other users in the project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="getComments"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        public async Task<ActionResult<List<WorkDescriptionGetDto>>> GetWorkDescriptionsAsync(int projectId)
        {
            var gitlabUser = new User(User);

            var project = DbRepository.Get<ProjectModel>(p => p.Id == projectId, p => p.AssignedGroup).FirstOrDefault();
            if (project is null)
                return new NotFoundResult();

            if (!(await PermissionHelper.IsUserProjectMember(User, project.Id, ProjectRepository)))
                return new UnauthorizedResult();

            var canGetComments = PermissionHelper.IsUserGroupOwner(User, project.AssignedGroup.Id, DbRepository);

            var workDescriptions = DbRepository.Get<WorkDescriptionModel>(wd => wd.ProjectId == projectId, wd => wd.User, wd => wd.Comments)
                .Where(wd => wd.UserId != gitlabUser.Id)
                .Select(wd => new WorkDescriptionGetDto()
                {
                    Id = wd.WorkDescriptionId,
                    User = new UserDto
                    {
                        Id = wd.User.Id,
                        Email = wd.User.Email,
                        Name = wd.User.Name
                    },
                    Description = wd.Description,
                    Comments = canGetComments ? wd.Comments.Select(c => c.Comment).ToList() : new List<string>(),
                    Date = wd.Date
                })
                .ToList();

            return new OkObjectResult(workDescriptions);
        }

        [HttpPut]
        public async Task<ActionResult> PutWorkDescriptionCommentAsync(int workDescriptionId, string comment)
        {
            var gitlabUser = new User(User);

            var workDescription = DbRepository.Get<WorkDescriptionModel>(wd => wd.WorkDescriptionId == workDescriptionId).FirstOrDefault();

            if (workDescription is null)
                return new NotFoundResult();

            if (!(await PermissionHelper.IsUserProjectMember(User, workDescription.ProjectId, ProjectRepository)))
            {
                return new UnauthorizedResult();
            }

            DbRepository.Add<WorkDescriptionCommentModel>(new WorkDescriptionCommentModel()
            {
                WorkDescription = workDescription,
                Comment = comment,
                CommenterId = gitlabUser.Id
            });

            return new OkResult();
        }

    }
}