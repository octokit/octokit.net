using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub Applications API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/apps/">GitHub Apps API documentation</a> for more information.
    /// </remarks>
    public class ObservableGitHubAppsClient : IObservableGitHubAppsClient
    {
        private readonly IGitHubAppsClient _client;
        private readonly IConnection _connection;

        public ObservableGitHubAppsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            Installation = new ObservableGitHubAppInstallationsClient(client);

            _client = client.GitHubApps;
            _connection = client.Connection;
        }

        /// <summary>
        /// Access GitHub's Apps Installations API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/apps/installations/
        /// </remarks>
        public IObservableGitHubAppInstallationsClient Installation { get; private set; }

        /// <summary>
        /// Get a single GitHub App (if private, requires Personal Access Token or GitHubApp auth)
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-a-single-github-app</remarks>
        /// <param name="slug">The URL-friendly name of your GitHub App. You can find this on the settings page for your GitHub App.</param>
        public IObservable<GitHubApp> Get(string slug)
        {
            Ensure.ArgumentNotNullOrEmptyString(slug, nameof(slug));

            return _client.Get(slug).ToObservable();
        }

        /// <summary>
        /// Returns the GitHub App associated with the authentication credentials used (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-the-authenticated-github-app</remarks>
        public IObservable<GitHubApp> GetCurrent()
        {
            return _client.GetCurrent().ToObservable();
        }

        /// <summary>
        /// List installations of the authenticated GitHub App (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-installations</remarks>
        public IObservable<Installation> GetAllInstallationsForCurrent()
        {
            return GetAllInstallationsForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// List installations of the authenticated GitHub App (requires GitHubApp auth).
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/#find-installations</remarks>
        public IObservable<Installation> GetAllInstallationsForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Installation>(ApiUrls.Installations(), null, options);
        }

        /// <summary>
        /// Get a single GitHub App Installation (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-a-single-installation</remarks>
        /// <param name="installationId">The Id of the GitHub App Installation</param>
        [Obsolete("This method will be removed in a future release.  Please use GetInstallationForCurrent() instead")]
        public IObservable<Installation> GetInstallation(long installationId)
        {
            return GetInstallationForCurrent(installationId);
        }

        /// <summary>
        /// Get a single GitHub App Installation (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-a-single-installation</remarks>
        /// <param name="installationId">The Id of the GitHub App Installation</param>
        public IObservable<Installation> GetInstallationForCurrent(long installationId)
        {
            return _client.GetInstallationForCurrent(installationId).ToObservable();
        }

        /// <summary>
        /// List installations for the currently authenticated user (requires GitHubApp User-To-Server Auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#list-installations-for-user</remarks>
        public IObservable<InstallationsResponse> GetAllInstallationsForCurrentUser()
        {
            return _connection.GetAndFlattenAllPages<InstallationsResponse>(ApiUrls.UserInstallations());
        }

        /// <summary>
        /// List installations for the currently authenticated user (requires GitHubApp User-To-Server Auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#list-installations-for-user</remarks>
        public IObservable<InstallationsResponse> GetAllInstallationsForCurrentUser(ApiOptions options)
        {
            return _connection.GetAndFlattenAllPages<InstallationsResponse>(ApiUrls.UserInstallations(), options);
        }

        /// <summary>
        /// Create a time bound access token for a GitHubApp Installation that can be used to access other API endpoints (requires GitHubApp auth).
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
        /// Enables an authenticated GitHub App to find the organization's installation information (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-organization-installation</remarks>
        /// <param name="organization">The name of the organization</param>
        public IObservable<Installation> GetOrganizationInstallationForCurrent(string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));

            return _client.GetOrganizationInstallationForCurrent(organization).ToObservable();
        }

        /// <summary>
        /// Enables an authenticated GitHub App to find the repository's installation information (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-repository-installation</remarks>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        public IObservable<Installation> GetRepositoryInstallationForCurrent(string owner, string repo)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            return _client.GetRepositoryInstallationForCurrent(owner, repo).ToObservable();
        }

        /// <summary>
        /// Enables an authenticated GitHub App to find the repository's installation information (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-repository-installation</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<Installation> GetRepositoryInstallationForCurrent(long repositoryId)
        {
            return _client.GetRepositoryInstallationForCurrent(repositoryId).ToObservable();
        }

        /// <summary>
        /// Enables an authenticated GitHub App to find the users's installation information (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-user-installation</remarks>
        /// <param name="user">The name of the user</param>
        public IObservable<Installation> GetUserInstallationForCurrent(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return _client.GetUserInstallationForCurrent(user).ToObservable();
        }

        /// <summary>
        /// Creates a GitHub app by completing the handshake necessary when implementing the GitHub App Manifest flow.
        /// https://docs.github.com/apps/sharing-github-apps/registering-a-github-app-from-a-manifest
        /// </summary>
        /// <remarks>https://docs.github.com/rest/apps/apps#create-a-github-app-from-a-manifest</remarks>
        /// <param name="code">Temporary code in a code parameter.</param>
        public IObservable<GitHubAppFromManifest> CreateAppFromManifest(string code)
        {
            Ensure.ArgumentNotNullOrEmptyString(code, nameof(code));

            return _client.CreateAppFromManifest(code).ToObservable();
        }
    }
}
