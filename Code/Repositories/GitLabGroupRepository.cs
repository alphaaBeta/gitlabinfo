using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Code.APIs.GitLab;
using GitlabInfo.Code.Exceptions;
using GitlabInfo.Code.GitLabApis;
using GitlabInfo.Code.Repositories.Interfaces;
using GitlabInfo.Models;

namespace GitlabInfo.Code.Repositories
{
    public class GitLabGroupRepository : IGroupRepository
    {
        private IGroupApiClient _groupApi { get; set; }
        public GitLabGroupRepository(IGroupApiClient groupApiClient)
        {
            _groupApi = groupApiClient;
        }
        public async Task<Group> GetRootGroupByName(string groupName, bool getAllProperties = false)
        {
            try
            {
                var group = await _groupApi.GetRootGroupByNameAsync(groupName);
                if (getAllProperties)
                    FillGroupProperties(group);

                return group;
            }
            catch (Exception)
            {
                throw new GroupInaccessibleException("Group does not exist or you do not have access to see it.");
            }
        }

        public async Task<Group> GetGroupById(int groupId, bool getAllProperties = false)
        {
            try
            {
                var group = await _groupApi.GetGroupByIdAsync(groupId);
                if (getAllProperties)
                    FillGroupProperties(group);

                return group;
            }
            catch (Exception)
            {
                throw new GroupInaccessibleException("Group does not exist or you do not have access to see it.");
            }
        }

        public Task<User> AddUserToGroup(int groupId, int userId)
        {
            try
            {
                return _groupApi.AddUserToGroup(groupId, userId, 10);
            }
            catch (Exception)
            {
                throw new GroupInaccessibleException("Group does not exist or you do not have access to use that function.");
            }
        }

        public async Task<IEnumerable<Issue>> GetIssuesGroupedByProject(int groupId)
        {
            return await _groupApi.GetAllIssuesFromGroup(groupId, null);
        }

        public async Task<IEnumerable<Project>> GetProjects(int groupId)
        {
            return await _groupApi.GetProjectsByGroupIdAsync(groupId);
        }

        private async void FillGroupProperties(Group group)
        {
            group.SubGroups = (await _groupApi.GetSubGroupsByGroupIdAsync(group.Id)).ToList();
            group.Projects = (await _groupApi.GetProjectsByGroupIdAsync(group.Id)).ToList();
            group.Members = (await _groupApi.GetMembersByGroupIdAsync(group.Id)).ToList();
        }
    }
}
