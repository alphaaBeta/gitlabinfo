using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.EFModels
{
    public class ProjectRequestModel
    {
        [Key]
        public int Id { get; set; }
        public UserModel Requestee { get; set; }
        public ICollection<UserModel> Members { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public GroupModel ParentGroup { get; set; }
    }
}
