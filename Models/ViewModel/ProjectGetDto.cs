using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GitlabInfo.Models.ViewModel
{
    public class ProjectGetDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        [DataMember(Name = "path_with_namespace")]
        public string PathWithNamespace { get; set; }
        [DataMember(Name = "created_at")]
        public string CreatedAt { get; set; }
        [DataMember(Name = "last_activity_at")]
        public string LastActivityAt { get; set; }
        public IEnumerable<UserDto> Members { get; set; }

    }
}
