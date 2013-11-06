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

        public Task<IReadOnlyList<SearchRepo>> SearchRepo(SearchTerm search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return ApiConnection.GetAll<SearchRepo>("search/repositories".FormatUri(), search.Parameters);
        }

        public Task<IReadOnlyList<SearchUser>> SearchUsers(SearchTerm search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return ApiConnection.GetAll<SearchUser>("search/users".FormatUri(), search.Parameters);
        }
    }
}