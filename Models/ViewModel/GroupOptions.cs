using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.ViewModel
{
    public class GroupOptions
    {
        public bool HasNewData { get; set; }
        public bool ReportTimeEnabled { get; set; }
        public bool EngagementPointsEnabled { get; set; }
        public bool WorkDescriptionEnabled { get; set; }
        public bool WorkDescriptionCommentsEnabled { get; set; }
        public bool SurveyEnabled { get; set; }
        public bool AllowsProjectCreation { get; set; }
    }
}
