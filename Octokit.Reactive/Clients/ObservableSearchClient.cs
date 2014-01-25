using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
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
        /// <param name="request"></param>
        /// <returns>List of repositories</returns>
        public IObservable<Repository> SearchRepo(SearchRepositoriesRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");
            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.SearchRepositories(), request.Parameters);
        }

        /// <summary>
        /// search users
        /// http://developer.github.com/v3/search/#search-users
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List of users</returns>
        public IObservable<User> SearchUsers(SearchUsersRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");
            return _connection.GetAndFlattenAllPages<User>(ApiUrls.SearchUsers(), request.Parameters);
        }

        /// <summary>
        /// search issues
        /// http://developer.github.com/v3/search/#search-issues
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List of issues</returns>
        public IObservable<Issue> SearchIssues(SearchIssuesRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");
            return _connection.GetAndFlattenAllPages<Issue>(ApiUrls.SearchIssues(), request.Parameters);
        }

        /// <summary>
        /// search code
        /// http://developer.github.com/v3/search/#search-code
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List of files</returns>
        public IObservable<SearchCode> SearchCode(SearchCodeRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");
            return _connection.GetAndFlattenAllPages<SearchCode>(ApiUrls.SearchCode(), request.Parameters);
        }
    }
}