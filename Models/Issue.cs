using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GitlabInfo.Models
{
    public class Issue
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("iid")]
        public int Iid { get; set; }

        [JsonPropertyName("project_id")]
        public int ProjectId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonPropertyName("closed_at")]
        public object ClosedAt { get; set; }

        [JsonPropertyName("closed_by")]
        public object ClosedBy { get; set; }

        [JsonPropertyName("labels")]
        public List<string> Labels { get; set; }

        [JsonPropertyName("milestone")]
        public object Milestone { get; set; }

        [JsonPropertyName("assignees")]
        public IEnumerable<Assignee> Assignees { get; set; }

        [JsonPropertyName("author")]
        public Assignee Author { get; set; }

        [JsonPropertyName("assignee")]
        public Assignee Assignee { get; set; }

        [JsonPropertyName("user_notes_count")]
        public int UserNotesCount { get; set; }

        [JsonPropertyName("merge_requests_count")]
        public int MergeRequestsCount { get; set; }

        [JsonPropertyName("upvotes")]
        public int Upvotes { get; set; }

        [JsonPropertyName("downvotes")]
        public int Downvotes { get; set; }

        [JsonPropertyName("due_date")]
        public object DueDate { get; set; }

        [JsonPropertyName("confidential")]
        public bool Confidential { get; set; }

        [JsonPropertyName("discussion_locked")]
        public object DiscussionLocked { get; set; }

        [JsonPropertyName("web_url")]
        public Uri WebUrl { get; set; }

        //[JsonPropertyName("time_stats")]
        //public TimeStats TimeStats { get; set; }
    }

    public class Assignee
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("avatar_url")]
        public Uri AvatarUrl { get; set; }

        [JsonPropertyName("web_url")]
        public Uri WebUrl { get; set; }
    }

    //public class TimeStats
    //{
    //    [JsonPropertyName("time_estimate")]
    //    public int TimeEstimate { get; set; }

    //    [JsonPropertyName("total_time_spent")]
    //    public int TotalTimeSpent { get; set; }

    //    [JsonPropertyName("human_time_estimate")]
    //    public object HumanTimeEstimate { get; set; }

    //    [JsonPropertyName("human_total_time_spent")]
    //    public object HumanTotalTimeSpent { get; set; }
    //}
}
