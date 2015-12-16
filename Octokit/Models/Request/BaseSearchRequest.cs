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
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSearchRequest"/> class.
        /// </summary>
        protected BaseSearchRequest()
        {
            Page = 1;
            PerPage = 100;
            Order = SortDirection.Descending;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSearchRequest"/> class.
        /// </summary>
        /// <param name="term">The term.</param>
        protected BaseSearchRequest(string term) : this()
        {
            Ensure.ArgumentNotNullOrEmptyString(term, "term");
            Term = term;
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

        /// <summary>
        /// Gets the sort order as a properly formatted lowercased query string parameter.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
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
        public abstract IReadOnlyList<string> MergedQualifiers();

        /// <summary>
        /// Add qualifiers onto the search term
        /// </summary>
        private string TermAndQualifiers
        {
            get
            {
                var mergedParameters = string.Join("+", MergedQualifiers());
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
                var d = new Dictionary<string, string>
                {
                    { "page", Page.ToString(CultureInfo.CurrentCulture) }
                    , { "per_page", PerPage.ToString(CultureInfo.CurrentCulture) }
                    , { "order", SortOrder }
                    , { "q", TermAndQualifiers }
                };
                if (!string.IsNullOrWhiteSpace(Sort))
                {
                    d.Add("sort", Sort);
                }
                return d;
            }
        }
    }
}