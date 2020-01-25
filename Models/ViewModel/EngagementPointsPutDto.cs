using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.ViewModel
{
    public class EngagementPointsPutDto
    {
        public UserDto ReceivingUser { get; set; }
        public string Points { get; set; }
        public int ProjectId { get; set; }
        public bool Bonus { get; set; }
        public string Comment { get; set; }
    }
}
