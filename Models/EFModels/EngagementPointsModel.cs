using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.EFModels
{
    [Table("EngagementPoints")]
    public class EngagementPointsModel
    {
        [Key]
        public int Id { get; set; }
        public UserModel AwardingUser { get; set; }
        public UserModel ReceivingUser { get; set; }
        public int Points { get; set; }
        public DateTime ReceivingDate { get; set; }
        public ProjectModel Project { get; set; }
    }
}
