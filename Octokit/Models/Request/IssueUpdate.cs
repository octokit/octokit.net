using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class IssueUpdate
    {
        public IssueUpdate()
        {
            Labels = new Collection<string>();
        }

        /// <summary>
        /// Title of the milestone (required)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Details about the issue.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Login for the user that this issue should be assigned to.
        /// </summary>
        /// <remarks>
        /// Only users with push access can set the assignee for new issues. The assignee is silently dropped otherwise.
        /// </remarks>
        public string Assignee { get; set; }

        /// <summary>
        /// Milestone to associate this issue with.
        /// </summary>
        /// <remarks>
        /// Only users with push access can set the milestone for new issues. The milestone is silently dropped
        /// otherwise
        /// </remarks>
        public int? Milestone { get; set; }

        /// <summary>
        /// Labels to associate with this issue.
        /// </summary>
        /// <remarks>
        /// Only users with push access can set labels for new issues. Labels are silently dropped otherwise.
        /// </remarks>
        public Collection<string> Labels { get; private set; }

        /// <summary>
        /// Whether the issue is open or closed.
        /// </summary>
        public ItemState State { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Title: {0}",Title);
            }
        }
    }
}
