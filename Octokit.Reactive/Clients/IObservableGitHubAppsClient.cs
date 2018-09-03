﻿using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub Applications API.
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
        IObservableGitHubAppInstallationsClient Installation { get; }

        /// <summary>
        /// Get a single GitHub App (if private, requires Personal Access Token or GitHubApp auth)
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-a-single-github-app</remarks>
        /// <param name="slug">The URL-friendly name of your GitHub App. You can find this on the settings page for your GitHub App.</param>
        IObservable<GitHubApp> Get(string slug);

        /// <summary>
        /// Returns the GitHub App associated with the authentication credentials used (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-the-authenticated-github-app</remarks>
        IObservable<GitHubApp> GetCurrent();

        /// <summary>
        /// List installations of the authenticated GitHub App (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-installations</remarks>
        IObservable<Installation> GetAllInstallationsForCurrent();

        /// <summary>
        /// List installations of the authenticated GitHub App (requires GitHubApp auth).
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/#find-installations</remarks>
        IObservable<Installation> GetAllInstallationsForCurrent(ApiOptions options);

        /// <summary>
        /// Get a single GitHub App Installation (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-a-single-installation</remarks>
        /// <param name="installationId">The Id of the GitHub App Installation</param>
        [Obsolete("This method will be removed in a future release.  Please use GetInstallationForCurrent() instead")]
        IObservable<Installation> GetInstallation(long installationId);

        /// <summary>
        /// Get a single GitHub App Installation (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-a-single-installation</remarks>
        /// <param name="installationId">The Id of the GitHub App Installation</param>
        IObservable<Installation> GetInstallationForCurrent(long installationId);

        /// <summary>
        /// List installations for the currently authenticated user (requires GitHubApp User-To-Server Auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#list-installations-for-user</remarks>
        IObservable<InstallationsResponse> GetAllInstallationsForCurrentUser();

        /// <summary>
        /// List installations for the currently authenticated user (requires GitHubApp User-To-Server Auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#list-installations-for-user</remarks>
        IObservable<InstallationsResponse> GetAllInstallationsForCurrentUser(ApiOptions options);

        /// <summary>
        /// Create a time bound access token for a GitHubApp Installation that can be used to access other API endpoints (requires GitHubApp auth).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/apps/#create-a-new-installation-token
        /// https://developer.github.com/apps/building-github-apps/authentication-options-for-github-apps/#authenticating-as-an-installation
        /// https://developer.github.com/v3/apps/available-endpoints/
        /// </remarks>
        /// <param name="installationId">The Id of the GitHub App Installation</param>
        IObservable<AccessToken> CreateInstallationToken(long installationId);

        /// <summary>
        /// Enables an authenticated GitHub App to find the organization's installation information (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-organization-installation</remarks>
        /// <param name="organization">The name of the organization</param>
        IObservable<Installation> GetOrganizationInstallationForCurrent(string organization);

        /// <summary>
        /// Enables an authenticated GitHub App to find the repository's installation information (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-repository-installation</remarks>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        IObservable<Installation> GetRepositoryInstallationForCurrent(string owner, string repo);

        /// <summary>
        /// Enables an authenticated GitHub App to find the repository's installation information (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-repository-installation</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        IObservable<Installation> GetRepositoryInstallationForCurrent(long repositoryId);

        /// <summary>
        /// Enables an authenticated GitHub App to find the users's installation information (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-user-installation</remarks>
        /// <param name="user">The name of the user</param>
        IObservable<Installation> GetUserInstallationForCurrent(string user);
    }
}
