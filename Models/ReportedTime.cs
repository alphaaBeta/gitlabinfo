﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models
{
    public class ReportedTime
    {
        public User User { get; set; }
        public Project Project { get; set; }
        public DateTime Date { get; set; }
        public double TimeInHours { get; set; }
        public string Description { get; set; }
        public int IssueId { get; set; }
    }
}
