using System.Collections.Generic;
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
        /// <summary>
        /// Instantiates a new GitHub Apps API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
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
            return ApiConnection.Get<GitHubApp>(ApiUrls.App(slug), null, AcceptHeaders.MachineManPreview);
        }

        /// <summary>
        /// Returns the GitHub App associated with the authentication credentials used (requires GitHubApp JWT token auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-the-authenticated-github-app</remarks>
        public Task<GitHubApp> GetCurrent()
        {
            return ApiConnection.Get<GitHubApp>(ApiUrls.App(), null, AcceptHeaders.MachineManPreview);
        }

        /// <summary>
        /// List installations of the authenticated GitHub App (requires GitHubApp JWT token auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#find-installations</remarks>
        public Task<IReadOnlyList<Installation>> GetAllInstallationsForCurrent()
        {
            return ApiConnection.GetAll<Installation>(ApiUrls.Installations(), null, AcceptHeaders.MachineManPreview);
        }

        /// <summary>
        /// List installations of the authenticated GitHub App (requires GitHubApp JWT token auth).
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/#find-installations</remarks>
        public Task<IReadOnlyList<Installation>> GetAllInstallationsForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Installation>(ApiUrls.Installations(), null, AcceptHeaders.MachineManPreview, options);
        }

        /// <summary>
        /// Get a single GitHub App Installation (requires GitHubApp JWT token auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/#get-a-single-installation</remarks>
        /// <param name="installationId">The Id of the GitHub App Installation</param>
        public Task<Installation> GetInstallation(long installationId)
        {
            return ApiConnection.Get<Installation>(ApiUrls.Installation(installationId), null, AcceptHeaders.MachineManPreview);
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
            return ApiConnection.Post<AccessToken>(ApiUrls.AccessTokens(installationId), string.Empty, AcceptHeaders.MachineManPreview);
        }
    }
}