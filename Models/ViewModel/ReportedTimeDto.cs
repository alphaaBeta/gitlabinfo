using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.ViewModel
{
    public class ReportedTimeDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ProjectId { get; set; }
        public DateTime Date { get; set; }
        public double TimeInHours { get; set; }
        public string Description { get; set; }
    }
}
