using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Threading.Tasks;
using GitlabInfo.Code.Extensions;

namespace GitlabInfo.Models
{
    [DataContract]
    public class User
    {
        public User(ClaimsPrincipal user)
        {
            Id = int.Parse(user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            Email = user.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            Name = user.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
            Login = user.FindFirst(c => c.Type == ClaimsTypesExtensions.Login)?.Value;
            WebUrl = user.FindFirst(c => c.Type == ClaimsTypesExtensions.WebUrl)?.Value;
            AvatarUrl = user.FindFirst(c => c.Type == ClaimsTypesExtensions.AvatarUrl)?.Value;
        }
        
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "username")]
        public string Login { get; set; }

        [DataMember(Name = "web_url")]
        public string WebUrl { get; set; }

        [DataMember(Name = "avatar_url")]
        public string AvatarUrl { get; set; }

        [DataMember(Name = "access_level")]
        public int AccessLevel { get; set; }

        [IgnoreDataMember]
        public string Email { get; set; }
    }
}
