using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public User(int gitLabId, DateTime firstJoined, DateTime lastJoined)
        {
            GitLabId = gitLabId;
            FirstJoined = firstJoined;
            LastJoined = lastJoined;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GitLabId { get; set; }
        public ICollection<UserGroup> OwnedGroups { get; set; }
        public DateTime FirstJoined { get; set; }
        public DateTime LastJoined { get; set; }

        #region unmapped db properties
        [NotMapped]
        public string GitLabName { get; set; }
        [NotMapped]
        public string GitLabEmail { get; set; }
        [NotMapped]
        public string GitLabLogin { get; set; }
        [NotMapped]
        public string GitLabWebUrl { get; set; }
        [NotMapped]
        public string GitLabAvatarUrl { get; set; }
        #endregion
    }
}
