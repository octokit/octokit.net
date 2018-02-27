using Octokit.Reactive.Internal;
using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableGitHubAppsClient : IObservableGitHubAppsClient
    {
        private IGitHubAppsClient _client;
        private readonly IConnection _connection;

        public ObservableGitHubAppsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.GitHubApps;
            _connection = client.Connection;
        }

        public IObservable<GitHubApp> Get(string slug)
        {
            return _client.Get(slug).ToObservable();
        }

        public IObservable<GitHubApp> GetCurrent()
        {
            return _client.GetCurrent().ToObservable();
        }

        public IObservable<AccessToken> CreateInstallationToken(long installationId)
        {
            return _client.CreateInstallationToken(installationId).ToObservable();
        }

        public IObservable<Installation> GetAllInstallationsForCurrent()
        {
            return _connection.GetAndFlattenAllPages<Installation>(ApiUrls.Installations(), null, AcceptHeaders.MachineManPreview);
        }

        public IObservable<Installation> GetAllInstallationsForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Installation>(ApiUrls.Installations(), null, AcceptHeaders.MachineManPreview, options);
        }
    }
}
