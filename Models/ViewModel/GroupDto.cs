using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GitlabInfo.Models.ViewModel
{
    public class GroupDto
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "web_url")]
        public Uri WebUrl { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "path")]
        public string Path { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "parent_id")]
        public object ParentId { get; set; }
        public bool IsOwner { get; set; }
        public GroupOptions Options { get; set; }
    }
}
