using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GitlabInfo.Code.Repositories.Interfaces;
using GitlabInfo.Models;
using GitlabInfo.Models.EFModels;

namespace GitlabInfo.Code.Helpers
{
    public static class PermissionHelper
    {
        public static bool IsUserGroupOwner(ClaimsPrincipal claimsPrincipal, int groupId, IGitLabInfoDbRepository dbRepository)
        {
            var gitlabOwnerUser = new User(claimsPrincipal);

            var dbGroup = dbRepository.GetGroup(groupId);

            if (dbGroup is null)
                throw new ArgumentException("Group has not been added to database");

            var dbUser = dbRepository.GetUsers(user => user.Id == gitlabOwnerUser.Id, true).First();

            if (dbUser.OwnedGroups.Any(g => g.Group == dbGroup && g.Role >= Role.Maintainer))
                return true;
            else
                return false;
        }
    }
}
