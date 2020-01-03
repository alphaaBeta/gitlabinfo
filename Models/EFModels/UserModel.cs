using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GitlabInfo.Models.EFModels
{
    [Table("Users")]
    public class UserModel
    {
        public UserModel() { }
        public UserModel(int id, string email, string name, DateTime? firstJoined = null, DateTime? lastJoined = null)
        {
            Id = id;
            Email = email;
            Name = name;
            FirstJoined = firstJoined.GetValueOrDefault();
            LastJoined = lastJoined.GetValueOrDefault();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime FirstJoined { get; set; }
        public DateTime LastJoined { get; set; }
        public virtual ICollection<UserGroupModel> UserGroups { get; set; }
        public virtual ICollection<ReportedTimeModel> ReportedTimes { get; set; }
        public virtual ICollection<UserProjectRequestModel> UserProjectRequestModels { get; set; }

    }
}
