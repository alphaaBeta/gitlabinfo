using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GitlabInfo.Models.ViewModel
{
    [DataContract]
    public class ProjectRequestGetDto
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "members")]
        public IEnumerable<UserDto> Members { get; set; }
        [DataMember(Name = "parent_group_id")]
        public int ParentGroupId { get; set; }
    }
}
