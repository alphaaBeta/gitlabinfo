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
        public Group GetRootGroupByName(string groupName, bool getAllProperties = false)
        {
            try
            {
                var group = _groupApi.GetRootGroupByNameAsync(groupName).Result;
                if (getAllProperties)
                    FillGroupProperties(group);

                return group;
            }
            catch (Exception ex)
            {
                throw new GroupInaccessibleException("Group does not exist or you do not have access to see it.");
            }
        }

        public Group GetGroupById(int groupId, bool getAllProperties = false)
        {
            try
            {
                var group = _groupApi.GetGroupByIdAsync(groupId).Result;
                if (getAllProperties)
                    FillGroupProperties(group);

                return group;
            }
            catch (Exception ex)
            {
                throw new GroupInaccessibleException("Group does not exist or you do not have access to see it.");
            }
        }

        public void AddUserToGroup(int groupId, int userId)
        {
            try
            {
                var user = _groupApi.AddUserToGroup(groupId, userId, 10);
            }
            catch (Exception ex)
            {
                throw new GroupInaccessibleException("Group does not exist or you do not have access to use that function.");
            }
        }

        public IEnumerable<Issue> GetIssuesGroupedByProject(int groupId)
        {
            return _groupApi.GetAllIssuesFromGroup(groupId, null).Result;
        }

        public IEnumerable<Project> GetProjects(int groupId)
        {
            return _groupApi.GetProjectsByGroupIdAsync(groupId).Result;
        }

        private void FillGroupProperties(Group group)
        {
                group.SubGroups = _groupApi.GetSubGroupsByGroupIdAsync(group.Id).Result;
                group.Projects = _groupApi.GetProjectsByGroupIdAsync(group.Id).Result;
                group.Members = _groupApi.GetMembersByGroupIdAsync(group.Id).Result;
        }
    }
}
