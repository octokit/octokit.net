using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    public class IssueRequest
    {
        static readonly IssueRequest _defaultParameterValues = new IssueRequest();
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
        public IssueSort SortProperty { get; set; }
        public SortDirection SortDirection { get; set; }
        public DateTimeOffset? Since { get; set; }

        /// <summary>
        /// Returns a dictionary of query string parameters that represent this request. Only values that
        /// do not have default values are in the dictionary. If everything is default, this returns an
        /// empty dictionary.
        /// </summary>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase",
            Justification = "The API expects lowercase")]
        public virtual IDictionary<string, string> ToParametersDictionary()
        {
            var parameters = new Dictionary<string, string>();

            if (Filter != _defaultParameterValues.Filter)
            {
                parameters.Add("filter", Enum.GetName(typeof(IssueFilter), Filter).ToLowerInvariant());
            }

            if (State != _defaultParameterValues.State)
            {
                parameters.Add("state", Enum.GetName(typeof(ItemState), State).ToLowerInvariant());
            }

            if (SortProperty != _defaultParameterValues.SortProperty)
            {
                parameters.Add("sort", Enum.GetName(typeof(IssueSort), SortProperty).ToLowerInvariant());
            }

            if (SortDirection != _defaultParameterValues.SortDirection)
            {
                parameters.Add("direction", "asc");
            }

            if (Since != null)
            {
                parameters.Add("since", Since.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture));
            }
            if (Labels.Count > 0)
            {
                parameters.Add("labels", String.Join(",", Labels));
            }

            return parameters;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>http://developer.github.com/v3/issues/#list-issues</remarks>
    public enum IssueFilter
    {
        /// <summary>
        /// Issues assigned to the authenticated user.
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
        Created,
        Updated,
        Comments
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }
}
