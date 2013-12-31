using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Searching Users
    /// </summary>
    public class SearchUsersRequest
    {
        public SearchUsersRequest(string term)
        {
            Ensure.ArgumentNotNullOrEmptyString(term, "term");
            Term = term;
            Page = 1;
            PerPage = 100;
            Order = SortDirection.Descending;
        }

        /// <summary>
        /// The search terms. This can be any combination of the supported repository search parameters:
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        public string Term { get; private set; }

        /// <summary>
        /// For http://developer.github.com/v3/search/#search-users
        /// Optional Sort field. One of followers, repositories, or joined. If not provided, results are sorted by best match.
        /// </summary>
        //public string Sort { get; set; } //re-add later

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
        /// Filter users based on the number of followers they have.
        /// </summary>
        public Range Followers { get; set; }

        /// <summary>
        /// Filter users based on when they joined.
        /// </summary>
        public DateRange Created { get; set; }

        /// <summary>
        /// Filter users by the location indicated in their profile.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Filters users based on the number of repositories they have.
        /// </summary>
        public Range Repos { get; set; }

        /// <summary>
        /// Search for users that have repositories that match a certain language.
        /// </summary>
        public Language Language { get; set; }
    }
}
