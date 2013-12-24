using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableSearchClient : IObservableSearchClient
    {
        readonly ISearchClient _client;
        readonly IConnection _connection;

        public ObservableSearchClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Search;
            _connection = client.Connection;
        }

        /// <summary>
        /// search repos
        /// http://developer.github.com/v3/search/#search-repositories
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List of repos</returns>
        public IObservable<Repository> SearchRepo(SearchRepositoriesRequest search)
        {
            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.SeachRepos(), search.Parameters);
        }

        public IObservable<User> SearchUsers(SearchUsersRequest search)
        {
            throw new NotImplementedException();
        }

        public IObservable<Issue> SearchIssues(SearchIssuesRequest search)
        {
            throw new NotImplementedException();
        }

        public IObservable<SearchCode> SearchCode(SearchCodeRequest search)
        {
            throw new NotImplementedException();
        }
    }
}