using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GitlabInfo.Models.ViewModel
{
    public class ProjectGetDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("path_with_namespace")]
        public string PathWithNamespace { get; set; }
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }
        [JsonPropertyName("last_activity_at")]
        public string LastActivityAt { get; set; }
        public IEnumerable<UserDto> Members { get; set; }

    }
}
