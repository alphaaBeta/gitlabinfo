using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.EFModels
{
    [Table("ReportedTimes")]
    public class ReportedTimeModel
    {
        [Key]
        public int Id { get; set; }
        public UserModel User { get; set; }
        public DateTime Date { get; set; }
        public double TimeInHours { get; set; }
        public string Description { get; set; }
        public int IssueId { get; set; }
        public ProjectModel Project { get; set; }
        public DateTime ReportedDate { get; set; }
    }
}
