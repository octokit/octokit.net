using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// GitHub Search Api Client
    /// </summary>
    public interface IObservableSearchClient
    {
        /// <summary>
        /// search repos
        /// http://developer.github.com/v3/search/#search-repositories
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of repositories</returns>
        IObservable<SearchRepositoryResult> SearchRepo(SearchRepositoriesRequest search);

        /// <summary>
        /// search users
        /// http://developer.github.com/v3/search/#search-users
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of users</returns>
        IObservable<SearchUsersResult> SearchUsers(SearchUsersRequest search);

        /// <summary>
        /// search issues
        /// http://developer.github.com/v3/search/#search-issues
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of issues</returns>
        IObservable<SearchIssuesResult> SearchIssues(SearchIssuesRequest search);

        /// <summary>
        /// search code
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of files</returns>
        IObservable<SearchCodeResult> SearchCode(SearchCodeRequest search);
    }
}