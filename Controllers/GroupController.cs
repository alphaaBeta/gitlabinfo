using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Code.EntiyFramework;
using GitlabInfo.Code.Exceptions;
using GitlabInfo.Code.Extensions;
using GitlabInfo.Code.Repositories;
using GitlabInfo.Code.Repositories.Interfaces;
using GitlabInfo.Models;
using GitlabInfo.Models.EFModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GitlabInfo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IGitLabInfoDbRepository _dbRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IStandaloneRepository _standaloneRepository;

        public GroupController(ILogger<GroupController> logger, IGroupRepository groupRepository, IStandaloneRepository standaloneRepository, IGitLabInfoDbRepository dbRepository)
        {
            _logger = logger;
            _groupRepository = groupRepository;
            _standaloneRepository = standaloneRepository;
            _dbRepository = dbRepository;
        }

        [HttpPut]
        public ActionResult RequestToJoinGroup(int groupId)
        {
            var gitlabUser = new User(User);
            var gitlabGroup = _groupRepository.GetGroupById(groupId, true);

            if (gitlabGroup.Members.Any(u => u.Id == gitlabUser.Id))
                return Conflict("You are already in that group");

            var group = _dbRepository.GetGroup(groupId);
            var dbUser = _dbRepository.GetUser(gitlabUser.Id);//_dbContext.Users.Find(gitlabUser.Id);

            //user should be added to DB if he had logged at least once
            if (dbUser == null)
                return NotFound();

            var request = new JoinRequestModel()
            {
                RequestedGroup = group,
                Requestee = dbUser
            };
            
            _dbRepository.Add(request);

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

            var dbUser = _dbRepository.GetUser(userId.Value, true);
            if (dbUser == null)
                return new List<Group>();

            var groupIds = dbUser.OwnedGroups.Select(x => x.GroupId).ToList();
            var groupList = new List<Group>();

            foreach (var groupId in groupIds)
            {
                groupList.Add(_groupRepository.GetGroupById(groupId));
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
                var requestsForGroup = _dbRepository.GetJoinRequestForGroup(ownedGroup.Id);

                requests.AddRange(requestsForGroup
                    .Select(joinRequestModel => new JoinRequest
                    {
                        Id = joinRequestModel.Id,
                        RequestedGroup = _groupRepository.GetGroupById(joinRequestModel.RequestedGroup.Id),
                        Requestee = _standaloneRepository.GetUserById(joinRequestModel.Requestee.Id)
                    }));
            }

            return requests;
        }

        [HttpPut]
        public ActionResult AddUserToGroup(int groupId, int userId)
        {
            var gitlabOwnerUser = new User(User);

            var dbGroup = _dbRepository.GetGroup(groupId);
            if (dbGroup == null)
                return NotFound("Group has not been added to database");

            var dbUser = _dbRepository.GetUser(gitlabOwnerUser.Id, true);

            if (dbUser.OwnedGroups.Any(g => g.Group == dbGroup && g.Role >= Role.Maintainer))
            {
                _groupRepository.AddUserToGroup(groupId, userId);
            }

            return Ok();
        }

        [HttpPut]
        public ActionResult AddCurrentUserAsGroupOwner(int groupId)
        {
            try
            {
                var gitGroup = _groupRepository.GetGroupById(groupId, true);
                var gitCurrentUser = new User(User);
                var accessLevel = gitGroup.Members.FirstOrDefault(u => u.Id == gitCurrentUser.Id)?.AccessLevel;

                if (accessLevel != null && RoleHelpers.GetRoleByValue(accessLevel.Value) >= Role.Maintainer)
                {
                    var dbUser = _dbRepository.GetUser(gitCurrentUser.Id, true);
                    var dbGroup = _dbRepository.GetGroup(gitGroup.Id, true);

                    if (dbUser == null)
                        return NotFound("User not found");
                    if (dbGroup == null)
                    {
                        dbGroup = new GroupModel(gitGroup.Id)
                        {
                            AssignedUsers = new List<UserGroupModel>()
                        };
                        _dbRepository.Add(dbGroup);
                    }

                    _dbRepository.AddUserAsOwner(dbUser, dbGroup, RoleHelpers.GetRoleByValue(accessLevel.Value));

                    return Ok();
                }
            }
            catch (GroupInaccessibleException giex)
            {
                return Unauthorized();
            }

            return Unauthorized();
        }
    }
}