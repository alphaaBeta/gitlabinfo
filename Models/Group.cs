using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GitlabInfo.Models
{
    [DataContract]
    public class Group
    {
        public Group(int gitLabId)
        {
            GitLabId = gitLabId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataMember(Name = "id")]
        public int GitLabId { get; set; }

        [IgnoreDataMember]
        public ICollection<Project> Projects { get; set; }

        [IgnoreDataMember]
        public ICollection<UserGroup> AssignedUsers { get; set; }

        #region unmapped db properties

        [NotMapped]
        [DataMember(Name = "web_url")]
        public string WebUrl { get; set; }

        [NotMapped]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [NotMapped]
        [DataMember(Name = "path")]
        public string Path { get; set; }

        [NotMapped]
        [DataMember(Name = "parent_id")]
        public int? ParentId { get; set; }

        #endregion
    }
}
