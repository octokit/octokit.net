using System;
using System.Diagnostics;
using System.Globalization;

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
        /// Identifies the actual type of Event that occurred.
        /// </summary>
        public EventInfoState InfoState { get; set; }

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
        Assigned
    }
}