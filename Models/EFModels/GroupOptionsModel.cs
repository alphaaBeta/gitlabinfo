using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.EFModels
{
    [Table("GroupOptions")]
    public class GroupOptionsModel
    {
        [Key]
        public int GroupOptionsId { get; set; }
        public GroupModel Group { get; set; }
        [ForeignKey(nameof(Group))]
        public int GroupId { get; set; }
        public SurveyModel Survey { get; set; }
        [ForeignKey(nameof(Survey))]
        public int? SurveyId { get; set; }
        public bool ReportTimeEnabled { get; set; }
        public bool EngagementPointsEnabled { get; set; }
        public bool WorkDescriptionEnabled { get; set; }
        public bool WorkDescriptionCommentsEnabled { get; set; }
        public bool SurveyEnabled { get; set; }
        public bool AllowsProjectCreation { get; set; }
    }
}
