using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GitlabInfo.Code.GitLabApis;
using GitlabInfo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;

namespace GitlabInfo.Code.APIs.GitLab
{
    public class GitLabGroupApiClient : GitLabApiClient, IGroupApiClient
    {
        public GitLabGroupApiClient(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory) : base(httpContextAccessor, httpClientFactory)
        {}

        public async Task<Group> GetRootGroupByNameAsync(string groupName)
        {
            return (await GETAsync<Group>($"groups/{groupName}/"));
        }

        public async Task<Group> GetGroupByIdAsync(int groupId)
        {
            return (await GETAsync<Group>($"groups/{groupId}"));
        }

        public async Task<List<Group>> GetSubGroupsByGroupIdAsync(int groupId)
        {
            return (await GETAsync<List<Group>>($"groups/{groupId}/subgroups"));
        }

        public async Task<List<Project>> GetProjectsByGroupIdAsync(int groupId)
        {
            return (await GETAsync<List<Project>>($"groups/{groupId}/projects"));
        }

        public async Task<List<User>> GetMembersByGroupIdAsync(int groupId)
        {
            return (await GETAsync<List<User>>($"groups/{groupId}/members"));
        }

        public async Task<User> AddUserToGroup(int groupId, int userId, int accessLevel)
        {
            var content = new
            {
                user_id = userId,
                access_level = accessLevel
            };
            return (await POSTAsync<User>($"groups/{groupId}/members", content));
        }

        public async Task<List<Issue>> GetAllIssuesFromGroup(int groupId, string[] labels = null)
        {
            var parameters = string.Empty;
            if (labels != null)
            {
                parameters = $"?labels={labels.Join(",")}";
            }

            return (await GETAsync<List<Issue>>($"groups/{groupId}/issues{parameters}"));
        }
    }
}
