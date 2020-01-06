using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.ViewModel
{
    public class GroupOptionsPostDto
    {
        public int GroupId { get; set; }
        public string SurveyString { get; set; }
        public int? SurveyId { get; set; }
        public bool ReportTimeEnabled { get; set; }
        public bool EngagementPointsEnabled { get; set; }
        public bool WorkDescriptionEnabled { get; set; }
        public bool WorkDescriptionCommentsEnabled { get; set; }
        public bool SurveyEnabled { get; set; }
    }
}
