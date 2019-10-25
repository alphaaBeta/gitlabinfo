using GitlabInfo.Code.GitLabApis;
using GitlabInfo.Models;
using System.Threading.Tasks;

namespace GitlabInfo.Code.Repositories
{
    public class GitLabStandaloneRepository : IStandaloneRepository
    {
        private IStandaloneApiClient _StandaloneApi { get; set; }
        public GitLabStandaloneRepository(IStandaloneApiClient standaloneApiClient)
        {
            _StandaloneApi = standaloneApiClient;
        }

        public Task<User> GetUserById(int userId)
        {
            return _StandaloneApi.GetUserByIdAsync(userId);
        }

        public Task<User> GetUserByEmail(string userEmail)
        {
            return _StandaloneApi.TryGetUserByEmail(userEmail);
        }
    }
}
