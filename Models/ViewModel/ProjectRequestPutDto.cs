using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GitlabInfo.Models
{
    [DataContract]
    public class ProjectRequestPutDto
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "member_emails")]
        public IEnumerable<string> MemberEmails { get; set; }
        [DataMember(Name = "parent_group_id")]
        public int ParentGroupId { get; set; }
    }
}
