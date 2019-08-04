using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Models;

namespace GitlabInfo.Code.GitLabApis
{
    public interface IGroupApiClient
    {
        Task<Group> GetRootGroupByNameAsync(string groupName);

        Task<Group> GetGroupByIdAsync(int groupId);

        Task<List<Group>> GetSubGroupsByGroupIdAsync(int groupId);

        Task<List<Project>> GetProjectsByGroupIdAsync(int groupId);

        Task<List<User>> GetMembersByGroupIdAsync(int groupId);

        Task<User> AddUserToGroup(int groupId, int userId, int accessLevel);
    }
}
