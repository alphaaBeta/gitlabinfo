using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.EFModels
{
    [Table("ProjectRequests")]
    public class ProjectRequestModel
    {
        [Key]
        public int Id { get; set; }
        public ICollection<UserProjectRequestModel> Members { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public GroupModel ParentGroup { get; set; }
        [ForeignKey(nameof(ParentGroup))]
        public int ParentGroupId { get; set; }
    }
}
