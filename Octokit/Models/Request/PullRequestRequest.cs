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
            State = ItemStateFilter.Open;
            SortProperty = PullRequestSort.Created;
            SortDirection = SortDirection.Descending;
        }

        /// <summary>
        /// Which PullRequests to get. The default is <see cref="ItemStateFilter.Open"/>
        /// </summary>
        public ItemStateFilter State { get; set; }

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
        [Parameter(Value = "created")]
        Created,

        /// <summary>
        /// Sort by last updated date
        /// </summary>
        [Parameter(Value = "updated")]
        Updated,

        /// <summary>
        /// Sort by popularity (comment count)
        /// </summary>
        [Parameter(Value = "popularity")]
        Popularity,

        /// <summary>
        /// Sort by age (filtering by pulls updated in the last month)
        /// </summary>
        [Parameter(Value = "long-running")]
        LongRunning
    }
}
