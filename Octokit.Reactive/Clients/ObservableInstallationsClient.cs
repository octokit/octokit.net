using System;
using Octokit.Clients;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub Applications Installations API. Provides the methods required to get GitHub Apps installations.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/apps/installations/">GitHub Apps Installations API documentation</a> for more information.
    /// </remarks>
    public class ObservableGitHubAppsInstallationsClient : IObservableGitHubAppsInstallationsClient
    {
        private IGitHubAppsInstallationsClient _client;
        private readonly IConnection _connection;

        public ObservableGitHubAppsInstallationsClient(IGitHubClient client)
        {
            _client = client.GitHubApps.Installations;
            _connection = client.Connection;
        }

        /// <summary>
        /// List repositories of the authenticated GitHub App (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories</remarks>
        public IObservable<RepositoriesResponse> GetAllRepositoriesForCurrent()
        {
            return _connection.GetAndFlattenAllPages<RepositoriesResponse>(ApiUrls.InstallationRepositories(), null, AcceptHeaders.GitHubAppsPreview);
        }

        /// <summary>
        /// List repositories of the authenticated GitHub App (requires GitHubApp auth).
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories</remarks>
        public IObservable<RepositoriesResponse> GetAllRepositoriesForCurrent(ApiOptions options)
        {
            return _connection.GetAndFlattenAllPages<RepositoriesResponse>(ApiUrls.InstallationRepositories(), null, AcceptHeaders.GitHubAppsPreview, options);
        }

        /// <summary>
        /// List repositories accessible to the user for an installation (requires GitHubApp User-To-Server Auth).
        /// </summary>
        /// <param name="installationId">The id of the installation</param>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories-accessible-to-the-user-for-an-installation</remarks>
        public IObservable<RepositoriesResponse> GetAllRepositoriesForUser(long installationId)
        {
            return _connection.GetAndFlattenAllPages<RepositoriesResponse>(ApiUrls.UserInstallationRepositories(installationId), null, AcceptHeaders.GitHubAppsPreview);
        }

        /// <summary>
        /// List repositories accessible to the user for an installation (requires GitHubApp User-To-Server Auth).
        /// </summary>
        /// <param name="installationId">The id of the installation</param>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories-accessible-to-the-user-for-an-installation</remarks>
        public IObservable<RepositoriesResponse> GetAllRepositoriesForUser(long installationId, ApiOptions options)
        {
            return _connection.GetAndFlattenAllPages<RepositoriesResponse>(ApiUrls.UserInstallationRepositories(installationId), null, AcceptHeaders.GitHubAppsPreview);
        }
    }
}