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
        /// Get a single GitHub App
        /// </summary>
        /// <param name="slug">The URL-friendly name of your GitHub App. You can find this on the settings page for your GitHub App.</param>
        /// <returns>The GitHub App</returns>
        public Task<GitHubApp> Get(string slug)
        {
            return ApiConnection.Get<GitHubApp>(ApiUrls.App(slug), null, AcceptHeaders.MachineManPreview);
        }

        /// <summary>
        /// Returns the GitHub App associated with the authentication credentials used.
        /// </summary>
        /// <returns>The GitHub App associated with the authentication credentials used.</returns>
        public Task<GitHubApp> GetCurrent()
        {
            return ApiConnection.Get<GitHubApp>(ApiUrls.App(), null, AcceptHeaders.MachineManPreview);
        }

        /// <summary>
        /// Create a new installation token
        /// </summary>
        /// <param name="installationId">The Id of the GitHub App installation.</param>
        /// <returns>The GitHub App installation access token.</returns>
        public Task<AccessToken> CreateInstallationToken(long installationId)
        {
            return ApiConnection.Post<AccessToken>(ApiUrls.AccessTokens(installationId), string.Empty, AcceptHeaders.MachineManPreview);
        }

        /// <summary>
        /// List installations that are accessible to the authenticated user.
        /// </summary>
        /// <returns>A list (read-only) that includes all the GitHub App installations accessible to the current authenticated user.</returns>
        public Task<IReadOnlyList<Installation>> GetAllInstallationsForCurrent()
        {
            return ApiConnection.GetAll<Installation>(ApiUrls.Installations(), null, AcceptHeaders.MachineManPreview);
        }

        /// <summary>
        /// List installations that are accessible to the authenticated user.
        /// </summary>
        /// <param name="options"></param>
        /// <returns>A list (read-only) that includes all the GitHub App installations accessible to the current authenticated user.</returns>
        public Task<IReadOnlyList<Installation>> GetAllInstallationsForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Installation>(ApiUrls.Installations(), null, AcceptHeaders.MachineManPreview, options);
        }
    }
}