using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Code.GitLabApis;
using GitlabInfo.Code.Repositories.Interfaces;
using GitlabInfo.Models;
using GitlabInfo.Models.EFModels;

namespace GitlabInfo.Code.Repositories
{
    public class GitLabProjectRepository : IProjectRepository
    {
        private IProjectApiClient _projectApiClient { get; set; }

        public GitLabProjectRepository(IProjectApiClient projectApi)
        {
            _projectApiClient = projectApi;
        }

        public void CreateProjectFromRequest(ProjectRequestModel requestModel)
        {
            var project = _projectApiClient.CreateProject(new Project()
            {
                Name = requestModel.ProjectName,
                Description = requestModel.ProjectDescription
            }).Result;

            foreach (var member in requestModel.Members)
            {
                _projectApiClient.AddUserToProject(project.Id, member.Id, 50);
            }
        }
    }
}
