using System;

namespace GitlabInfo.Models.ViewModel
{
    public class ReportedTimePostDto
    {
        public int ProjectId { get; set; }
        public DateTime Date { get; set; }
        public string TimeInHours { get; set; }
        public string Description { get; set; }
        public string IssueId { get; set; }
    }
}
