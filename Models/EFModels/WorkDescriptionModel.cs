using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.EFModels
{
    [Table("WorkDescriptions")]
    public class WorkDescriptionModel
    {
        [Key]
        public int WorkDescriptionId { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public UserModel User { get; set; }
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }
        public ProjectModel Project { get; set; }
        [InverseProperty("WorkDescription")]
        public virtual ICollection<WorkDescriptionCommentModel> Comments { get; set; }
        public string Description { get; set; }
    }
}
