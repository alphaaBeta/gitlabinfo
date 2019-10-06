using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GitlabInfo.Models.EFModels
{
    [Table("Groups")]
    public class GroupModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public virtual ICollection<UserGroupModel> AssignedUsers { get; set; }
        [InverseProperty("AssignedGroup")]
        public virtual ICollection<ProjectModel> Projects { get; set; }
    }
}
