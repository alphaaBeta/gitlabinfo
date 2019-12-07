using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.ViewModel
{
    public class EngagementPointsGetDto
    {
        public UserDto AwardingUser { get; set; }
        public UserDto ReceivingUser { get; set; }
        public int Points { get; set; }
        public DateTime ReceivingDate { get; set; }
    }
}
