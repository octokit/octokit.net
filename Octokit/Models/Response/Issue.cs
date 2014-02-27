using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Issue
    {
        /// <summary>
        /// The URL for this milestone.
        /// </summary>
        public Uri Url { get; set; }
        public Uri HtmlUrl { get; set; }

        /// <summary>
        /// The issue number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Whether the issue is open or closed.
        /// </summary>
        public ItemState State { get; set; }

        /// <summary>
        /// Title of the issue
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Details about the issue.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The user that created the issue.
        /// </summary>
        public User User { get; set; }
        
        /// <summary>
        /// The set of labels applied to the issue
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Label> Labels { get; set; }

        /// <summary>
        /// The user this issue is assigned to.
        /// </summary>
        public User Assignee { get; set; }

        /// <summary>
        /// The milestone, if any, that this issue is assigned to.
        /// </summary>
        public Milestone Milestone { get; set; }

        /// <summary>
        /// The number of comments on the issue.
        /// </summary>
        public int Comments { get; set; }

        public PullRequest PullRequest { get; set; }
        
        /// <summary>
        /// The date the issue was closed if closed.
        /// </summary>
        public DateTimeOffset? ClosedAt { get; set; }

        /// <summary>
        /// The date the issue was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The date the issue was last updated.
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Number: {0} State: {1}", Number, State);
            }
        }
    }
}
