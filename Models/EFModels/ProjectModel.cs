using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GitlabInfo.Models.EFModels
{
    [Table("Projects")]
    public class ProjectModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public GroupModel AssignedGroup { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<ReportedTimeModel> ReportedTimes { get; set; }
        [InverseProperty("Project")]
        public virtual ICollection<EngagementPointsModel> EngagementPoints { get; set; }
    }
}
