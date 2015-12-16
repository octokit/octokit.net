using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to filter a request to list issues.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class IssueRequest : RequestParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueRequest"/> class.
        /// </summary>
        public IssueRequest()
        {
            Filter = IssueFilter.Assigned;
            State = ItemState.Open;
            Labels = new Collection<string>();
            SortProperty = IssueSort.Created;
            SortDirection = SortDirection.Descending;
        }

        /// <summary>
        /// Gets or sets the <see cref="IssueFilter" /> which indicates which sorts of issues to return.
        /// </summary>
        /// <value>
        /// The filter.
        /// </value>
        public IssueFilter Filter { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ItemState"/> for the issues to return.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public ItemState State { get; set; }

        /// <summary>
        /// Gets the labels to filter by. Add labels to the collection to only request issues with those labels.
        /// </summary>
        /// <remarks>Sent as a comma separated list</remarks>
        /// <value>
        /// The labels.
        /// </value>
        public Collection<string> Labels { get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="IssueSort"/> property to sort the returned issues by.
        /// Combine this with <see cref="SortDirection"/> to specify sort direction.
        /// </summary>
        /// <value>
        /// The sort property.
        /// </value>
        [Parameter(Key = "sort")]
        public IssueSort SortProperty { get; set; }

        /// <summary>
        /// Gets or sets the sort direction.
        /// </summary>
        /// <value>
        /// The sort direction.
        /// </value>
        [Parameter(Key = "direction")]
        public SortDirection SortDirection { get; set; }

        /// <summary>
        /// Gets or sets the date for which only issues updated at or after this time are returned.
        /// </summary>
        /// <remarks>
        /// This is sent as a timestamp in ISO 8601 format: YYYY-MM-DDTHH:MM:SSZ.
        /// </remarks>
        /// <value>
        /// The since.
        /// </value>
        public DateTimeOffset? Since { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Filter: {0} State: {1}", Filter, State);
            }
        }
    }

    /// <summary>
    /// The range of filters available for issues.
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

    /// <summary>
    /// The range of states that an issue can be in.
    /// </summary>
    public enum ItemState
    {
        /// <summary>
        /// Isuses that are open (default).
        /// </summary>
        Open,

        /// <summary>
        /// Isuses that are closed.
        /// </summary>
        Closed,

        /// <summary>
        /// All the issues.
        /// </summary>
        All
    }

    /// <summary>
    /// The available properties to sort issues by.
    /// </summary>
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

    /// <summary>
    /// The two possible sort directions.
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// Sort ascending
        /// </summary>
        [Parameter(Value = "asc")]
        Ascending,

        /// <summary>
        /// Sort descending
        /// </summary>
        [Parameter(Value = "desc")]
        Descending
    }
}
