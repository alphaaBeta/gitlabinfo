using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GitlabInfo.Models
{
    [DataContract]
    public class Group
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        
        [DataMember(Name = "web_url")]
        public string WebUrl { get; set; }
        
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [DataMember(Name = "path")]
        public string Path { get; set; }
        
        [DataMember(Name = "parent_id")]
        public int? ParentId { get; set; }

        [IgnoreDataMember]
        public List<Group> SubGroups { get; set; }

        [IgnoreDataMember]
        public List<Project> Projects { get; set; }

        [IgnoreDataMember]
        public List<User> Members { get; set; }
    }
}
