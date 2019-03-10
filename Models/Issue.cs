using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GitlabInfo.Models
{
    [DataContract]
    public class Issue
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "iid")]
        public int IssueId { get; set; }

        [DataMember(Name = "project_id")]
        public int ProjectId { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime Created { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime Updated { get; set; }

        [DataMember(Name = "web_url")]
        public string WebUrl { get; set; }
        
        [IgnoreDataMember]
        public User Assignee { get; set; }
    }
}
