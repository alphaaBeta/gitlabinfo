using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GitlabInfo.Code.Extensions;

namespace GitlabInfo.Models
{
    public class User
    {
        public User(ClaimsPrincipal user)
        {
            GitLabId = int.Parse(user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            GitLabEmail = user.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            GitLabName = user.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
            GitLabLogin = user.FindFirst(c => c.Type == ClaimsTypesExtensions.Login)?.Value;
            GitLabWebUrl = user.FindFirst(c => c.Type == ClaimsTypesExtensions.WebUrl)?.Value;
            GitLabAvatarUrl = user.FindFirst(c => c.Type == ClaimsTypesExtensions.AvatarUrl)?.Value;
        }
        public int GitLabId { get; set; }
        public string GitLabName { get; set; }
        public string GitLabEmail { get; set; }
        public string GitLabLogin { get; set; }
        public string GitLabWebUrl { get; set; }
        public string GitLabAvatarUrl { get; set; }
    }
}
