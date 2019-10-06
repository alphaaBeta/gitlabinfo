using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult RequestToJoinGroup(int groupId)
        {
            var gitlabUser = new User(User);
            //Check if user alredy has access and if he isn't in the group
            try
            {
                var gitlabGroup = GroupRepository.GetGroupById(groupId, true);

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
        public List<Group> GetOwnedGroups(int? userId = null)
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
                groupList.Add(GroupRepository.GetGroupById(groupId));
            }

            return groupList;
        }

        [HttpGet]
        public List<JoinRequest> GetJoinRequestsForOwnedGroups(int? userId = null)
        {
            var ownedGroups = GetOwnedGroups(userId);

            var requests = new List<JoinRequest>();

            foreach (var ownedGroup in ownedGroups)
            {
                var requestsForGroup = DbRepository.GetJoinRequestForGroup(ownedGroup.Id);

                requests.AddRange(requestsForGroup
                    .Select(joinRequestModel => new JoinRequest
                    {
                        Id = joinRequestModel.Id,
                        RequestedGroup = GroupRepository.GetGroupById(joinRequestModel.RequestedGroup.Id),
                        Requestee = StandaloneRepository.GetUserById(joinRequestModel.Requestee.Id)
                    }));
            }

            return requests;
        }

        //TODO: Tests
        [HttpPut]
        public ActionResult AddUserToGroup(int groupId, int userId)
        {
            try
            {
                if (PermissionHelper.IsUserGroupOwner(User, groupId, DbRepository))
                {
                    GroupRepository.AddUserToGroup(groupId, userId);

                    var joinRequests = DbRepository.GetJoinRequestForGroup(groupId).Where(jr => jr.Requestee.Id == userId);
                    DbRepository.RemoveRange(joinRequests);

                    return Ok();
                }
                else
                    return Unauthorized();
            }
            catch (ArgumentException ae)
            {
                return NotFound();
            }
        }

        //TODO: Tests
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
            catch (ArgumentException ae)
            {
                return NotFound();
            }
        }

        //TODO: Tests
        [HttpPut]
        public ActionResult AddCurrentUserAsGroupOwner(int groupId)
        {
            try
            {
                var gitGroup = GroupRepository.GetGroupById(groupId, true);
                var gitCurrentUser = new User(User);
                var accessLevel = gitGroup.Members.FirstOrDefault(u => u.Id == gitCurrentUser.Id)?.AccessLevel;

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

                    DbRepository.AddUserAsOwner(dbUser, dbGroup, RoleHelpers.GetRoleByValue(accessLevel.Value));

                    return Ok();
                }
            }
            catch (GroupInaccessibleException giex)
            {
                return Unauthorized();
            }

            return Unauthorized();
        }

        //TODO: Tests
        [HttpGet]
        public List<Issue> GetIssuesFromGroup(int groupId)
        {
            if (PermissionHelper.IsUserGroupOwner(User, groupId, DbRepository))
            {
                return GroupRepository.GetIssuesGroupedByProject(groupId).ToList();
            }

            return new List<Issue>();

        }
        
        [HttpGet]
        public List<Project> GetProjectsFromGroup(int groupId)
        {
            if (PermissionHelper.IsUserGroupOwner(User, groupId, DbRepository))
            {
                return GroupRepository.GetProjects(groupId).ToList();
            }

            return new List<Project>();
        }

        [HttpGet]
        public List<ReportedTime> GetReportedHoursInGroup(int groupId)
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
                reportedTimes.AddRange(fullProject.ReportedTimes.Select(rtm => new ReportedTime()
                {
                    User = StandaloneRepository.GetUserById(rtm.User.Id),
                    Date = rtm.Date,
                    TimeInHours = rtm.TimeInHours
                }));
            }

            return reportedTimes.ToList();
        }
    }
}