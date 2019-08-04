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
    public class ProjectController : ControllerBase
    {
        private readonly ILogger _logger;
        public IGitLabInfoDbRepository DbRepository { get; private set; }
        public IProjectRepository ProjectRepository { get; private set; }
        public IStandaloneRepository StandaloneRepository { get; private set; }

        public ProjectController(ILogger<ProjectController> logger, IProjectRepository projectRepository, IStandaloneRepository standaloneRepository, IGitLabInfoDbRepository dbRepository)
        {
            _logger = logger;
            ProjectRepository = projectRepository;
            StandaloneRepository = standaloneRepository;
            DbRepository = dbRepository;
        }
        //TODO: Tests
        [HttpPost]
        public ActionResult RequestProjectCreation(Project projectModel, int parentGroupId, IEnumerable<string> memberEmails)
        {
            var gitlabUser = new User(User);
            var dbUser = DbRepository.GetUsers(user => user.Id == gitlabUser.Id, true).First();
            var isUserOwner = !(dbUser.OwnedGroups.FirstOrDefault(g => g.GroupId == parentGroupId) is null);
            var parentGroup = DbRepository.GetGroup(parentGroupId);

            var memberList = new List<UserModel>();
            foreach (var email in memberEmails)
            {
                var dbMember = DbRepository.GetUsers(user => user.Email == email).FirstOrDefault();

                if (dbMember is null)
                {
                    var gitlabMember = StandaloneRepository.GetUserByEmail(email);
                    if (gitlabMember is null)
                        continue;

                    dbMember = new UserModel(gitlabMember.Id, gitlabMember.Email);
                    DbRepository.Add(dbMember);
                }

                memberList.Add(dbMember);
            }

            //No need to add owner to members
            if (!isUserOwner)
                memberList.Add(dbUser);

            DbRepository.Add(new ProjectRequestModel()
            {
                Requestee = dbUser,
                Members = memberList,
                ProjectName = projectModel.Name,
                ProjectDescription = projectModel.Description,
                ParentGroup = parentGroup
            });

            return Ok();
        }
    }
}