using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class IssueRequest : RequestParameters
    {
        public IssueRequest()
        {
            Filter = IssueFilter.Assigned;
            State = ItemState.Open;
            Labels = new Collection<string>();
            SortProperty = IssueSort.Created;
            SortDirection = SortDirection.Descending;
        }

        public IssueFilter Filter { get; set; }
        public ItemState State { get; set; }
        public Collection<string> Labels { get; private set; }
        [Parameter(Key = "sort")]
        public IssueSort SortProperty { get; set; }
        [Parameter(Key = "direction")]
        public SortDirection SortDirection { get; set; }
        public DateTimeOffset? Since { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Filter: {0} State: {1}", Filter, State);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>http://developer.github.com/v3/issues/#list-issues</remarks>
    public enum IssueFilter
    {
        /// <summary>
        /// Issues assigned to the authenticated user. (Default)
        /// </summary>
        Assigned,

        /// <summary>
        /// Issues created by the authenticated user.
        /// </summary>
        Created,

        /// <summary>
        /// Issues mentioning the authenticated user.
        /// </summary>
        Mentioned,

        /// <summary>
        /// Issues the authenticated user is subscribed to for updates.
        /// </summary>
        Subscribed,

        /// <summary>
        /// All issues the authenticated user can see, regardless of participation or creation.
        /// </summary>
        All
    }

    public enum ItemState
    {
        /// <summary>
        /// Isuses that are open (default).
        /// </summary>
        Open,

        /// <summary>
        /// Isuses that are closed.
        /// </summary>
        Closed
    }

    public enum IssueSort
    {
        /// <summary>
        /// Sort by create date (default)
        /// </summary>
        Created,

        /// <summary>
        /// Sort by the date of the last update
        /// </summary>
        Updated,

        /// <summary>
        /// Sort by the number of comments
        /// </summary>
        Comments
    }

    public enum SortDirection
    {
        [Parameter(Value = "asc")]
        Ascending,

        [Parameter(Value = "desc")]
        Descending
    }
}
