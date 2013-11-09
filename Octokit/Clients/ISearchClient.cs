#if NET_45
using System.Collections.Generic;
#endif
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
        Task<IReadOnlyList<Repository>> SearchRepo(SearchTerm search);

        /// <summary>
        /// search users
        /// http://developer.github.com/v3/search/#search-users
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of users</returns>
        Task<IReadOnlyList<SearchUser>> SearchUsers(SearchTerm search);

        /// <summary>
        /// search issues
        /// http://developer.github.com/v3/search/#search-issues
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of issues</returns>
        Task<IReadOnlyList<Issue>> SearchIssues(SearchTerm search);

        /// <summary>
        /// search code
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of files</returns>
        Task<IReadOnlyList<SearchCode>> SearchCode(SearchTerm search);
    }
}