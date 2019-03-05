using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitlabInfo.Models.EFModels;

namespace GitlabInfo.Models.EFModels
{
    /// <summary>
    /// Join entity to map many-to-many relationship in EF
    /// </summary>
    public class UserGroupModel
    {
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public int GroupId { get; set; }
        public GroupModel Group { get; set; }
        public Role Role { get; set; }
    }
}
