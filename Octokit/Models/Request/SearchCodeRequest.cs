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
    public class SearchCodeRequest
    {
        public SearchCodeRequest(string term)
        {
            Term = term;
            Page = 1;
            PerPage = 100;
        }

        /// <summary>
        /// The search term
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// Optional Sort field. Can only be indexed, which indicates how recently a file has been indexed by the GitHub search infrastructure. If not provided, results are sorted by best match.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// Optional Sort order if sort parameter is provided. One of asc or desc; the default is desc.
        /// </summary>
        public SortDirection? Order { get; set; }

        /// <summary>
        /// Page of paginated results
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// get the params in the correct format...
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
        public System.Collections.Generic.IDictionary<string, string> Parameters
        {
            get
            {
                var d = new System.Collections.Generic.Dictionary<string, string>();
                d.Add("q", Term);
                d.Add("page", Page.ToString());
                d.Add("per_page ", PerPage.ToString());

                if (Sort.IsNotBlank()) //only add if not blank
                    d.Add("sort", Sort);

                if (Order.HasValue)
                    d.Add("order", Order.Value.ToString());

                return d;
            }
        }
    }
}
