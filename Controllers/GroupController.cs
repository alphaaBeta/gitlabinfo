using System.Collections.Generic;
using System.Linq;
using GitlabInfo.Code.Exceptions;
using GitlabInfo.Code.Repositories;
using GitlabInfo.Code.Repositories.Interfaces;
using GitlabInfo.Models;
using GitlabInfo.Models.EFModels;
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
        public IGitLabInfoDbRepository DbRepository { get; private set; }
        public IGroupRepository GroupRepository { get; private set; }
        public IStandaloneRepository StandaloneRepository { get; private set; }

        public GroupController(ILogger<GroupController> logger, IGroupRepository groupRepository, IStandaloneRepository standaloneRepository, IGitLabInfoDbRepository dbRepository)
        {
            _logger = logger;
            GroupRepository = groupRepository;
            StandaloneRepository = standaloneRepository;
            DbRepository = dbRepository;
        }

        [HttpPut]
        public ActionResult RequestToJoinGroup(int groupId)
        {
            var gitlabUser = new User(User);
            var gitlabGroup = GroupRepository.GetGroupById(groupId, true);

            if (gitlabGroup.Members.Any(u => u.Id == gitlabUser.Id))
                return Conflict("You are already in that group");

            var group = DbRepository.GetGroup(groupId);
            var dbUser = DbRepository.GetUsers(user => user.Id == gitlabUser.Id).FirstOrDefault();

            //user should be added to DB if he had logged at least once
            if (dbUser == null)
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
            var gitlabOwnerUser = new User(User);

            var dbGroup = DbRepository.GetGroup(groupId);
            if (dbGroup == null)
                return NotFound("Group has not been added to database");

            var dbUser = DbRepository.GetUsers(user => user.Id == gitlabOwnerUser.Id, true).First();

            if (dbUser.OwnedGroups.Any(g => g.Group == dbGroup && g.Role >= Role.Maintainer))
            {
                GroupRepository.AddUserToGroup(groupId, userId);
                DbRepository.RemoveRange<JoinRequestModel>(jr =>
                    jr.RequestedGroup.Id == groupId && jr.Requestee.Id == userId);
            }
            else
                return Unauthorized();

            return Ok();
        }

        //TODO: Tests
        [HttpDelete]        
        public ActionResult RemoveUserJoinRequest(int groupId, int userId)
        {
            var gitlabOwnerUser = new User(User);

            var dbGroup = DbRepository.GetGroup(groupId);
            if (dbGroup == null)
                return NotFound("Group has not been added to database");

            var dbUser = DbRepository.GetUsers(user => user.Id == gitlabOwnerUser.Id, true).First();

            if (dbUser.OwnedGroups.Any(g => g.Group == dbGroup && g.Role >= Role.Maintainer))
            {
                DbRepository.RemoveRange<JoinRequestModel>(jr =>
                    jr.RequestedGroup.Id == groupId && jr.Requestee.Id == userId);
            }
            else
                return Unauthorized();

            return Ok();
        }

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
                        dbGroup = new GroupModel(gitGroup.Id)
                        {
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

        [HttpGet]
        public ActionResult GetIssuesFromGroup(int groupId)
        {

        }
    }
}