using AutoMapper;
using GitlabInfo.Code.Exceptions;
using GitlabInfo.Code.Helpers;
using GitlabInfo.Code.Repositories;
using GitlabInfo.Code.Repositories.Interfaces;
using GitlabInfo.Models;
using GitlabInfo.Models.EFModels;
using GitlabInfo.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GitlabInfo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public IGitLabInfoDbRepository DbRepository { get; private set; }
        public IGroupRepository GroupRepository { get; private set; }
        public IStandaloneRepository StandaloneRepository { get; private set; }
        public IProjectRepository ProjectRepository { get; private set; }
        public IExcelExportRepository ExcelExportRepository { get; private set; }

        public GroupController(ILogger<GroupController> logger, IMapper mapper, IGroupRepository groupRepository, IStandaloneRepository standaloneRepository, IProjectRepository projectRepository, IGitLabInfoDbRepository dbRepository, IExcelExportRepository excelExportRepository)
        {
            _logger = logger;
            _mapper = mapper;
            GroupRepository = groupRepository;
            StandaloneRepository = standaloneRepository;
            ProjectRepository = projectRepository;
            DbRepository = dbRepository;
            ExcelExportRepository = excelExportRepository;
        }

        [HttpPut]
        public async Task<ActionResult> RequestToJoinGroup(int groupId)
        {
            var gitlabUser = new User(User);
            //Check if user alredy has access and if he isn't in the group
            try
            {
                var gitlabGroup = await GroupRepository.GetGroupById(groupId, true);

                if (gitlabGroup.Members.Any(u => u.Id == gitlabUser.Id))
                    return Conflict("You are already in that group");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Something went wrong connecting to gitlab api");
            }

            var group = DbRepository.GetGroup(groupId);
            var dbUser = DbRepository.GetUsers(user => user.Id == gitlabUser.Id).FirstOrDefault();

            //user should be added to DB if he had logged at least once
            if (group is null || dbUser is null)
                return NotFound();

            var request = new JoinRequestModel()
            {
                RequestedGroup = group,
                Requestee = dbUser
            };

            DbRepository.Add(request);

            return Ok();
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        public async Task<ActionResult<List<GroupDto>>> GetGroups(int? userId = null, int role = 10)
        {
            var groupList = await GetGroupDtosAsync(userId, role);

            return new OkObjectResult(groupList);
        }

        [HttpGet]
        public async Task<ActionResult<List<JoinRequest>>> GetJoinRequestsForOwnedGroups(int? userId = null)
        {
            var ownedGroups = await GetGroupDtosAsync(userId, 50);

            var requests = new List<JoinRequest>();

            foreach (var ownedGroup in ownedGroups)
            {
                var requestsForGroup = DbRepository.GetJoinRequestForGroup(ownedGroup.Id);
                foreach (var requestForGroup in requestsForGroup)
                {
                    var joinRequest = new JoinRequest
                    {
                        Id = requestForGroup.Id,
                        RequestedGroup = await GroupRepository.GetGroupById(requestForGroup.RequestedGroup.Id),
                        Requestee = await StandaloneRepository.GetUserById(requestForGroup.Requestee.Id)
                    };


                    requests.Add(joinRequest);
                }
            }

            return new OkObjectResult(requests);
        }

        private async Task<List<GroupDto>> GetGroupDtosAsync(int? userId = null, int role = 10)
        {
            if (userId == null)
            {
                //Get groups for current user
                var gitlabUser = new User(User);
                userId = gitlabUser.Id;
            }

            var dbUser = DbRepository.GetUsers(user => user.Id == userId.Value, true).FirstOrDefault();
            if (dbUser == null)
                return new List<GroupDto>();

            var groups = dbUser.UserGroups.ToList();
            var groupList = new List<Group>();

            foreach (var group in groups)
            {
                if ((int)group.Role >= role)
                {
                    var gitlabGroup = await GroupRepository.GetGroupById(group.GroupId);
                    groupList.Add(gitlabGroup);
                }
            }

            return groupList.Select(g => new GroupDto()
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                ParentId = g.ParentId,
                Path = g.Path,
                WebUrl = g.WebUrl,
                IsOwner = PermissionHelper.IsUserGroupOwner(User, g.Id, DbRepository),
                Options = _mapper.Map<GroupOptions>(DbRepository.Get<GroupOptionsModel>(go => go.GroupId == g.Id).FirstOrDefault() ?? new GroupOptionsModel())
            }).ToList();
        }

        [HttpPut]
        public async Task<ActionResult> AddUserToGroup(int groupId, int userId)
        {
            try
            {
                if (PermissionHelper.IsUserGroupOwner(User, groupId, DbRepository))
                {
                    await GroupRepository.AddUserToGroup(groupId, userId);

                    var joinRequests = DbRepository.GetJoinRequestForGroup(groupId).Where(jr => jr.Requestee.Id == userId);
                    DbRepository.RemoveRange(joinRequests);

                    var dbUser = DbRepository.GetUsers(user => user.Id == userId, true).First();
                    var dbGroup = DbRepository.GetGroup(groupId, true);

                    if (dbUser == null)
                        return NotFound("User not found");

                    DbRepository.AddUserWithRole(dbUser, dbGroup, Role.Guest);


                    return new UnauthorizedResult();
                }
                else
                    return new UnauthorizedResult();
            }
            catch (ArgumentException)
            {
                return new NotFoundResult();
            }
        }

        [HttpDelete]
        public ActionResult RemoveUserJoinRequest(int groupId, int userId)
        {
            try
            {
                if (PermissionHelper.IsUserGroupOwner(User, groupId, DbRepository))
                {
                    var joinRequests = DbRepository.GetJoinRequestForGroup(groupId).Where(jr => jr.Requestee.Id == userId);
                    DbRepository.RemoveRange(joinRequests);

                    return Ok();
                }
                else
                    return Unauthorized();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpPut]
        public async Task<ActionResult> AddCurrentUserAsGroupOwner(int groupId)
        {
            try
            {
                var gitGroup = await GroupRepository.GetGroupById(groupId, true);
                var gitCurrentUser = new User(User);
                var accessLevel = gitGroup.Members?.FirstOrDefault(u => u.Id == gitCurrentUser.Id)?.AccessLevel;

                if (accessLevel != null && RoleHelpers.GetRoleByValue(accessLevel.Value) >= Role.Maintainer)
                {
                    var dbUser = DbRepository.GetUsers(user => user.Id == gitCurrentUser.Id, true).First();
                    var dbGroup = DbRepository.GetGroup(gitGroup.Id, true);

                    if (dbUser == null)
                        return NotFound("User not found");
                    if (dbGroup == null)
                    {
                        dbGroup = new GroupModel()
                        {
                            Id = gitGroup.Id,
                            AssignedUsers = new List<UserGroupModel>()
                        };
                        DbRepository.Add(dbGroup);
                    }

                    DbRepository.AddUserWithRole(dbUser, dbGroup, RoleHelpers.GetRoleByValue(accessLevel.Value));

                    return Ok();
                }
            }
            catch (GroupInaccessibleException)
            {
                return Unauthorized();
            }

            return Unauthorized();
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        public async Task<List<Issue>> GetIssuesFromGroup(int groupId)
        {
            if (PermissionHelper.IsUserGroupOwner(User, groupId, DbRepository))
            {
                return (await GroupRepository.GetIssuesGroupedByProject(groupId)).ToList();
            }

            return new List<Issue>();

        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        public async Task<List<Project>> GetProjectsFromGroupAsync(int groupId)
        {
            if (PermissionHelper.IsUserGroupOwner(User, groupId, DbRepository))
            {
                return (await GroupRepository.GetProjects(groupId)).ToList();
            }

            return new List<Project>();
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        public async Task<List<ReportedTime>> GetReportedHoursInGroupAsync(int groupId)
        {
            if (!PermissionHelper.IsUserGroupOwner(User, groupId, DbRepository))
                return new List<ReportedTime>();

            var dbGroup = DbRepository.Get<GroupModel>(g => g.Id == groupId, g => g.Projects).FirstOrDefault();
            if (dbGroup is null)
                return new List<ReportedTime>();
            var groupProjects = dbGroup.Projects.ToList();

            var reportedTimes = new List<ReportedTime>();
            foreach (var project in groupProjects)
            {
                var fullProject = DbRepository.GetProjectWithReportedTimes(project.Id);

                foreach (var rtm in fullProject.ReportedTimes)
                {
                    var reportedTime = new ReportedTime
                    {
                        User = await StandaloneRepository.GetUserById(rtm.User.Id),
                        Date = rtm.Date,
                        TimeInHours = rtm.TimeInHours
                    };

                    reportedTimes.Add(reportedTime);
                }
            }

            return reportedTimes.ToList();
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Default30")]
        public async Task<ActionResult<List<SurveyDto>>> GetAvailableSurveysAsync(int groupId)
        {
            var gitlabUser = new User(User);

            if (!PermissionHelper.IsUserGroupMember(User, groupId, DbRepository))
                return new List<SurveyDto>();

            var surveyModels = DbRepository.Get<SurveyModel>(s => s.GroupOptionsList.Any(go => go.GroupId == groupId)).ToList();

            var surveys = surveyModels.Select(s =>
            {
                var surveyObj = JsonConvert.DeserializeObject<SurveyObject>(s.SurveyString);
                return new SurveyDto()
                {
                    SurveyId = s.SurveyId,
                    Name = surveyObj.Name,
                    MultiselectQuestions = surveyObj.MultiselectQuestions,
                    TextQuestions = surveyObj.TextQuestions
                };
            }).ToList();

            return new OkObjectResult(surveys);
        }

        [HttpGet]
        public async Task<ActionResult<List<SurveyDto>>> GetSurveysForOwnedGroups()
        {
            var ownedGroupIds = (await GetGroupDtosAsync(role: (int)Role.Owner)).Select(g => g.Id).ToList();

            var surveyModels = DbRepository.Get<SurveyModel>(s => s.GroupOptionsList.Any(go => ownedGroupIds.Contains(go.GroupId))).ToList();

            var surveys = surveyModels.Select(s =>
            {
                var surveyObj = JsonConvert.DeserializeObject<SurveyObject>(s.SurveyString);
                return new SurveyDto()
                {
                    SurveyId = s.SurveyId,
                    Name = surveyObj.Name
                    //,
                    //MultiselectQuestions = surveyObj.MultiselectQuestions,
                    //TextQuestions = surveyObj.TextQuestions
                };
            }).ToList();

            return new OkObjectResult(surveys);
        }

        [HttpPost]
        public async Task<ActionResult> PostAnswerSurveyAsync([FromBody] SurveyAnswerDto surveyAnswer)
        {
            var gitlabUser = new User(User);
            var dbUser = DbRepository.GetUsers(u => u.Id == gitlabUser.Id).FirstOrDefault();

            var survey = DbRepository.Get<SurveyModel>(s => s.SurveyId == surveyAnswer.SurveyId).FirstOrDefault();
            if (survey is null)
                return new NotFoundResult();

            var project = DbRepository.Get<ProjectModel>(p => p.Id == surveyAnswer.ProjectId, p => p.AssignedGroup).FirstOrDefault();

            if (!PermissionHelper.IsUserGroupMember(User, project.AssignedGroup.Id, DbRepository))
                return new UnauthorizedResult();

            var answerObject = new AnswersObject()
            {
                MultiselectAnswers = surveyAnswer.MultiselectAnswers,
                TextAnswers = surveyAnswer.TextAnswers
            };


            DbRepository.Add<SurveyAnswerModel>(new SurveyAnswerModel()
            {
                ProjectId = surveyAnswer.ProjectId,
                SurveyId = surveyAnswer.SurveyId,
                AnswerString = JsonConvert.SerializeObject(answerObject),
                User = dbUser,
                AnswerDate = DateTime.UtcNow
            });
            return new OkResult();
        }

        [HttpPost]
        public async Task<ActionResult> PostGroupOptionsAsync([FromBody] GroupOptionsPostDto groupOptions)
        {
            var gitlabUser = new User(User);
            var dbUser = DbRepository.GetUsers(u => u.Id == gitlabUser.Id).FirstOrDefault();

            var group = DbRepository.GetGroup(groupOptions.GroupId);
            if (group is null)
                return new NotFoundResult();

            if (!PermissionHelper.IsUserGroupOwner(User, groupOptions.GroupId, DbRepository))
                return new UnauthorizedResult();

            if (!(groupOptions.SurveyString is null))
            {
                try
                {
                    var surveyObj = JsonConvert.DeserializeObject<SurveyObject>(groupOptions.SurveyString);
                }
                catch (Exception)
                {
                    return new BadRequestResult();
                }

                var surveyModel = new SurveyModel
                {
                    SurveyString = groupOptions.SurveyString
                };

                DbRepository.Add<SurveyModel>(surveyModel);

                groupOptions.SurveyId = surveyModel.SurveyId;
            }

            var existingGroupOptions = DbRepository.Get<GroupOptionsModel>(go => go.GroupId == groupOptions.GroupId, go => go.Survey).FirstOrDefault();
            if (existingGroupOptions is null)
            {
                existingGroupOptions = new GroupOptionsModel()
                {
                    GroupId = groupOptions.GroupId,
                    EngagementPointsEnabled = groupOptions.EngagementPointsEnabled,
                    ReportTimeEnabled = groupOptions.ReportTimeEnabled,
                    SurveyEnabled = groupOptions.SurveyEnabled,
                    WorkDescriptionCommentsEnabled = groupOptions.WorkDescriptionCommentsEnabled,
                    WorkDescriptionEnabled = groupOptions.WorkDescriptionEnabled,
                    AllowsProjectCreation = groupOptions.AllowsProjectCreation,
                    SurveyId = groupOptions.SurveyId
                };
                DbRepository.Add(existingGroupOptions);
            }
            else
            {
                existingGroupOptions.EngagementPointsEnabled = groupOptions.EngagementPointsEnabled;
                existingGroupOptions.ReportTimeEnabled = groupOptions.ReportTimeEnabled;
                existingGroupOptions.SurveyEnabled = groupOptions.SurveyEnabled;
                existingGroupOptions.WorkDescriptionCommentsEnabled = groupOptions.WorkDescriptionCommentsEnabled;
                existingGroupOptions.WorkDescriptionEnabled = groupOptions.WorkDescriptionEnabled;
                existingGroupOptions.AllowsProjectCreation = groupOptions.AllowsProjectCreation;
                if (groupOptions.SurveyId.HasValue)
                {
                    existingGroupOptions.SurveyId = groupOptions.SurveyId;
                }
                DbRepository.Update(existingGroupOptions);
            }

            return new OkResult();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDbInfo(int groupId)
        {
            var gitlabUser = new User(User);
            var dbUser = DbRepository.GetUsers(u => u.Id == gitlabUser.Id).FirstOrDefault();

            var dbGroup = DbRepository.GetGroup(groupId, true);
            if (dbGroup is null)
                return new NotFoundResult();

            if (!PermissionHelper.IsUserGroupOwner(User, groupId, DbRepository))
                return new UnauthorizedResult();

            var gitlabGroup = await GroupRepository.GetGroupById(groupId, true);

            //Add or update group members
            var members = gitlabGroup.Members.ToList();
            foreach (var member in members)
            {
                var dbMissingMember = DbRepository.GetUsers(u => u.Id == member.Id).FirstOrDefault();
                if (dbMissingMember is null)
                {
                    DbRepository.Add<UserModel>(new UserModel
                    {
                        Id = member.Id,
                        Email = member.Email,
                        Name = member.Name
                    });
                }


                var ugModel = DbRepository.Get<UserGroupModel>(ug => ug.GroupId == dbGroup.Id && ug.UserId == member.Id).FirstOrDefault();
                if (ugModel is null)
                {
                    DbRepository.Add<UserGroupModel>(new UserGroupModel
                    {
                        GroupId = dbGroup.Id,
                        UserId = member.Id,
                        Role = RoleHelpers.GetRoleByValue(member.AccessLevel)
                    });
                }
                else
                {
                    ugModel.Role = RoleHelpers.GetRoleByValue(member.AccessLevel);
                    DbRepository.Update(ugModel);
                }
            }

            //Add projects
            foreach (var project in gitlabGroup.Projects)
            {
                var dbProject = DbRepository.Get<ProjectModel>(p => p.Id == project.Id).FirstOrDefault();
                if (dbProject is null)
                {
                    DbRepository.Add(new ProjectModel
                    {
                        Id = project.Id,
                        AssignedGroup = dbGroup,
                        Name = project.Name
                    });
                }
            }

            return new OkResult();
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ExportToExcel(int groupId)
        {
            //TODO: add chekcing if user is group owner

            var dbGroup = DbRepository.GetGroup(groupId, true);
            var dbProjects = dbGroup.Projects.ToList();
            var projectIds = dbProjects.Select(p => p.Id);

            var reportedTimes = _mapper.Map<IEnumerable<ReportedTimeModel>, List<Models.ExcelExport.ReportedTime>>(DbRepository.Get<ReportedTimeModel>(rt => projectIds.Contains(rt.Project.Id), rt => rt.Project, rt => rt.User));
            var engagementPoints = _mapper.Map<IEnumerable<EngagementPointsModel>, List<Models.ExcelExport.EngagementPoints>>(DbRepository.Get<EngagementPointsModel>(ep => projectIds.Contains(ep.Project.Id), ep => ep.Project, ep => ep.AwardingUser, ep => ep.ReceivingUser));
            var workDescriptions = _mapper.Map<IEnumerable<WorkDescriptionModel>, List<Models.ExcelExport.WorkDescription>>(DbRepository.Get<WorkDescriptionModel>(wd => projectIds.Contains(wd.Project.Id), wd => wd.Project, wd => wd.User));

            var surveyAnswers = DbRepository.Get<SurveyAnswerModel>(s => projectIds.Contains(s.ProjectId), s => s.User, s => s.Project).ToList();
            //There can be only one survey
            var surveyId = surveyAnswers?.FirstOrDefault()?.SurveyId ?? 0;
            var survey = DbRepository.Get<SurveyModel>(s => s.SurveyId == surveyId).FirstOrDefault();

            var surveyList = surveyAnswers.Select(s => new Models.ExcelExport.Survey()
            {
                AnswerDate = s.AnswerDate,
                Answers = JsonConvert.DeserializeObject<AnswersObject>(s.AnswerString),
                ProjectName = s.Project.Name,
                Questions = JsonConvert.DeserializeObject<SurveyObject>(survey.SurveyString),
                UserName = s.User.Name
            }).ToList();

            var stream = ExcelExportRepository.ExportGroupInfo(reportedTimes, engagementPoints, workDescriptions, surveyList);
            string excelName = $"Test-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}