using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.ViewModel
{
    public class WorkDescriptionGetDto
    {
        public int Id { get; set; }
        public UserDto User { get; set; }
        public string Description { get; set; }
        public List<string> Comments { get; set; }
    }
}
