using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models
{
    public class JoinRequest
    {
        public int Id { get; set; }
        public User Requestee { get; set; }
        public Group RequestedGroup { get; set; }
    }
}
