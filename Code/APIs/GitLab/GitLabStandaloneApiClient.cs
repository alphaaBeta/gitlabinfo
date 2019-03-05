using System.Net.Http;
using GitlabInfo.Code.GitLabApis;
using Microsoft.AspNetCore.Http;

namespace GitlabInfo.Code.APIs.GitLab
{
    public class GitLabStandaloneApiClient : GitLabApiClient, IStandaloneApiClient
    {
        public GitLabStandaloneApiClient(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory) : base(httpContextAccessor, httpClientFactory)
        { }
    }
}
