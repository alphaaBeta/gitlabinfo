using GitlabInfo.Code.GitLabApis;
using GitlabInfo.Code.Repositories.Interfaces;
using GitlabInfo.Models;
using GitlabInfo.Models.EFModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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

            var taskList = new List<Task<User>>();

            try
            {
                foreach (var member in requestModel.Members)
                {
                    taskList.Add(_projectApiClient.AddUserToProject(project.Id, member.UserId, 40));
                }

                await Task.WhenAll(taskList);
            }
            catch (HttpRequestException)
            { }

            return project;

        }

        public Task<Project> GetProjectDetails(int projectId)
        {
            return _projectApiClient.GetProjectDetails(projectId);
        }

        public Task<IEnumerable<User>> GetMembers(int projectId, bool getAll = false)
        {
            return _projectApiClient.GetMembersByProjectId(projectId, getAll);
        }
    }
}
