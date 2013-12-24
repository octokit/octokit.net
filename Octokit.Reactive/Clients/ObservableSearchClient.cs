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

        public IObservable<Repository> SearchRepo(SearchRepositoriesRequest search)
        {
            throw new NotImplementedException();
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