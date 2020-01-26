using System.Security.Claims;
using System.Text.Json.Serialization;
using GitlabInfo.Code.Extensions;

namespace GitlabInfo.Models
{
    public class User
    {
        public User() { }
        /// <summary>
        /// Gets user using supplied ClaimsPrincipal
        /// </summary>
        /// <param name="user">ClaimsPrincipal with claims set</param>
        public User(ClaimsPrincipal user)
        {
            Id = int.Parse(user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            Email = user.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            Name = user.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
            Login = user.FindFirst(c => c.Type == ClaimsTypesExtensions.Login)?.Value;
            WebUrl = user.FindFirst(c => c.Type == ClaimsTypesExtensions.WebUrl)?.Value;
            AvatarUrl = user.FindFirst(c => c.Type == ClaimsTypesExtensions.AvatarUrl)?.Value;
        }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("username")]
        public string Login { get; set; }

        [JsonPropertyName("web_url")]
        public string WebUrl { get; set; }

        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonPropertyName("access_level")]
        public int AccessLevel { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonIgnore]
        public string Email { get; set; }
    }
}
