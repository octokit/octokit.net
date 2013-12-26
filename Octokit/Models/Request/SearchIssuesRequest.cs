using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Searching Issues
    /// </summary>
    public class SearchIssuesRequest : RequestParameters
    {
        public SearchIssuesRequest(string term)
        {
            Ensure.ArgumentNotNullOrEmptyString(term,"term");
            Term = term;
            Page = 1;
            PerPage = 100;
            Order = SortDirection.Descending;
        }

        /// <summary>
        /// The search terms. This can be any combination of the supported repository search parameters:
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        [Parameter(Key= "q")]
        public string Term { get; private set; }

        /// <summary>
        /// For http://developer.github.com/v3/search/#search-issues
        /// Optional Sort field. One of comments, created, or updated. If not provided, results are sorted by best match.
        /// </summary>
        //public string Sort { get; set; } //re-add laters

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
    }
}
