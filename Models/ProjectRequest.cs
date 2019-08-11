using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models
{
    public class ProjectRequest
    {
        public Project Project { get; set; }
        public IEnumerable<string> MemberEmails { get; set; }
    }
}
