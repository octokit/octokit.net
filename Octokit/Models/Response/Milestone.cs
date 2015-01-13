using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Milestone
    {
        public Milestone()
        {
        }

        public Milestone(int number)
        {
            Number = number;
        }

        /// <summary>
        /// The URL for this milestone.
        /// </summary>
        public Uri Url { get; protected set; }

        /// <summary>
        /// The milestone number.
        /// </summary>
        public int Number { get; protected set; }

        /// <summary>
        /// Whether the milestone is open or closed.
        /// </summary>
        public ItemState State { get; protected set; }

        /// <summary>
        /// Title of the milestone
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// Optional description for the milestone.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// The user that created this milestone.
        /// </summary>
        public User Creator { get; protected set; }

        /// <summary>
        /// The number of open issues in this milestone.
        /// </summary>
        public int OpenIssues { get; protected set; }

        /// <summary>
        /// The number of closed issues in this milestone.
        /// </summary>
        public int ClosedIssues { get; protected set; }

        /// <summary>
        /// The date this milestone was created
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// The date, if any, when this milestone is due.
        /// </summary>
        public DateTimeOffset? DueOn { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return String.Format(CultureInfo.InvariantCulture, "Title {0} ", Title); }
        }
    }
}
