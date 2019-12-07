using AutoMapper;
using GitlabInfo.Code.Exceptions;
using GitlabInfo.Code.Helpers;
using GitlabInfo.Code.Repositories;
using GitlabInfo.Code.Repositories.Interfaces;
using GitlabInfo.Models;
using GitlabInfo.Models.EFModels;
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
    public class GroupController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public IGitLabInfoDbRepository DbRepository { get; private set; }
        public IGroupRepository GroupRepository { get; private set; }
        public IStandaloneRepository StandaloneRepository { get; private set; }
        public IProjectRepository ProjectRepository { get; private set; }

        public GroupController(ILogger<GroupController> logger, IMapper mapper, IGroupRepository groupRepository, IStandaloneRepository standaloneRepository, IProjectRepository projectRepository, IGitLabInfoDbRepository dbRepository)
        {
            _logger = logger;
            _mapper = mapper;
            GroupRepository = groupRepository;
            StandaloneRepository = standaloneRepository;
            ProjectRepository = projectRepository;
            DbRepository = dbRepository;
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
        public async Task<List<Group>> GetOwnedGroups(int? userId = null)
        {
            if (userId == null)
            {
                //Get groups for current user
                var gitlabUser = new User(User);
                userId = gitlabUser.Id;
            }

            var dbUser = DbRepository.GetUsers(user => user.Id == userId.Value, true).FirstOrDefault();
            if (dbUser == null)
                return new List<Group>();

            var groupIds = dbUser.OwnedGroups.Select(x => x.GroupId).ToList();
            var groupList = new List<Group>();

            foreach (var groupId in groupIds)
            {
                var gitlabGroup = await GroupRepository.GetGroupById(groupId);
                groupList.Add(gitlabGroup);
            }

            return groupList;
        }

        [HttpGet]
        public async Task<List<JoinRequest>> GetJoinRequestsForOwnedGroups(int? userId = null)
        {
            var ownedGroups = await GetOwnedGroups(userId);

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

            return requests;
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


                    return Unauthorized();

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
        public async Task<List<Issue>> GetIssuesFromGroup(int groupId)
        {
            if (PermissionHelper.IsUserGroupOwner(User, groupId, DbRepository))
            {
                return (await GroupRepository.GetIssuesGroupedByProject(groupId)).ToList();
            }

            return new List<Issue>();

        }

        [HttpGet]
        public async Task<List<Project>> GetProjectsFromGroupAsync(int groupId)
        {
            if (PermissionHelper.IsUserGroupOwner(User, groupId, DbRepository))
            {
                return (await GroupRepository.GetProjects(groupId)).ToList();
            }

            return new List<Project>();
        }

        [HttpGet]
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
    }
}