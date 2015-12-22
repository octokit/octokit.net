using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class IssueEvent
    {
        public IssueEvent() { }

        public IssueEvent(int id, Uri url, User actor, User assignee, Label label, EventInfoState @event, string commitId, DateTimeOffset createdAt, Issue issue, Uri commitUrl)
        {
            Id = id;
            Url = url;
            Actor = actor;
            Assignee = assignee;
            Label = label;
            Event = @event;
            CommitId = commitId;
            CreatedAt = createdAt;
            Issue = issue;
            CommitUrl = commitUrl;
        }

        /// <summary>
        /// The id of the issue/pull request event.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The URL for this issue/pull request event.
        /// </summary>
        public Uri Url { get; protected set; }

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
        public EventInfoState Event { get; protected set; }

        /// <summary>
        /// The String SHA of a commit that referenced this Issue.
        /// </summary>
        public string CommitId { get; protected set; }

        /// <summary>
        /// The commit URL of a commit that referenced this issue.
        /// </summary>
        public Uri CommitUrl { get; protected set; }

        /// <summary>
        /// Date the event occurred for the issue/pull request.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// The issue associated to this event.
        /// </summary>
        public Issue Issue { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0} CreatedAt: {1}", Id, CreatedAt); }
        }
    }
}
