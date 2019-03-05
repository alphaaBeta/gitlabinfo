using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GitlabInfo.Models.EFModels
{
    public class GroupModel
    {
        public GroupModel(int id)
        {
            Id = id;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        
        public ICollection<ProjectModel> Projects { get; set; }
        
        public ICollection<UserGroupModel> AssignedUsers { get; set; }
    }
}
