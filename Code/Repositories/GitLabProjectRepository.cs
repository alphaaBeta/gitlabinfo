using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public async Task<Project> CreateProjectFromRequest(ProjectRequestModel requestModel)
        {
            var project = await _projectApiClient.CreateProject(new Project()
            {
                Name = requestModel.ProjectName,
                Description = requestModel.ProjectDescription,
                NamespaceId = requestModel.ParentGroup.Id
            });

            //var taskList = new List<Task<User>>();

            foreach (var member in requestModel.Members)
            {
                //taskList.Add(_projectApiClient.AddUserToProject(project.Id, member.Id, 40));
                try
                {
                    await _projectApiClient.AddUserToProject(project.Id, member.Id, 40);
                }
                catch (HttpRequestException)
                { }
            }

            return project;
            //await Task.WhenAll(taskList.ToArray());

        }

        public Project GetProjectDetails(int projectId)
        {
            return _projectApiClient.GetProjectDetails(projectId).Result;
        }

        public IEnumerable<User> GetMembers(int projectId)
        {
            return _projectApiClient.GetMembersByProjectId(projectId).Result;
        }
    }
}
