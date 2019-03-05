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
            Id = int.Parse(user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            Email = user.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            Name = user.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;
            Login = user.FindFirst(c => c.Type == ClaimsTypesExtensions.Login)?.Value;
            WebUrl = user.FindFirst(c => c.Type == ClaimsTypesExtensions.WebUrl)?.Value;
            AvatarUrl = user.FindFirst(c => c.Type == ClaimsTypesExtensions.AvatarUrl)?.Value;
        }
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Login { get; set; }
        
        public string WebUrl { get; set; }
        
        public string AvatarUrl { get; set; }
    }
}
