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

        public EventInfo(long id, string nodeId, string url, User actor, User assignee, Label label, EventInfoState @event, string commitId, DateTimeOffset createdAt)
        {
            Id = id;
            NodeId = nodeId;
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
        public long Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// The URL for this event.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Always the User that generated the event.
        /// </summary>
        public User Actor { get; private set; }

        /// <summary>
        /// The user that was assigned, if the event was 'Assigned'.
        /// </summary>
        public User Assignee { get; private set; }

        /// <summary>
        /// The label that was assigned, if the event was 'Labeled'
        /// </summary>
        public Label Label { get; private set; }

        /// <summary>
        /// Identifies the actual type of Event that occurred.
        /// </summary>
        public StringEnum<EventInfoState> Event { get; private set; }

        /// <summary>
        /// The String SHA of a commit that referenced this Issue.
        /// </summary>
        public string CommitId { get; private set; }

        /// <summary>
        /// Date the event occurred for the issue/pull request.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

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
        /// The issue was added to a project board.
        /// </summary>
        [Parameter(Value = "added_to_project")]
        AddedToProject,

        /// <summary>
        /// The issue was assigned to the actor.
        /// </summary>
        [Parameter(Value = "assigned")]
        Assigned,

        /// <summary>
        /// GitHub unsuccessfully attempted to automatically change the base branch of the pull request.
        /// </summary>
        [Parameter(Value = "automatic_base_change_failed")]
        AutomaticBaseChangeFailed,

        /// <summary>
        /// GitHub successfully attempted to automatically change the base branch of the pull request.
        /// </summary>
        [Parameter(Value = "automatic_base_change_succeeded")]
        AutomaticBaseChangeSucceeded,

        /// <summary>
        /// The base reference branch of the pull request changed.
        /// </summary>
        [Parameter(Value = "base_ref_changed")]
        BaseRefChanged,

        /// <summary>
        /// The issue was closed by the actor. When the commit_id is present, it identifies the commit that
        /// closed the issue using “closes / fixes #NN” syntax.
        /// </summary>
        [Parameter(Value = "closed")]
        Closed,

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
        /// An issue was connected.
        /// </summary>
        [Parameter(Value = "connected")]
        Connected,

        /// <summary>
        /// The pull request was converted to draft mode.
        /// </summary>
        [Parameter(Value = "convert_to_draft")]
        ConvertToDraft,

        /// <summary>
        /// The issue was created by converting a note in a project board to an issue.
        /// </summary>
        [Parameter(Value = "converted_note_to_issue")]
        ConvertedNoteToIssue,

        /// <summary>
        /// The issue was referenced from another issue.
        /// The source attribute contains the id, actor, and
        /// url of the reference's source.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Crossreferenced")]
        [Parameter(Value = "cross-referenced")]
        Crossreferenced,

        /// <summary>
        /// The issue was removed from a milestone.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Demilestoned")]
        [Parameter(Value = "demilestoned")]
        Demilestoned,

        /// <summary>
        /// The pull request was deployed.
        /// </summary>
        [Parameter(Value = "deployed")]
        Deployed,

        /// <summary>
        /// The pull request deployment environment was changed.
        /// </summary>
        [Parameter(Value = "deployment_environment_changed")]
        DeploymentEnvironmentChanged,

        /// <summary>
        /// The issue or pull request was unlinked from another issue or pull request.
        /// </summary>
        [Parameter(Value = "disconnected")]
        Disconnected,

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
        /// The pull request’s branch was force pushed to.
        /// </summary>
        [Parameter(Value = "head_ref_force_pushed")]
        HeadRefForcePushed,

        /// <summary>
        /// A label was added to the issue.
        /// </summary>
        [Parameter(Value = "labeled")]
        Labeled,

        /// <summary>
        /// The issue was locked by the actor.
        /// </summary>
        [Parameter(Value = "locked")]
        Locked,

        /// <summary>
        /// The actor was @mentioned in an issue body.
        /// </summary>
        [Parameter(Value = "mentioned")]
        Mentioned,

        /// <summary>
        /// A user with write permissions marked an issue as a duplicate of another issue or a pull request as a duplicate of another pull request.
        /// </summary>
        [Parameter(Value = "marked_as_duplicate")]
        MarkedAsDuplicate,

        /// <summary>
        /// The issue was merged by the actor. The commit_id attribute is the SHA1 of the HEAD commit that was merged.
        /// </summary>
        [Parameter(Value = "merged")]
        Merged,

        /// <summary>
        /// The issue was added to a milestone.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Milestoned")]
        [Parameter(Value = "milestoned")]
        Milestoned,

        /// <summary>
        /// The issue was moved between columns in a project board.
        /// </summary>
        [Parameter(Value = "moved_columns_in_project")]
        MovedColumnsInProject,

        /// <summary>
        /// An issue was pinned.
        /// </summary>
        [Parameter(Value = "pinned")]
        Pinned,

        /// <summary>
        /// The pull request is ready for review
        /// </summary>
        [Parameter(Value = "ready_for_review")]
        ReadyForReview,

        /// <summary>
        /// The issue was referenced from a commit message. The commit_id attribute is the commit SHA1 of where
        /// that happened.
        /// </summary>
        [Parameter(Value = "referenced")]
        Referenced,

        /// <summary>
        /// The issue was removed from a project board.
        /// </summary>
        [Parameter(Value = "removed_from_project")]
        RemovedFromProject,

        /// <summary>
        /// The issue title was changed.
        /// </summary>
        [Parameter(Value = "renamed")]
        Renamed,

        /// <summary>
        /// The issue was reopened by the actor.
        /// </summary>
        [Parameter(Value = "reopened")]
        Reopened,

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
        /// The issue was reviewed.
        /// </summary>
        [Parameter(Value = "reviewed")]
        Reviewed,

        /// <summary>
        /// The actor subscribed to receive notifications for an issue.
        /// </summary>
        [Parameter(Value = "subscribed")]
        Subscribed,

        /// <summary>
        /// An issue was transferred.
        /// </summary>
        [Parameter(Value = "transferred")]
        Transferred,

        /// <summary>
        /// The issue was unassigned to the actor.
        /// </summary>
        [Parameter(Value = "unassigned")]
        Unassigned,

        /// <summary>
        /// A label was removed from the issue.
        /// </summary>
        [Parameter(Value = "unlabeled")]
        Unlabeled,

        /// <summary>
        /// The issue was unlocked by the actor.
        /// </summary>
        [Parameter(Value = "unlocked")]
        Unlocked,

        /// <summary>
        /// An issue that a user had previously marked as a duplicate of another issue is no longer considered a duplicate.
        /// </summary>
        [Parameter(Value = "unmarked_as_duplicate")]
        UnmarkedAsDuplicate,

        /// <summary>
        /// An issue was unpinned.
        /// </summary>
        [Parameter(Value = "unpinned")]
        Unpinned,

        /// <summary>
        /// The actor unsubscribed from notifications for an issue.
        /// </summary>
        [Parameter(Value = "unsubscribed")]
        Unsubscribed,

        /// <summary>
        /// An organization owner blocked a user from the organization.
        /// </summary>
        [Parameter(Value = "user_blocked")]
        UserBlocked,










        /// <summary>
        /// A line comment was made.
        /// </summary>
        [Parameter(Value = "line-commented")]
        LineCommented,

        /// <summary>
        /// A commit comment was made.
        /// </summary>
        [Parameter(Value = "commit-commented")]
        CommitCommented,



        /// <summary>
        /// An issue comment was deleted.
        /// </summary>
        [Parameter(Value = "comment_deleted")]
        CommentDeleted,




    }
}
