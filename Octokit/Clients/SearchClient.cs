#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;
namespace Octokit
{
    public class SearchClient : ApiClient, ISearchClient
    {
        /// <summary>
        /// Initializes a new GitHub Search API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public SearchClient(IApiConnection apiConnection)
            : base(apiConnection)
        {

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString", 
            Justification="Trust me")]
        public Task<IReadOnlyList<SearchRepo>> SearchRepo(SearchTerm search)
        {
            Ensure.ArgumentNotNull(search, "search");
            var param = new Dictionary<string, string>();
            param.Add("q", search.Term);
            param.Add("page", search.Page.ToString());
            param.Add("per_page ", search.PerPage.ToString());

            if (search.Sort.HasValue)
                param.Add("sort", search.Sort.Value.ToString());

            if (search.Order.HasValue)
                param.Add("order", search.Order.Value.ToString());

            return ApiConnection.GetAll<SearchRepo>("search/repositories".FormatUri(), param);
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int32.ToString")]
        public Task<IReadOnlyList<SearchUser>> SearchUsers(SearchTerm search)
        {
            Ensure.ArgumentNotNull(search, "search");
            var param = new Dictionary<string, string>();
            param.Add("q", search.Term);
            param.Add("page", search.Page.ToString());
            param.Add("per_page ", search.PerPage.ToString());

            if (search.Sort.HasValue)
                param.Add("sort", search.Sort.Value.ToString());

            if (search.Order.HasValue)
                param.Add("order", search.Order.Value.ToString());

            return ApiConnection.GetAll<SearchUser>("search/users".FormatUri(), param);
        }
    }
}