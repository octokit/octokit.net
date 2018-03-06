using Octokit.Reactive.Internal;
using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub Applications API. Provides the methods required to get GitHub applications and installations.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/apps/">GitHub Apps API documentation</a> for more information.
    /// </remarks>
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

        /// <summary>
        /// Returns the GitHub App associated with the authentication credentials used (requires GitHubApp JWT token auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-the-authenticated-github-app</remarks>
        public IObservable<GitHubApp> GetCurrent()
        {
            return _client.GetCurrent().ToObservable();
        }

        /// <summary>
        /// List installations of the authenticated GitHub App (requires GitHubApp JWT token auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-installations</remarks>
        public IObservable<Installation> GetAllInstallationsForCurrent()
        {
            return _connection.GetAndFlattenAllPages<Installation>(ApiUrls.Installations(), null, AcceptHeaders.MachineManPreview);
        }

        /// <summary>
        /// List installations of the authenticated GitHub App (requires GitHubApp JWT token auth).
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/#find-installations</remarks>
        public IObservable<Installation> GetAllInstallationsForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Installation>(ApiUrls.Installations(), null, AcceptHeaders.MachineManPreview, options);
        }

        /// <summary>
        /// Get a single GitHub App Installation (requires GitHubApp JWT token auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-a-single-installation</remarks>
        /// <param name="installationId">The Id of the GitHub App Installation</param>
        public IObservable<Installation> GetInstallation(long installationId)
        {
            return _client.GetInstallation(installationId).ToObservable();
        }

        /// <summary>
        /// Create a time bound access token for a GitHubApp Installation that can be used to access other API endpoints (requires GitHubApp JWT token auth).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/apps/#create-a-new-installation-token
        /// https://developer.github.com/apps/building-github-apps/authentication-options-for-github-apps/#authenticating-as-an-installation
        /// https://developer.github.com/v3/apps/available-endpoints/
        /// </remarks>
        /// <param name="installationId">The Id of the GitHub App Installation</param>
        public IObservable<AccessToken> CreateInstallationToken(long installationId)
        {
            return _client.CreateInstallationToken(installationId).ToObservable();
        }
    }
}
