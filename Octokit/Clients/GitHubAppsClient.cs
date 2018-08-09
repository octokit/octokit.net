using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub Applications API. Provides the methods required to get GitHub applications and installations.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/apps/">GitHub Apps API documentation</a> for more information.
    /// </remarks>
    public class GitHubAppsClient : ApiClient, IGitHubAppsClient
    {
        public GitHubAppsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Get a single GitHub App.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-a-single-github-app</remarks>
        /// <param name="slug">The URL-friendly name of your GitHub App. You can find this on the settings page for your GitHub App.</param>
        public Task<GitHubApp> Get(string slug)
        {
            return ApiConnection.Get<GitHubApp>(ApiUrls.App(slug), null, AcceptHeaders.GitHubAppsPreview);
        }

        /// <summary>
        /// Returns the GitHub App associated with the authentication credentials used (requires GitHubApp JWT token auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-the-authenticated-github-app</remarks>
        public Task<GitHubApp> GetCurrent()
        {
            return ApiConnection.Get<GitHubApp>(ApiUrls.App(), null, AcceptHeaders.GitHubAppsPreview);
        }

        /// <summary>
        /// List installations of the authenticated GitHub App (requires GitHubApp JWT token auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-installations</remarks>
        public Task<IReadOnlyList<Installation>> GetAllInstallationsForCurrent()
        {
            return ApiConnection.GetAll<Installation>(ApiUrls.Installations(), null, AcceptHeaders.GitHubAppsPreview);
        }

        /// <summary>
        /// List installations of the authenticated GitHub App (requires GitHubApp JWT token auth).
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/#find-installations</remarks>
        public Task<IReadOnlyList<Installation>> GetAllInstallationsForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Installation>(ApiUrls.Installations(), null, AcceptHeaders.GitHubAppsPreview, options);
        }

        /// <summary>
        /// Enables an authenticated GitHub App to find the organizations's installation information.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-repository-installation</remarks>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        public Task<Installation> GetRepositoryInstallation(string owner, string repo)
        {
            return ApiConnection.Get<Installation>(ApiUrls.RepoInstallation(owner, repo), null, AcceptHeaders.GitHubAppsPreview);
        }

        /// <summary>
        /// Enables an authenticated GitHub App to find the repository's installation information.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-organization-installation</remarks>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        public Task<Installation> GetOrganizationInstallation(string organization)
        {
            return ApiConnection.Get<Installation>(ApiUrls.OrganizationInstallation(organization), null, AcceptHeaders.GitHubAppsPreview);
        }

        /// <summary>
        /// Enables an authenticated GitHub App to find the users's installation information.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-user-installation</remarks>
        /// <param name="owner">The owner of the repo</param>
        /// <param name="repo">The name of the repo</param>
        public Task<Installation> GetUserInstallation(string user)
        {
            return ApiConnection.Get<Installation>(ApiUrls.UserInstallation(user), null, AcceptHeaders.GitHubAppsPreview);
        }

        /// <summary>
        /// Get a single GitHub App Installation (requires GitHubApp JWT token auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-a-single-installation</remarks>
        /// <param name="installationId">The Id of the GitHub App Installation</param>
        public Task<Installation> GetInstallation(long installationId)
        {
            return ApiConnection.Get<Installation>(ApiUrls.Installation(installationId), null, AcceptHeaders.GitHubAppsPreview);
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
        public Task<AccessToken> CreateInstallationToken(long installationId)
        {
            return ApiConnection.Post<AccessToken>(ApiUrls.AccessTokens(installationId), string.Empty, AcceptHeaders.GitHubAppsPreview);
        }

        /// <summary>
        /// List installations for user
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#list-installations-for-user</remarks>
        public async Task<InstallationsResponse> GetAllInstallationsForUser()
        {
            var results = await ApiConnection.GetAll<InstallationsResponse>(ApiUrls.UserInstallations(), null, AcceptHeaders.GitHubAppsPreview).ConfigureAwait(false);

            return new InstallationsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.Installations).ToList());
        }

        /// <summary>
        /// List installations for user
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#list-installations-for-user</remarks>
        public async Task<InstallationsResponse> GetAllInstallationsForUser(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var results = await ApiConnection.GetAll<InstallationsResponse>(ApiUrls.UserInstallations(), null, AcceptHeaders.GitHubAppsPreview, options).ConfigureAwait(false);

            return new InstallationsResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.Installations).ToList());
        }
    }
}