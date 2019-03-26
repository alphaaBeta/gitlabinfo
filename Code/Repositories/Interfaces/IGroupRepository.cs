using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Models;

namespace GitlabInfo.Code.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Group GetRootGroupByName(string groupName, bool getAllProperties = false);
        Group GetGroupById(int groupId, bool getAllProperties = false);
        void AddUserToGroup(int groupId, int userId);
    }
}
