using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GitlabInfo.Models
{
    public class ProjectRequestPutDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("member_emails")]
        public IEnumerable<string> MemberEmails { get; set; }
        [JsonPropertyName("parent_group_id")]
        public int ParentGroupId { get; set; }
    }
}
