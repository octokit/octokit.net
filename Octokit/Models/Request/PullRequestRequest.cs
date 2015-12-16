using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to filter requests for lists of pull requests.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PullRequestRequest : RequestParameters
    {
        public PullRequestRequest()
        {
            State = ItemState.Open;
            SortProperty = PullRequestSort.Created;
            SortDirection = SortDirection.Descending;
        }

        /// <summary>
        /// "open" or "closed" to filter by state. Default is "open".
        /// </summary>
        public ItemState State { get; set; }

        /// <summary>
        /// Filter pulls by head user and branch name in the format of "user:ref-name".
        /// </summary>
        public string Head { get; set; }

        /// <summary>
        /// Filter pulls by base branch name.
        /// </summary>
        public string Base { get; set; }

        /// <summary>
        /// What property to sort pull requests by.
        /// </summary>
        [Parameter(Key = "sort")]
        public PullRequestSort SortProperty { get; set; }

        /// <summary>
        /// What direction to sort the pull requests.
        /// </summary>
        [Parameter(Key = "direction")]
        public SortDirection SortDirection { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Base: {0} ", Base);
            }
        }
    }

    public enum PullRequestSort
    {
        /// <summary>
        /// Sort by created date (default)
        /// </summary>
        Created,
        /// <summary>
        /// Sort by last updated date
        /// </summary>
        Updated,
        /// <summary>
        /// Sort by popularity (comment count)
        /// </summary>
        Popularity,
        /// <summary>
        /// Sort by age (filtering by pulls updated in the last month)
        /// </summary>
        [Parameter(Value = "long-running")]
        LongRunning
    }
}
