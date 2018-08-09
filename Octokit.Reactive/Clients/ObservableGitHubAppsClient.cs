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

            Installations = new ObservableGitHubAppsInstallationsClient(client);

            _client = client.GitHubApps;
            _connection = client.Connection;
        }

        public IObservableGitHubAppsInstallationsClient Installations { get; private set; }

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
            return _connection.GetAndFlattenAllPages<Installation>(ApiUrls.Installations(), null, AcceptHeaders.GitHubAppsPreview);
        }

        /// <summary>
        /// List installations of the authenticated GitHub App (requires GitHubApp JWT token auth).
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/#find-installations</remarks>
        public IObservable<Installation> GetAllInstallationsForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Installation>(ApiUrls.Installations(), null, AcceptHeaders.GitHubAppsPreview, options);
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

        /// <summary>
        /// List installations for user
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#list-installations-for-user</remarks>
        public IObservable<InstallationsResponse> GetAllInstallationsForUser()
        {
            return _connection.GetAndFlattenAllPages<InstallationsResponse>(ApiUrls.UserInstallations(), null, AcceptHeaders.GitHubAppsPreview);
        }

        /// <summary>
        /// List installations for user
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#list-installations-for-user</remarks>
        public IObservable<InstallationsResponse> GetAllInstallationsForUser(ApiOptions options)
        {
            return _connection.GetAndFlattenAllPages<InstallationsResponse>(ApiUrls.UserInstallations(), null, AcceptHeaders.GitHubAppsPreview, options);
        }

        /// <summary>
        /// Enables an authenticated GitHub App to find the organizations's installation information.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-repository-installation</remarks>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        public IObservable<Installation> GetRepositoryInstallation(string owner, string repo)
        {
            return _client.GetRepositoryInstallation(owner, repo).ToObservable();
        }

        /// <summary>
        /// Enables an authenticated GitHub App to find the repository's installation information.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-organization-installation</remarks>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        public IObservable<Installation> GetOrganizationInstallation(string organization)
        {
            return _client.GetOrganizationInstallation(organization).ToObservable();
        }

        /// <summary>
        /// Enables an authenticated GitHub App to find the users's installation information.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-user-installation</remarks>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        public IObservable<Installation> GetUserInstallation(string user)
        {
            return _client.GetUserInstallation(user).ToObservable();
        }
    }
}
