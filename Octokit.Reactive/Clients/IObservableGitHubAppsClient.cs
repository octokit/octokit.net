using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub Applications API. Provides the methods required to get GitHub applications and installations.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/apps/">GitHub Apps API documentation</a> for more information.
    /// </remarks>
    public interface IObservableGitHubAppsClient
    {
        /// <summary>
        /// Access GitHub's Apps Installations API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/apps/installations/
        /// </remarks>
        IObservableGitHubAppsInstallationsClient Installations { get; }

        /// <summary>
        /// Get a single GitHub App.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-a-single-github-app</remarks>
        /// <param name="slug">The URL-friendly name of your GitHub App. You can find this on the settings page for your GitHub App.</param>
        IObservable<GitHubApp> Get(string slug);

        /// <summary>
        /// Returns the GitHub App associated with the authentication credentials used (requires GitHubApp JWT token auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-the-authenticated-github-app</remarks>
        IObservable<GitHubApp> GetCurrent();

        /// <summary>
        /// List installations of the authenticated GitHub App (requires GitHubApp JWT token auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-installations</remarks>
        IObservable<Installation> GetAllInstallationsForCurrent();

        /// <summary>
        /// List installations of the authenticated GitHub App (requires GitHubApp JWT token auth).
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/#find-installations</remarks>
        IObservable<Installation> GetAllInstallationsForCurrent(ApiOptions options);

        /// <summary>
        /// Get a single GitHub App Installation (requires GitHubApp JWT token auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-a-single-installation</remarks>
        /// <param name="installationId">The Id of the GitHub App Installation</param>
        IObservable<Installation> GetInstallation(long installationId);

        /// <summary>
        /// Create a time bound access token for a GitHubApp Installation that can be used to access other API endpoints (requires GitHubApp JWT token auth).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/apps/#create-a-new-installation-token
        /// https://developer.github.com/apps/building-github-apps/authentication-options-for-github-apps/#authenticating-as-an-installation
        /// https://developer.github.com/v3/apps/available-endpoints/
        /// </remarks>
        /// <param name="installationId">The Id of the GitHub App Installation</param>
        IObservable<AccessToken> CreateInstallationToken(long installationId);

        /// <summary>
        /// List installations for user
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#list-installations-for-user</remarks>
        IObservable<InstallationsResponse> GetAllInstallationsForUser();

        /// <summary>
        /// List installations for user
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#list-installations-for-user</remarks>
        IObservable<InstallationsResponse> GetAllInstallationsForUser(ApiOptions options);

        /// <summary>
        /// Enables an authenticated GitHub App to find the organizations's installation information.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-repository-installation</remarks>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        IObservable<Installation> GetRepositoryInstallation(string owner, string repo);

        /// <summary>
        /// Enables an authenticated GitHub App to find the repository's installation information.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-organization-installation</remarks>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        IObservable<Installation> GetOrganizationInstallation(string organization);

        /// <summary>
        /// Enables an authenticated GitHub App to find the users's installation information.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-user-installation</remarks>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        IObservable<Installation> GetUserInstallation(string user);
    }
}
