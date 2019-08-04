using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.EFModels
{
    public class UserModel
    {
        public UserModel() { }
        public UserModel(int id, string email, DateTime? firstJoined = null, DateTime? lastJoined = null)
        {
            Id = id;
            Email = email;
            FirstJoined = firstJoined.GetValueOrDefault();
            LastJoined = lastJoined.GetValueOrDefault();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Email { get; set; }

        public virtual ICollection<UserGroupModel> OwnedGroups { get; set; }

        public DateTime FirstJoined { get; set; }

        public DateTime LastJoined { get; set; }

    }
}
