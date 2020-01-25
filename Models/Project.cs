using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GitlabInfo.Models
{
    public class Project
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("name_with_namespace")]
        public string NameWithNamespace { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("path_with_namespace")]
        public string PathWithNamespace { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("default_branch")]
        public object DefaultBranch { get; set; }

        [JsonPropertyName("tag_list")]
        public List<object> TagList { get; set; }

        [JsonPropertyName("ssh_url_to_repo")]
        public string SshUrlToRepo { get; set; }

        [JsonPropertyName("http_url_to_repo")]
        public Uri HttpUrlToRepo { get; set; }

        [JsonPropertyName("web_url")]
        public Uri WebUrl { get; set; }

        [JsonPropertyName("readme_url")]
        public object ReadmeUrl { get; set; }

        [JsonPropertyName("avatar_url")]
        public object AvatarUrl { get; set; }

        [JsonPropertyName("star_count")]
        public int StarCount { get; set; }

        [JsonPropertyName("forks_count")]
        public int ForksCount { get; set; }

        [JsonPropertyName("last_activity_at")]
        public string LastActivityAt { get; set; }

        ////[JsonPropertyName("namespace")]
        ////public Namespace Namespace { get; set; }
        
        [JsonIgnore]
        public int NamespaceId { get; set; }

        ////[JsonPropertyName("_links")]
        ////public Links Links { get; set; }

        [JsonPropertyName("archived")]
        public bool Archived { get; set; }

        [JsonPropertyName("visibility")]
        public string Visibility { get; set; }

        [JsonPropertyName("resolve_outdated_diff_discussions")]
        public bool ResolveOutdatedDiffDiscussions { get; set; }

        [JsonPropertyName("container_registry_enabled")]
        public bool ContainerRegistryEnabled { get; set; }

        [JsonPropertyName("issues_enabled")]
        public bool IssuesEnabled { get; set; }

        [JsonPropertyName("merge_requests_enabled")]
        public bool MergeRequestsEnabled { get; set; }

        [JsonPropertyName("wiki_enabled")]
        public bool WikiEnabled { get; set; }

        [JsonPropertyName("jobs_enabled")]
        public bool JobsEnabled { get; set; }

        [JsonPropertyName("snippets_enabled")]
        public bool SnippetsEnabled { get; set; }

        [JsonPropertyName("shared_runners_enabled")]
        public bool SharedRunnersEnabled { get; set; }

        [JsonPropertyName("lfs_enabled")]
        public bool LfsEnabled { get; set; }

        [JsonPropertyName("creator_id")]
        public int CreatorId { get; set; }

        [JsonPropertyName("import_status")]
        public string ImportStatus { get; set; }

        [JsonPropertyName("import_error")]
        public object ImportError { get; set; }

        [JsonPropertyName("open_issues_count")]
        public int OpenIssuesCount { get; set; }

        [JsonPropertyName("runners_token")]
        public string RunnersToken { get; set; }

        [JsonPropertyName("public_jobs")]
        public bool PublicJobs { get; set; }

        [JsonPropertyName("ci_config_path")]
        public object CiConfigPath { get; set; }

        [JsonPropertyName("shared_with_groups")]
        public List<object> SharedWithGroups { get; set; }

        [JsonPropertyName("only_allow_merge_if_pipeline_succeeds")]
        public bool OnlyAllowMergeIfPipelineSucceeds { get; set; }

        [JsonPropertyName("request_access_enabled")]
        public bool RequestAccessEnabled { get; set; }

        [JsonPropertyName("only_allow_merge_if_all_discussions_are_resolved")]
        public bool OnlyAllowMergeIfAllDiscussionsAreResolved { get; set; }

        [JsonPropertyName("printing_merge_request_link_enabled")]
        public bool PrintingMergeRequestLinkEnabled { get; set; }

        [JsonPropertyName("merge_method")]
        public string MergeMethod { get; set; }

        [JsonPropertyName("mirror")]
        public bool Mirror { get; set; }

        [JsonPropertyName("external_authorization_classification_label")]
        public string ExternalAuthorizationClassificationLabel { get; set; }
    }
}
