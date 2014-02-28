using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// GitHub Search Api Client
    /// </summary>
    public class ObservableSearchClient : IObservableSearchClient
    {
        readonly IConnection _connection;

        public ObservableSearchClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _connection = client.Connection;
        }

        /// <summary>
        /// search repos
        /// http://developer.github.com/v3/search/#search-repositories
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of repositories</returns>
        public IObservable<Repository> SearchRepo(SearchRepositoriesRequest search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.SearchRepositories(), search.Parameters);
        }

        /// <summary>
        /// search users
        /// http://developer.github.com/v3/search/#search-users
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of users</returns>
        public IObservable<User> SearchUsers(SearchUsersRequest search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return _connection.GetAndFlattenAllPages<User>(ApiUrls.SearchUsers(), search.Parameters);
        }

        /// <summary>
        /// search issues
        /// http://developer.github.com/v3/search/#search-issues
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of issues</returns>
        public IObservable<Issue> SearchIssues(SearchIssuesRequest search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return _connection.GetAndFlattenAllPages<Issue>(ApiUrls.SearchIssues(), search.Parameters);
        }

        /// <summary>
        /// search code
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of files</returns>
        public IObservable<SearchCode> SearchCode(SearchCodeRequest search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return _connection.GetAndFlattenAllPages<SearchCode>(ApiUrls.SearchCode(), search.Parameters);
        }
    }
}