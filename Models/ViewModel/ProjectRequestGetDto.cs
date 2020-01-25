using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GitlabInfo.Models.ViewModel
{
    public class ProjectRequestGetDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("members")]
        public IEnumerable<UserDto> Members { get; set; }
        [JsonPropertyName("parent_group_id")]
        public int ParentGroupId { get; set; }
    }
}
