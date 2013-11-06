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
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<IReadOnlyList<SearchRepo>> SearchRepo(SearchTerm search);

        /// <summary>
        /// search users
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<IReadOnlyList<SearchUser>> SearchUsers(SearchTerm search);

        /// <summary>
        /// search issues
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<IReadOnlyList<SearchIssue>> SearchIssues(SearchTerm search);

    }
}