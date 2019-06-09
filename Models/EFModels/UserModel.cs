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
        public UserModel(int id, DateTime firstJoined, DateTime lastJoined)
        {
            Id = id;
            FirstJoined = firstJoined;
            LastJoined = lastJoined;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public virtual ICollection<UserGroupModel> OwnedGroups { get; set; }

        public DateTime FirstJoined { get; set; }

        public DateTime LastJoined { get; set; }

    }
}
