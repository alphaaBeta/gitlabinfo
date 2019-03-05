using System.Net.Http;
using GitlabInfo.Code.GitLabApis;
using Microsoft.AspNetCore.Http;

namespace GitlabInfo.Code.APIs.GitLab
{
    public class GitLabProjectApiClient : GitLabApiClient, IProjectApiClient
    {
        public GitLabProjectApiClient(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory) : base(httpContextAccessor, httpClientFactory)
        { }
    }
}
