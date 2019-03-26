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
        #region Parsed properties
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "web_url")]
        public Uri WebUrl { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "visibility")]
        public string Visibility { get; set; }

        [DataMember(Name = "lfs_enabled")]
        public bool LfsEnabled { get; set; }

        [DataMember(Name = "avatar_url")]
        public object AvatarUrl { get; set; }

        [DataMember(Name = "request_access_enabled")]
        public bool RequestAccessEnabled { get; set; }

        [DataMember(Name = "full_name")]
        public string FullName { get; set; }

        [DataMember(Name = "full_path")]
        public string FullPath { get; set; }

        [DataMember(Name = "parent_id")]
        public object ParentId { get; set; }

        //[DataMember(Name = "projects")]
        //public List<Project> Projects { get; set; }

        [DataMember(Name = "shared_projects")]
        public List<object> SharedProjects { get; set; }

        [DataMember(Name = "ldap_cn")]
        public object LdapCn { get; set; }

        [DataMember(Name = "ldap_access")]
        public object LdapAccess { get; set; }

        [DataMember(Name = "shared_runners_minutes_limit")]
        public object SharedRunnersMinutesLimit { get; set; }

        #endregion

        [IgnoreDataMember]
        public List<Group> SubGroups { get; set; }

        [IgnoreDataMember]
        public List<Project> Projects { get; set; }

        [IgnoreDataMember]
        public List<User> Members { get; set; }
    }
}
