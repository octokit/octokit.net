using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

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
        /// <returns>List of repos</returns>
        IObservable<Repository> SearchRepo(SearchRepositoriesRequest search);

        /// <summary>
        /// search users
        /// http://developer.github.com/v3/search/#search-users
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of users</returns>
        IObservable<User> SearchUsers(SearchUsersRequest search);

        /// <summary>
        /// search issues
        /// http://developer.github.com/v3/search/#search-issues
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of issues</returns>
        IObservable<Issue> SearchIssues(SearchIssuesRequest search);

        /// <summary>
        /// search code
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of files</returns>
        IObservable<SearchCode> SearchCode(SearchCodeRequest search);
    }
}