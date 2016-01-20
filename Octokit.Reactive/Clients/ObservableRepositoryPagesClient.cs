using Octokit.Reactive.Internal;
using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableRepositoryPagesClient : IObservableRepositoryPagesClient
    {
        readonly IRepositoryPagesClient _client;
        readonly IConnection _connection;

        public ObservableRepositoryPagesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Repository.Page;
            _connection = client.Connection;
        }

        public IObservable<Page> Get(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.Get(owner, repositoryName).ToObservable();
        }

        public IObservable<PagesBuild> GetBuilds(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _connection.GetAndFlattenAllPages<PagesBuild>(ApiUrls.RepositoryPageBuilds(owner, repositoryName));
        }

        public IObservable<PagesBuild> GetLatestBuild(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.GetLatestBuild(owner, repositoryName).ToObservable();
        }
    }
}
