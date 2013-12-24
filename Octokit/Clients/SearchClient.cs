#if NET_45
using System.Collections.Generic;
#endif
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
        public Task<IReadOnlyList<Repository>> SearchRepo(SearchRepositoriesRequest search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return ApiConnection.GetAll<Repository>("search/repositories".FormatUri(), search.Parameters);
        }

        /// <summary>
        /// search users
        /// http://developer.github.com/v3/search/#search-users
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of users</returns>
        public Task<IReadOnlyList<User>> SearchUsers(SearchUsersRequest search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return ApiConnection.GetAll<User>("search/users".FormatUri(), search.ToParametersDictionary());
        }

        /// <summary>
        /// search issues
        /// http://developer.github.com/v3/search/#search-issues
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of issues</returns>
        public Task<IReadOnlyList<Issue>> SearchIssues(SearchIssuesRequest search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return ApiConnection.GetAll<Issue>("search/issues".FormatUri(), search.ToParametersDictionary());
        }

        /// <summary>
        /// search code
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of files</returns>
        public Task<IReadOnlyList<SearchCode>> SearchCode(SearchCodeRequest search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return ApiConnection.GetAll<SearchCode>("search/code".FormatUri(), search.ToParametersDictionary());
        }
    }
}