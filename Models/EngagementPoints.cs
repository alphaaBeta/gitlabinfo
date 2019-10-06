using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models
{
    public class EngagementPoints
    {
        public User AwardingUser { get; set; }
        public User ReceivingUser { get; set; }
        public int Points { get; set; }
        public DateTime ReceivingDate { get; set; }
    }
}
