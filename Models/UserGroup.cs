using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models
{
    /// <summary>
    /// Join entity to map many-to-many relationship in EF
    /// </summary>
    public class UserGroup
    {
        public int UserGitLabId { get; set; }
        public User User { get; set; }
        public int GroupGitLabId { get; set; }
        public Group Group { get; set; }
        public Role Role { get; set; }
    }
}
