using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GitlabInfo.Models.ViewModel
{
    public class GroupDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("web_url")]
        public Uri WebUrl { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("path")]
        public string Path { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("parent_id")]
        public object ParentId { get; set; }
        public bool IsOwner { get; set; }
        public GroupOptions Options { get; set; }
    }
}
