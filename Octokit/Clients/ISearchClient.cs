using System.Threading.Tasks;
using System.Threading;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Search API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/search/">Search API documentation</a> for more information.
    /// </remarks>
    public interface ISearchClient
    {
        /// <summary>
        /// search repos
        /// http://developer.github.com/v3/search/#search-repositories
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of repos</returns>
        Task<SearchRepositoryResult> SearchRepo(SearchRepositoriesRequest search, CancellationToken cancellationToken = default);

        /// <summary>
        /// search users
        /// http://developer.github.com/v3/search/#search-users
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of users</returns>
        Task<SearchUsersResult> SearchUsers(SearchUsersRequest search, CancellationToken cancellationToken = default);

        /// <summary>
        /// search issues
        /// http://developer.github.com/v3/search/#search-issues
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of issues</returns>
        Task<SearchIssuesResult> SearchIssues(SearchIssuesRequest search, CancellationToken cancellationToken = default);

        /// <summary>
        /// search code
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of files</returns>
        Task<SearchCodeResult> SearchCode(SearchCodeRequest search, CancellationToken cancellationToken = default);

        /// <summary>
        /// search labels
        /// https://developer.github.com/v3/search/#search-labels
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of labels</returns>
        Task<SearchLabelsResult> SearchLabels(SearchLabelsRequest search, CancellationToken cancellationToken = default);
    }
}
