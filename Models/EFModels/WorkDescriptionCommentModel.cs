using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.EFModels
{
    [Table("WorkDescriptionComments")]
    public class WorkDescriptionCommentModel
    {
        [Key]
        public int WorkDescriptionCommentId { get; set; }
        [ForeignKey(nameof(Commenter))]
        public int CommenterId { get; set; }
        public UserModel Commenter { get; set; }
        [ForeignKey(nameof(WorkDescription))]
        public int WorkDescriptionId { get; set; }
        public WorkDescriptionModel WorkDescription { get; set; }
        public string Comment { get; set; }
    }
}
