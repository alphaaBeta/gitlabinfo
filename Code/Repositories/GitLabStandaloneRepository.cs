using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Code.APIs.GitLab;
using GitlabInfo.Code.GitLabApis;
using GitlabInfo.Models;

namespace GitlabInfo.Code.Repositories
{
    public class GitLabStandaloneRepository : IStandaloneRepository
    {
        private IStandaloneApiClient _StandaloneApi { get; set; }
        public GitLabStandaloneRepository(IStandaloneApiClient standaloneApiClient)
        {
            _StandaloneApi = standaloneApiClient;
        }

        public User GetUserById(int userId)
        {
            return _StandaloneApi.GetUserByIdAsync(userId).Result;
        }
    }
}
