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
    public class Project
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        
        [DataMember(Name = "description")]
        public string Description { get; set; }
        
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [DataMember(Name = "path")]
        public string Path { get; set; }
        
        [DataMember(Name = "path_with_namespace")]
        public string PathWithNamespace { get; set; }
        
        [DataMember(Name = "web_url")]
        public string WebUrl { get; set; }
    }
}
