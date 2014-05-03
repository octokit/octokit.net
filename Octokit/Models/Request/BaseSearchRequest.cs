using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Base class for searching issues/code/users/repos
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1012:AbstractTypesShouldNotHaveConstructors")]
    public abstract class BaseSearchRequest
    {
        public BaseSearchRequest(string term)
        {
            Ensure.ArgumentNotNullOrEmptyString(term, "term");
            Term = term;
            Page = 1;
            PerPage = 100;
            Order = SortDirection.Descending;
        }

        /// <summary>
        /// The search term
        /// </summary>
        public string Term { get; private set; }

        /// <summary>
        /// The sort field
        /// </summary>
        public abstract string Sort
        {
            get;
        }

        private string SortOrder
        {
            get
            {
                return Order.ToParameter();
            }
        }

        /// <summary>
        /// Optional Sort order if sort parameter is provided. One of asc or desc; the default is desc.
        /// </summary>
        public SortDirection Order { get; set; }

        /// <summary>
        /// Page of paginated results
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// All qualifiers that are used for this search
        /// </summary>
        public abstract IReadOnlyCollection<string> MergedQualifiers();

        /// <summary>
        /// Add qualifiers onto the search term
        /// </summary>
        private string TermAndQualifiers
        {
            get
            {
                var mergedParameters = String.Join("+", MergedQualifiers());
                return Term + (mergedParameters.IsNotBlank() ? "+" + mergedParameters : "");
            }
        }

        /// <summary>
        /// Get the query parameters that will be appending onto the search
        /// </summary>
        public IDictionary<string, string> Parameters
        {
            get
            {
                var d = new Dictionary<string, string>();
                d.Add("page", Page.ToString(CultureInfo.CurrentCulture));
                d.Add("per_page", PerPage.ToString(CultureInfo.CurrentCulture));
                if (!String.IsNullOrWhiteSpace(Sort))
                {
                    d.Add("sort", Sort);
                }
                d.Add("order", SortOrder);
                d.Add("q", TermAndQualifiers);
                return d;
            }
        }
    }
}