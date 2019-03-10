using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Code.APIs.GitLab;
using GitlabInfo.Code.GitLabApis;
using GitlabInfo.Code.Repositories.Interfaces;
using GitlabInfo.Models;

namespace GitlabInfo.Code.Repositories
{
    public class GitLabGroupRepository : IGroupRepository
    {
        private GitLabGroupApiClient _groupApi { get; set; }
        public GitLabGroupRepository(GitLabGroupApiClient groupApiClient)
        {
            _groupApi = groupApiClient;
        }
        public Group GetRootGroupByName(string groupName, bool getAllProperties = true)
        {
            var group = _groupApi.GetRootGroupByNameAsync(groupName).Result;
            if (getAllProperties)
                FillGroupProperties(ref group);

            return group;
        }

        public Group GetGroupById(int groupId, bool getAllProperties = true)
        {
            var group = _groupApi.GetGroupByIdAsync(groupId).Result;
            if (getAllProperties)
                FillGroupProperties(ref group);

            return group;
        }

        private void FillGroupProperties(ref Group group)
        {
            group.SubGroups = _groupApi.GetSubGroupsByGroupIdAsync(group.Id).Result;
            group.Projects = _groupApi.GetProjectsByGroupIdAsync(group.Id).Result;
            group.Members = _groupApi.GetMembersByGroupIdAsync(group.Id).Result;
        }
    }
}
