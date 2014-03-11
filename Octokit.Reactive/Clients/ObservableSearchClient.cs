using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// GitHub Search Api Client
    /// </summary>
    public class ObservableSearchClient : IObservableSearchClient
    {
        readonly ISearchClient _client;

        public ObservableSearchClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Search;
        }

        /// <summary>
        /// search repos
        /// http://developer.github.com/v3/search/#search-repositories
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of repositories</returns>
        public IObservable<SearchRepositoryResult> SearchRepo(SearchRepositoriesRequest search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return _client.SearchRepo(search).ToObservable();
        }

        /// <summary>
        /// search users
        /// http://developer.github.com/v3/search/#search-users
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of users</returns>
        public IObservable<SearchUsersResult> SearchUsers(SearchUsersRequest search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return _client.SearchUsers(search).ToObservable();
        }

        /// <summary>
        /// search issues
        /// http://developer.github.com/v3/search/#search-issues
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of issues</returns>
        public IObservable<SearchIssuesResult> SearchIssues(SearchIssuesRequest search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return _client.SearchIssues(search).ToObservable();
        }

        /// <summary>
        /// search code
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of files</returns>
        public IObservable<SearchCodeResult> SearchCode(SearchCodeRequest search)
        {
            Ensure.ArgumentNotNull(search, "search");
            return _client.SearchCode(search).ToObservable();
        }
    }
}