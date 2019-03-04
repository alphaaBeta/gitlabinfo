using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models
{
    public class Group
    {
        public Group(int gitLabId)
        {
            GitLabId = gitLabId;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GitLabId { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<UserGroup> AssignedUsers { get; set; }
    }
}
