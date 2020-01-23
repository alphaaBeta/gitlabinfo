using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.ExcelExport
{
    public class ReportedTime
    {
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public double TimeInHours { get; set; }
        public string Description { get; set; }
        public int IssueId { get; set; }
        public int ProjectId { get; set; }
        public int ProjectName { get; set; }
        public DateTime ReportedDate { get; set; }
    }
}
