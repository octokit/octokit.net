using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// GitHub Search Api Client
    /// </summary>
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

        /// <summary>
        /// search repos
        /// http://developer.github.com/v3/search/#search-repositories
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of repos</returns>
        public Task<SearchRepositoryResult> SearchRepo(SearchRepositoriesRequest search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return ApiConnection.Get<SearchRepositoryResult>(ApiUrls.SearchRepositories(), search.Parameters);
        }

        /// <summary>
        /// search users
        /// http://developer.github.com/v3/search/#search-users
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of users</returns>
        public Task<SearchUsersResult> SearchUsers(SearchUsersRequest search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return ApiConnection.Get<SearchUsersResult>(ApiUrls.SearchUsers(), search.Parameters);
        }

        /// <summary>
        /// search issues
        /// http://developer.github.com/v3/search/#search-issues
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of issues</returns>
        public Task<SearchIssuesResult> SearchIssues(SearchIssuesRequest search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return ApiConnection.Get<SearchIssuesResult>(ApiUrls.SearchIssues(), search.Parameters);
        }

        /// <summary>
        /// search code
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of files</returns>
        public Task<SearchCodeResult> SearchCode(SearchCodeRequest search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return ApiConnection.Get<SearchCodeResult>(ApiUrls.SearchCode(), search.Parameters);
        }
    }
}