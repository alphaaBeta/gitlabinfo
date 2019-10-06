using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.ViewModel
{
    public class EngagementPointsPutDto
    {
        public int ReceivingUserId { get; set; }
        public int Points { get; set; }
        public int ProjectId { get; set; }
    }
}
