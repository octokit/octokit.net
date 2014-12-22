using System;
using System.Diagnostics;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class EventInfo
    {
        /// <summary>
        /// The id of the issue/pull request event.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The URL for this event.
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// Always the User that generated the event.
        /// </summary>
        public User Actor { get; set; }

        /// <summary>
        /// The user that was assigned, if the event was 'Assigned'.
        /// </summary>
        public User Assignee { get; set; }

        /// <summary>
        /// The label that was assigned, if the event was 'Labeled'
        /// </summary>
        public Label Label { get; set; }

        /// <summary>
        /// Identifies the actual type of Event that occurred.
        /// </summary>
        public EventInfoState Event { get; set; }

        /// <summary>
        /// The String SHA of a commit that referenced this Issue.
        /// </summary>
        public string CommitId { get; set; }

        /// <summary>
        /// Date the event occurred for the issue/pull request.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Id: {0} CreatedAt: {1}", Id, CreatedAt);
            }
        }
    }

    public enum EventInfoState
    {
        /// <summary>
        /// The issue was closed by the actor. When the commit_id is present, it identifies the commit that 
        /// closed the issue using “closes / fixes #NN” syntax.
        /// </summary>
        Closed,

        /// <summary>
        /// The issue was reopened by the actor.
        /// </summary>
        Reopened,

        /// <summary>
        /// The actor subscribed to receive notifications for an issue.
        /// </summary>
        Subscribed,

        /// <summary>
        /// The issue was merged by the actor. The commit_id attribute is the SHA1 of the HEAD commit that was merged.
        /// </summary>
        Merged,

        /// <summary>
        /// The issue was referenced from a commit message. The commit_id attribute is the commit SHA1 of where 
        /// that happened.
        /// </summary>
        Referenced,

        /// <summary>
        /// The actor was @mentioned in an issue body.
        /// </summary>
        Mentioned,

        /// <summary>
        /// The issue was assigned to the actor.
        /// </summary>
        Assigned,

        /// <summary>
        /// The issue was unassigned to the actor.
        /// </summary>
        Unassigned,

        /// <summary>
        /// A label was added to the issue.
        /// </summary>
        Labeled,

        /// <summary>
        /// A label was removed from the issue.
        /// </summary>
        Unlabeled,

        /// <summary>
        /// The issue was added to a milestone.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Milestoned")]
        Milestoned,

        /// <summary>
        /// The issue was removed from a milestone.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Demilestoned")]
        Demilestoned,

        /// <summary>
        /// The issue title was changed.
        /// </summary>
        Renamed,

        /// <summary>
        /// The issue was locked by the actor.
        /// </summary>
        Locked,

        /// <summary>
        /// The issue was unlocked by the actor.
        /// </summary>
        Unlocked,

        /// <summary>
        /// The pull request’s branch was deleted.
        /// </summary>
        HeadRefDeleted,

        /// <summary>
        /// The pull request’s branch was restored.
        /// </summary>
        HeadRefRestored,
    }
}