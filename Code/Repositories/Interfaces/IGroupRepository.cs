using GitlabInfo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitlabInfo.Code.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group> GetRootGroupByName(string groupName, bool getAllProperties = false);
        Task<Group> GetGroupById(int groupId, bool getAllProperties = false);
        Task<User> AddUserToGroup(int groupId, int userId);
        Task<IEnumerable<Issue>> GetIssuesGroupedByProject(int groupId);
        Task<IEnumerable<Project>> GetProjects(int groupId);
    }
}
