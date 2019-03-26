using GitlabInfo.Code.APIs.GitLab;
using GitlabInfo.Models;

namespace GitlabInfo.Code.Repositories
{
    public interface IStandaloneRepository
    {
        User GetUserById(int userId);
    }
}