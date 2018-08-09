using System;
using Octokit.Clients;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableGitHubAppsInstallationsClient : IObservableGitHubAppsInstallationsClient
    {
        private IGitHubAppsInstallationsClient _client;
        private readonly IConnection _connection;

        public ObservableGitHubAppsInstallationsClient(IGitHubClient client)
        {
            _client = client.GitHubApps.Installations;
            _connection = client.Connection;
        }

        public IObservable<RepositoriesResponse> GetAllRepositoriesForCurrent()
        {
            return _connection.GetAndFlattenAllPages<RepositoriesResponse>(ApiUrls.InstallationRepositories(), null, AcceptHeaders.GitHubAppsPreview);
        }

        public IObservable<RepositoriesResponse> GetAllRepositoriesForCurrent(ApiOptions options)
        {
            return _connection.GetAndFlattenAllPages<RepositoriesResponse>(ApiUrls.InstallationRepositories(), null, AcceptHeaders.GitHubAppsPreview, options);
        }

        public IObservable<RepositoriesResponse> GetAllRepositoriesForUser(long installationId)
        {
            return _connection.GetAndFlattenAllPages<RepositoriesResponse>(ApiUrls.UserInstallationRepositories(installationId), null, AcceptHeaders.GitHubAppsPreview);
        }

        public IObservable<RepositoriesResponse> GetAllRepositoriesForUser(long installationId, ApiOptions options)
        {
            return _connection.GetAndFlattenAllPages<RepositoriesResponse>(ApiUrls.UserInstallationRepositories(installationId), null, AcceptHeaders.GitHubAppsPreview);
        }
    }
}