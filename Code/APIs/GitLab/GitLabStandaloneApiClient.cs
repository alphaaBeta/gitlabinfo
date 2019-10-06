using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GitlabInfo.Code.GitLabApis;
using GitlabInfo.Models;
using Microsoft.AspNetCore.Http;

namespace GitlabInfo.Code.APIs.GitLab
{
    public class GitLabStandaloneApiClient : GitLabApiClient, IStandaloneApiClient
    {
        public GitLabStandaloneApiClient(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory) : base(httpContextAccessor, httpClientFactory)
        { }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return (await GETAsync<User>($"users/{userId}"));
        }

        public async Task<User> TryGetUserByEmail(string userEmail)
        {
            return (await GETAsync<IEnumerable<User>>($"users?search={userEmail}"))?.FirstOrDefault();
        }
    }
}
