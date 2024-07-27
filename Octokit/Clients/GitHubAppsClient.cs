using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub Applications API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/apps/">GitHub Apps API documentation</a> for more information.
    /// </remarks>
    public class GitHubAppsClient : ApiClient, IGitHubAppsClient
    {
        public GitHubAppsClient(IApiConnection apiConnection) : base(apiConnection)
        {
            Ensure.ArgumentNotNull(apiConnection, nameof(apiConnection));

            Installation = new GitHubAppInstallationsClient(apiConnection);
        }

        /// <summary>
        /// Access GitHub's Apps Installations API.
        /// </summary>
        /// <remarks>
        /// Refer to the API documentation for more information: https://developer.github.com/v3/apps/installations/
        /// </remarks>
        public IGitHubAppInstallationsClient Installation { get; }

        /// <summary>
        /// Get a single GitHub App (if private, requires Personal Access Token or GitHubApp auth)
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-a-single-github-app</remarks>
        /// <param name="slug">The URL-friendly name of your GitHub App. You can find this on the settings page for your GitHub App.</param>
        [ManualRoute("GET", "/apps/{slug}")]
        public Task<GitHubApp> Get(string slug)
        {
            Ensure.ArgumentNotNullOrEmptyString(slug, nameof(slug));

            return ApiConnection.Get<GitHubApp>(ApiUrls.App(slug), null);
        }

        /// <summary>
        /// Returns the GitHub App associated with the authentication credentials used (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-the-authenticated-github-app</remarks>
        [ManualRoute("GET", "/app")]
        public Task<GitHubApp> GetCurrent()
        {
            return ApiConnection.Get<GitHubApp>(ApiUrls.App(), null);
        }

        /// <summary>
        /// List installations of the authenticated GitHub App (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-installations</remarks>
        [ManualRoute("GET", "/app/installations")]
        public Task<IReadOnlyList<Installation>> GetAllInstallationsForCurrent()
        {
            return GetAllInstallationsForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// List installations of the authenticated GitHub App (requires GitHubApp auth).
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/#find-installations</remarks>
        [ManualRoute("GET", "/app/installations")]
        public Task<IReadOnlyList<Installation>> GetAllInstallationsForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Installation>(ApiUrls.Installations(), null, options);
        }

        /// <summary>
        /// Get a single GitHub App Installation (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-a-single-installation</remarks>
        /// <param name="installationId">The Id of the GitHub App Installation</param>
        [Obsolete("This method will be removed in a future release.  Please use GetInstallationForCurrent() instead")]
        public Task<Installation> GetInstallation(long installationId)
        {
            return GetInstallationForCurrent(installationId);
        }

        /// <summary>
        /// Get a single GitHub App Installation (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-a-single-installation</remarks>
        /// <param name="installationId">The Id of the GitHub App Installation</param>
        [ManualRoute("GET", "/app/installations/{installation_id}")]
        public Task<Installation> GetInstallationForCurrent(long installationId)
        {
            return ApiConnection.Get<Installation>(ApiUrls.Installation(installationId), null);
        }

        /// <summary>
        /// List installations for the currently authenticated user (requires GitHubApp User-To-Server Auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#list-installations-for-user</remarks>
        [ManualRoute("GET", "/user/installations")]
        public async Task<InstallationsResponse> GetAllInstallationsForCurrentUser()
        {
            var results = await ApiConnection.GetAll<InstallationsResponse>(ApiUrls.UserInstallations()).ConfigureAwait(false);

            return new InstallationsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.Installations).ToList());
        }

        /// <summary>
        /// List installations for the currently authenticated user (requires GitHubApp User-To-Server Auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#list-installations-for-user</remarks>
        [ManualRoute("GET", "/user/installations")]
        public async Task<InstallationsResponse> GetAllInstallationsForCurrentUser(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var results = await ApiConnection.GetAll<InstallationsResponse>(ApiUrls.UserInstallations(), null, options).ConfigureAwait(false);

            return new InstallationsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.Installations).ToList());
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
        [ManualRoute("GET", "/app/installations/{installation_id}/access_tokens")]
        public Task<AccessToken> CreateInstallationToken(long installationId)
        {
            return ApiConnection.Post<AccessToken>(ApiUrls.AccessTokens(installationId), string.Empty);
        }

        /// <summary>
        /// Enables an authenticated GitHub App to find the organization's installation information (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-organization-installation</remarks>
        /// <param name="organization">The name of the organization</param>
        [ManualRoute("GET", "/orgs/{org}/installation")]
        public Task<Installation> GetOrganizationInstallationForCurrent(string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, nameof(organization));

            return ApiConnection.Get<Installation>(ApiUrls.OrganizationInstallation(organization), null);
        }

        /// <summary>
        /// Enables an authenticated GitHub App to find the repository's installation information (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-repository-installation</remarks>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/installation")]
        public Task<Installation> GetRepositoryInstallationForCurrent(string owner, string repo)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repo, nameof(repo));

            return ApiConnection.Get<Installation>(ApiUrls.RepoInstallation(owner, repo), null);
        }

        /// <summary>
        /// Enables an authenticated GitHub App to find the repository's installation information (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-repository-installation</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        [ManualRoute("GET", "/repositories/{id}/installation")]
        public Task<Installation> GetRepositoryInstallationForCurrent(long repositoryId)
        {
            return ApiConnection.Get<Installation>(ApiUrls.RepoInstallation(repositoryId), null);
        }

        /// <summary>
        /// Enables an authenticated GitHub App to find the users's installation information (requires GitHubApp auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-user-installation</remarks>
        /// <param name="user">The name of the user</param>
        [ManualRoute("GET", "/users/{username}/installation")]
        public Task<Installation> GetUserInstallationForCurrent(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return ApiConnection.Get<Installation>(ApiUrls.UserInstallation(user), null);
        }

        /// <summary>
        /// Creates a GitHub app by completing the handshake necessary when implementing the GitHub App Manifest flow.
        /// https://docs.github.com/apps/sharing-github-apps/registering-a-github-app-from-a-manifest
        /// </summary>
        /// <remarks>https://docs.github.com/rest/apps/apps#create-a-github-app-from-a-manifest</remarks>
        /// <param name="code">Temporary code in a code parameter.</param>
        [ManualRoute("POST", "/app-manifests/{code}/conversions")]
        public Task<GitHubAppFromManifest> CreateAppFromManifest(string code)
        {
            Ensure.ArgumentNotNullOrEmptyString(code, nameof(code));

            return ApiConnection.Post<GitHubAppFromManifest>(ApiUrls.AppManifestConversions(code));
        }
    }
}
