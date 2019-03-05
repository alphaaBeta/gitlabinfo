using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GitlabInfo.Models
{
    public class Project
    {
        public Project(int gitLabId)
        {
            GitLabId = gitLabId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataMember(Name = "id")]
        public int GitLabId { get; set; }

        #region unmapped db properties

        [NotMapped]
        [DataMember(Name = "description")]
        public string Description { get; set; }

        [NotMapped]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [NotMapped]
        [DataMember(Name = "path")]
        public string Path { get; set; }

        [NotMapped]
        [DataMember(Name = "path_with_namespace")]
        public string PathWithNamespace { get; set; }

        [NotMapped]
        [DataMember(Name = "web_url")]
        public string WebUrl { get; set; }

        #endregion


    }
}
