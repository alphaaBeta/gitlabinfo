using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.ExcelExport
{
    public class EngagementPoints
    {
        public string AwardingUserName { get; set; }
        public string ReceivingUserName { get; set; }
        public int Points { get; set; }
        public DateTime ReceivingDate { get; set; }
        public string ProjectName { get; set; }
        public bool Bonus { get; set; }
        public string Comment { get; set; }
    }
}
