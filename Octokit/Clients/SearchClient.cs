using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Search API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/search/">Search API documentation</a> for more information.
    /// </remarks>
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
        [ManualRoute("GET", "/search/repositories")]
        public Task<SearchRepositoryResult> SearchRepo(SearchRepositoriesRequest search)
        {
            Ensure.ArgumentNotNull(search, nameof(search));
            return ApiConnection.Get<SearchRepositoryResult>(ApiUrls.SearchRepositories(), search.Parameters);
        }

        /// <summary>
        /// search users
        /// http://developer.github.com/v3/search/#search-users
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of users</returns>
        [ManualRoute("GET", "/search/users")]
        public Task<SearchUsersResult> SearchUsers(SearchUsersRequest search)
        {
            Ensure.ArgumentNotNull(search, nameof(search));
            return ApiConnection.Get<SearchUsersResult>(ApiUrls.SearchUsers(), search.Parameters);
        }

        /// <summary>
        /// search issues
        /// http://developer.github.com/v3/search/#search-issues
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of issues</returns>
        [ManualRoute("GET", "/search/issues")]
        public Task<SearchIssuesResult> SearchIssues(SearchIssuesRequest search)
        {
            Ensure.ArgumentNotNull(search, nameof(search));
            return ApiConnection.Get<SearchIssuesResult>(ApiUrls.SearchIssues(), search.Parameters);
        }

        /// <summary>
        /// search code
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of files</returns>
        [ManualRoute("GET", "/search/code")]
        public Task<SearchCodeResult> SearchCode(SearchCodeRequest search)
        {
            Ensure.ArgumentNotNull(search, nameof(search));
            return ApiConnection.Get<SearchCodeResult>(ApiUrls.SearchCode(), search.Parameters);
        }

        /// <summary>
        /// search labels
        /// https://developer.github.com/v3/search/#search-labels
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of labels</returns>
        [ManualRoute("GET", "/search/labels")]
        public Task<SearchLabelsResult> SearchLabels(SearchLabelsRequest search)
        {
            Ensure.ArgumentNotNull(search, nameof(search));
            return ApiConnection.Get<SearchLabelsResult>(ApiUrls.SearchLabels(), search.Parameters);
        }
    }
}
