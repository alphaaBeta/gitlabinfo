using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GitlabInfo.Code.GitLabApis;
using GitlabInfo.Models;
using Microsoft.AspNetCore.Http;

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
    }
}
