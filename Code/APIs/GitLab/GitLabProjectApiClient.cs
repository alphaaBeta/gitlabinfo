using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GitlabInfo.Code.GitLabApis;
using GitlabInfo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;

namespace GitlabInfo.Code.APIs.GitLab
{
    public class GitLabProjectApiClient : GitLabApiClient, IProjectApiClient
    {
        public GitLabProjectApiClient(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory) : base(httpContextAccessor, httpClientFactory)
        { }
        public async Task<IEnumerable<User>> GetMembersByProjectId(int projectId)
        {
            return (await GETAsync<IEnumerable<User>>($"projects/{projectId}/members"));
        }

        public async Task<IEnumerable<Issue>> GetIssuesByProjectIdAndLabel(int projectId, IEnumerable<string> labels = null)
        {
            var relativeUrl = $"projects/{projectId}/issues?scope=all";
            if (!(labels is null))
                relativeUrl += $"&labels={labels.Join(",")}";

            return (await GETAsync<IEnumerable<Issue>>(relativeUrl));
        }

        public async Task<Project> CreateProject(Project projectModel)
        {
            var content = new
            {
                name = projectModel.Name,
                description = projectModel.Description
            };
            return await POSTAsync<Project>($"projects", content);
        }

        public async Task<Issue> CreateIssue(int projectId, Issue issueModel)
        {
            var content = new
            {
                title = issueModel.Title,
                description = issueModel.Description,
                assignee_ids = issueModel.Assignees.ToArray(),
                labels = issueModel.Labels.Join(",")
            };
            return await POSTAsync<Issue>($"projects/{projectId}/issues", content);
        }

        public async Task<User> AddUserToProject(int projectId, int userId, int accessLevel)
        {
            var content = new
            {
                user_id = userId,
                access_level = accessLevel
            };
            return (await POSTAsync<User>($"projects/{projectId}/members", content));
        }

    }
}
