using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Searching Code/Files
    /// http://developer.github.com/v3/search/#search-code
    /// </summary>
    public class SearchCodeRequest : RequestParameters
    {
        public SearchCodeRequest(string term)
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
        [Parameter(Key = "q")]
        public string Term { get; private set; }

        /// <summary>
        /// Optional Sort field. Can only be indexed, which indicates how recently a file has been indexed by the GitHub search infrastructure. If not provided, results are sorted by best match.
        /// </summary>
        //public string Sort { get; set; } //this will need to be re-added

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
        [Parameter(Key = "per_page")]
        public int PerPage { get; set; }
    }
}
