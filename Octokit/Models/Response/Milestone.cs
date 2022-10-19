using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Milestone
    {
        public Milestone() { }

        public Milestone(int number)
        {
            Number = number;
        }

        public Milestone(string title)
        {
            Title = title;
        }

        public Milestone(string url, string htmlUrl, long id, int number, string nodeId, ItemState state, string title, string description, User creator, int openIssues, int closedIssues, DateTimeOffset createdAt, DateTimeOffset? dueOn, DateTimeOffset? closedAt, DateTimeOffset? updatedAt)
        {
            Url = url;
            HtmlUrl = htmlUrl;
            Id = id;
            Number = number;
            NodeId = nodeId;
            State = state;
            Title = title;
            Description = description;
            Creator = creator;
            OpenIssues = openIssues;
            ClosedIssues = closedIssues;
            CreatedAt = createdAt;
            DueOn = dueOn;
            ClosedAt = closedAt;
            UpdatedAt = updatedAt;
        }

        /// <summary>
        /// The URL for this milestone.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The Html page for this milestone.
        /// </summary>
        public string HtmlUrl { get; private set; }

        /// <summary>
        /// The ID for this milestone.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// The milestone number.
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// Whether the milestone is open or closed.
        /// </summary>
        public StringEnum<ItemState> State { get; private set; }

        /// <summary>
        /// Title of the milestone.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Optional description for the milestone.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The user that created this milestone.
        /// </summary>
        public User Creator { get; private set; }

        /// <summary>
        /// The number of open issues in this milestone.
        /// </summary>
        public int OpenIssues { get; private set; }

        /// <summary>
        /// The number of closed issues in this milestone.
        /// </summary>
        public int ClosedIssues { get; private set; }

        /// <summary>
        /// The date this milestone was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// The date, if any, when this milestone is due.
        /// </summary>
        public DateTimeOffset? DueOn { get; private set; }

        /// <summary>
        /// The date, if any, when this milestone was closed.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; private set; }

        /// <summary>
        /// The date, if any, when this milestone was updated.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Title {0} ", Title); }
        }
    }
}
