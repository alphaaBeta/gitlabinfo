using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GitlabInfo.Models
{
    [DataContract]
    public class Issue
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "iid")]
        public int Iid { get; set; }

        [DataMember(Name = "project_id")]
        public int ProjectId { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }

        [DataMember(Name = "created_at")]
        public string CreatedAt { get; set; }

        [DataMember(Name = "updated_at")]
        public string UpdatedAt { get; set; }

        [DataMember(Name = "closed_at")]
        public object ClosedAt { get; set; }

        [DataMember(Name = "closed_by")]
        public object ClosedBy { get; set; }

        [DataMember(Name = "labels")]
        public List<string> Labels { get; set; }

        [DataMember(Name = "milestone")]
        public object Milestone { get; set; }

        [DataMember(Name = "assignees")]
        public IEnumerable<Assignee> Assignees { get; set; }

        [DataMember(Name = "author")]
        public Assignee Author { get; set; }

        [DataMember(Name = "assignee")]
        public Assignee Assignee { get; set; }

        [DataMember(Name = "user_notes_count")]
        public int UserNotesCount { get; set; }

        [DataMember(Name = "merge_requests_count")]
        public int MergeRequestsCount { get; set; }

        [DataMember(Name = "upvotes")]
        public int Upvotes { get; set; }

        [DataMember(Name = "downvotes")]
        public int Downvotes { get; set; }

        [DataMember(Name = "due_date")]
        public object DueDate { get; set; }

        [DataMember(Name = "confidential")]
        public bool Confidential { get; set; }

        [DataMember(Name = "discussion_locked")]
        public object DiscussionLocked { get; set; }

        [DataMember(Name = "web_url")]
        public Uri WebUrl { get; set; }

        //[DataMember(Name = "time_stats")]
        //public TimeStats TimeStats { get; set; }
    }

    public class Assignee
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }

        [DataMember(Name = "avatar_url")]
        public Uri AvatarUrl { get; set; }

        [DataMember(Name = "web_url")]
        public Uri WebUrl { get; set; }
    }

    //public class TimeStats
    //{
    //    [DataMember(Name = "time_estimate")]
    //    public int TimeEstimate { get; set; }

    //    [DataMember(Name = "total_time_spent")]
    //    public int TotalTimeSpent { get; set; }

    //    [DataMember(Name = "human_time_estimate")]
    //    public object HumanTimeEstimate { get; set; }

    //    [DataMember(Name = "human_total_time_spent")]
    //    public object HumanTotalTimeSpent { get; set; }
    //}
}
