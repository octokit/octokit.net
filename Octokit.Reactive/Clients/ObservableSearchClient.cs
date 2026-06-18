using System;
using System.Threading;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// GitHub Search API Client
    /// </summary>
    public class ObservableSearchClient : IObservableSearchClient
    {
        readonly ISearchClient _client;

        public ObservableSearchClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Search;
        }

        /// <summary>
        /// search repos
        /// http://developer.github.com/v3/search/#search-repositories
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of repositories</returns>
        public IObservable<SearchRepositoryResult> SearchRepo(SearchRepositoriesRequest search, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(search, nameof(search));
            return _client.SearchRepo(search, cancellationToken).ToObservable();
        }

        /// <summary>
        /// search users
        /// http://developer.github.com/v3/search/#search-users
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of users</returns>
        public IObservable<SearchUsersResult> SearchUsers(SearchUsersRequest search, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(search, nameof(search));
            return _client.SearchUsers(search, cancellationToken).ToObservable();
        }

        /// <summary>
        /// search issues
        /// http://developer.github.com/v3/search/#search-issues
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of issues</returns>
        public IObservable<SearchIssuesResult> SearchIssues(SearchIssuesRequest search, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(search, nameof(search));
            return _client.SearchIssues(search, cancellationToken).ToObservable();
        }

        /// <summary>
        /// search code
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of files</returns>
        public IObservable<SearchCodeResult> SearchCode(SearchCodeRequest search, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(search, nameof(search));
            return _client.SearchCode(search, cancellationToken).ToObservable();
        }

        /// <summary>
        /// search labels
        /// https://developer.github.com/v3/search/#search-labels
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of labels</returns>
        public IObservable<SearchLabelsResult> SearchLabels(SearchLabelsRequest search, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(search, nameof(search));
            return _client.SearchLabels(search, cancellationToken).ToObservable();
        }
    }
}
