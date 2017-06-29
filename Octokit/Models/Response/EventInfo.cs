using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EventInfo
    {
        public EventInfo() { }

        public EventInfo(int id, string url, User actor, User assignee, Label label, EventInfoState @event, string commitId, DateTimeOffset createdAt)
        {
            Id = id;
            Url = url;
            Actor = actor;
            Assignee = assignee;
            Label = label;
            Event = @event;
            CommitId = commitId;
            CreatedAt = createdAt;
        }

        /// <summary>
        /// The id of the issue/pull request event.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The URL for this event.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// Always the User that generated the event.
        /// </summary>
        public User Actor { get; protected set; }

        /// <summary>
        /// The user that was assigned, if the event was 'Assigned'.
        /// </summary>
        public User Assignee { get; protected set; }

        /// <summary>
        /// The label that was assigned, if the event was 'Labeled'
        /// </summary>
        public Label Label { get; protected set; }

        /// <summary>
        /// Identifies the actual type of Event that occurred.
        /// </summary>
        public StringEnum<EventInfoState> Event { get; protected set; }

        /// <summary>
        /// The String SHA of a commit that referenced this Issue.
        /// </summary>
        public string CommitId { get; protected set; }

        /// <summary>
        /// Date the event occurred for the issue/pull request.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Id: {0} CreatedAt: {1}", Id, CreatedAt);
            }
        }
    }

    public enum EventInfoState
    {
        /// <summary>
        /// The issue was closed by the actor. When the commit_id is present, it identifies the commit that 
        /// closed the issue using “closes / fixes #NN” syntax.
        /// </summary>
        [Parameter(Value = "closed")]
        Closed,

        /// <summary>
        /// The issue was reopened by the actor.
        /// </summary>
        [Parameter(Value = "reopened")]
        Reopened,

        /// <summary>
        /// The actor subscribed to receive notifications for an issue.
        /// </summary>
        [Parameter(Value = "subscribed")]
        Subscribed,

        /// <summary>
        /// The issue was merged by the actor. The commit_id attribute is the SHA1 of the HEAD commit that was merged.
        /// </summary>
        [Parameter(Value = "merged")]
        Merged,

        /// <summary>
        /// The issue was referenced from a commit message. The commit_id attribute is the commit SHA1 of where 
        /// that happened.
        /// </summary>
        [Parameter(Value = "referenced")]
        Referenced,

        /// <summary>
        /// The actor was @mentioned in an issue body.
        /// </summary>
        [Parameter(Value = "mentioned")]
        Mentioned,

        /// <summary>
        /// The issue was assigned to the actor.
        /// </summary>
        [Parameter(Value = "assigned")]
        Assigned,

        /// <summary>
        /// The issue was unassigned to the actor.
        /// </summary>
        [Parameter(Value = "unassigned")]
        Unassigned,

        /// <summary>
        /// A label was added to the issue.
        /// </summary>
        [Parameter(Value = "labeled")]
        Labeled,

        /// <summary>
        /// A label was removed from the issue.
        /// </summary>
        [Parameter(Value = "unlabeled")]
        Unlabeled,

        /// <summary>
        /// The issue was added to a milestone.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Milestoned")]
        [Parameter(Value = "milestoned")]
        Milestoned,

        /// <summary>
        /// The issue was removed from a milestone.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Demilestoned")]
        [Parameter(Value = "demilestoned")]
        Demilestoned,

        /// <summary>
        /// The issue title was changed.
        /// </summary>
        [Parameter(Value = "renamed")]
        Renamed,

        /// <summary>
        /// The issue was locked by the actor.
        /// </summary>
        [Parameter(Value = "locked")]
        Locked,

        /// <summary>
        /// The issue was unlocked by the actor.
        /// </summary>
        [Parameter(Value = "unlocked")]
        Unlocked,

        /// <summary>
        /// The pull request’s branch was deleted.
        /// </summary>
        [Parameter(Value = "head_ref_deleted")]
        HeadRefDeleted,

        /// <summary>
        /// The pull request’s branch was restored.
        /// </summary>
        [Parameter(Value = "head_ref_restored")]
        HeadRefRestored,

        /// <summary>
        /// The actor dismissed a review from the pull request.
        /// </summary>
        [Parameter(Value = "review_dismissed")]
        ReviewDismissed,

        /// <summary>
        /// The actor requested review from the subject on this pull request.
        /// </summary>
        [Parameter(Value = "review_requested")]
        ReviewRequested,

        /// <summary>
        /// The actor removed the review request for the subject on this pull request.
        /// </summary>
        [Parameter(Value = "review_request_removed")]
        ReviewRequestRemoved,

        /// <summary>
        /// The issue was added to a project board.
        /// </summary>
        [Parameter(Value = "added_to_project")]
        AddedToProject,

        /// <summary>
        /// The issue was moved between columns in a project board.
        /// </summary>
        [Parameter(Value = "moved_columns_in_project")]
        MovedColumnsInProject,

        /// <summary>
        /// The issue was removed from a project board.
        /// </summary>
        [Parameter(Value = "removed_from_project")]
        RemovedFromProject,

        /// <summary>
        /// The issue was created by converting a note in a project board to an issue.
        /// </summary>
        [Parameter(Value = "converted_note_to_issue")]
        ConvertedNoteToIssue,

        /// <summary>
        /// The actor unsubscribed from notifications for an issue.
        /// </summary>
        [Parameter(Value = "unsubscribed")]
        Unsubscribed,

        /// <summary>
        /// A comment was added to the issue.
        /// </summary>
        [Parameter(Value = "commented")]
        Commented,

        /// <summary>
        /// A commit was added to the pull request's HEAD branch.
        /// Only provided for pull requests.
        /// </summary>
        [Parameter(Value = "committed")]
        Committed,

        /// <summary>
        /// Base branch of the pull request was changed.
        /// </summary>
        [Parameter(Value = "base_ref_changed")]
        BaseRefChanged,

        /// <summary>
        /// The issue was referenced from another issue.
        /// The source attribute contains the id, actor, and
        /// url of the reference's source.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Crossreferenced")]
        [Parameter(Value = "cross-referenced")]
        Crossreferenced,

        /// <summary>
        /// The issue was reveiewed.
        /// </summary>
        [Parameter(Value = "reviewed")]
        Reviewed,

        /// <summary>
        /// A line comment was made.
        /// </summary>
        [Parameter(Value = "line-commented")]
        LineCommented,

        /// <summary>
        /// A commit comment was made.
        /// </summary>
        [Parameter(Value = "commit-commented")]
        CommitCommented
    }
}
