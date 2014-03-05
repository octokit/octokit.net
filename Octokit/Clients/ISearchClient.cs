using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// GitHub Search Api Client
    /// </summary>
    public interface ISearchClient
    {
        /// <summary>
        /// search repos
        /// http://developer.github.com/v3/search/#search-repositories
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of repos</returns>
        Task<SearchRepositoryResult> SearchRepo(SearchRepositoriesRequest search);

        /// <summary>
        /// search users
        /// http://developer.github.com/v3/search/#search-users
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of users</returns>
        Task<SearchUsersResult> SearchUsers(SearchUsersRequest search);

        /// <summary>
        /// search issues
        /// http://developer.github.com/v3/search/#search-issues
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of issues</returns>
        Task<SearchIssuesResult> SearchIssues(SearchIssuesRequest search);

        /// <summary>
        /// search code
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of files</returns>
        Task<SearchCodeResult> SearchCode(SearchCodeRequest search);
    }
}