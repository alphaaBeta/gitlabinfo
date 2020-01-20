using GitlabInfo.Models;
using System.Threading.Tasks;

namespace GitlabInfo.Code.Repositories
{
    public interface IStandaloneRepository
    {
        Task<User> GetUserById(int userId);
        Task<User> GetUserByEmail(string userEmail);
    }
}