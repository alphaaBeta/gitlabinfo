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
    public class Project
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "name_with_namespace")]
        public string NameWithNamespace { get; set; }

        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "path_with_namespace")]
        public string PathWithNamespace { get; set; }

        [DataMember(Name = "created_at")]
        public string CreatedAt { get; set; }

        [DataMember(Name = "default_branch")]
        public object DefaultBranch { get; set; }

        [DataMember(Name = "tag_list")]
        public List<object> TagList { get; set; }

        [DataMember(Name = "ssh_url_to_repo")]
        public string SshUrlToRepo { get; set; }

        [DataMember(Name = "http_url_to_repo")]
        public Uri HttpUrlToRepo { get; set; }

        [DataMember(Name = "web_url")]
        public Uri WebUrl { get; set; }

        [DataMember(Name = "readme_url")]
        public object ReadmeUrl { get; set; }

        [DataMember(Name = "avatar_url")]
        public object AvatarUrl { get; set; }

        [DataMember(Name = "star_count")]
        public int StarCount { get; set; }

        [DataMember(Name = "forks_count")]
        public int ForksCount { get; set; }

        [DataMember(Name = "last_activity_at")]
        public string LastActivityAt { get; set; }

        ////[DataMember(Name = "namespace")]
        ////public Namespace Namespace { get; set; }

        ////[DataMember(Name = "_links")]
        ////public Links Links { get; set; }

        [DataMember(Name = "archived")]
        public bool Archived { get; set; }

        [DataMember(Name = "visibility")]
        public string Visibility { get; set; }

        [DataMember(Name = "resolve_outdated_diff_discussions")]
        public bool ResolveOutdatedDiffDiscussions { get; set; }

        [DataMember(Name = "container_registry_enabled")]
        public bool ContainerRegistryEnabled { get; set; }

        [DataMember(Name = "issues_enabled")]
        public bool IssuesEnabled { get; set; }

        [DataMember(Name = "merge_requests_enabled")]
        public bool MergeRequestsEnabled { get; set; }

        [DataMember(Name = "wiki_enabled")]
        public bool WikiEnabled { get; set; }

        [DataMember(Name = "jobs_enabled")]
        public bool JobsEnabled { get; set; }

        [DataMember(Name = "snippets_enabled")]
        public bool SnippetsEnabled { get; set; }

        [DataMember(Name = "shared_runners_enabled")]
        public bool SharedRunnersEnabled { get; set; }

        [DataMember(Name = "lfs_enabled")]
        public bool LfsEnabled { get; set; }

        [DataMember(Name = "creator_id")]
        public int CreatorId { get; set; }

        [DataMember(Name = "import_status")]
        public string ImportStatus { get; set; }

        [DataMember(Name = "import_error")]
        public object ImportError { get; set; }

        [DataMember(Name = "open_issues_count")]
        public int OpenIssuesCount { get; set; }

        [DataMember(Name = "runners_token")]
        public string RunnersToken { get; set; }

        [DataMember(Name = "public_jobs")]
        public bool PublicJobs { get; set; }

        [DataMember(Name = "ci_config_path")]
        public object CiConfigPath { get; set; }

        [DataMember(Name = "shared_with_groups")]
        public List<object> SharedWithGroups { get; set; }

        [DataMember(Name = "only_allow_merge_if_pipeline_succeeds")]
        public bool OnlyAllowMergeIfPipelineSucceeds { get; set; }

        [DataMember(Name = "request_access_enabled")]
        public bool RequestAccessEnabled { get; set; }

        [DataMember(Name = "only_allow_merge_if_all_discussions_are_resolved")]
        public bool OnlyAllowMergeIfAllDiscussionsAreResolved { get; set; }

        [DataMember(Name = "printing_merge_request_link_enabled")]
        public bool PrintingMergeRequestLinkEnabled { get; set; }

        [DataMember(Name = "merge_method")]
        public string MergeMethod { get; set; }

        [DataMember(Name = "mirror")]
        public bool Mirror { get; set; }

        [DataMember(Name = "external_authorization_classification_label")]
        public string ExternalAuthorizationClassificationLabel { get; set; }
    }
}
