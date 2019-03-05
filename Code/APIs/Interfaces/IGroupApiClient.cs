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
    }
}
