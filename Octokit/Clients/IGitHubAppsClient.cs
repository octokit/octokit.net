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
    public interface IGitHubAppsClient
    {
        /// <summary>
        /// Get a single GitHub App
        /// </summary>
        /// <param name="slug">The URL-friendly name of your GitHub App. You can find this on the settings page for your GitHub App.</param>
        /// <returns>The GitHub App</returns>
        Task<GitHubApp> Get(string slug);

        /// <summary>
        /// Returns the GitHub App associated with the authentication credentials used.
        /// </summary>
        /// <returns>The GitHub App associated with the authentication credentials used.</returns>
        Task<GitHubApp> GetCurrent();

        /// <summary>
        /// Create a new installation token
        /// </summary>
        /// <param name="installationId">The Id of the GitHub App installation.</param>
        /// <returns>The GitHub App installation access token.</returns>
        Task<AccessToken> CreateInstallationToken(long installationId);

        /// <summary>
        /// List installations that are accessible to the authenticated user.
        /// </summary>
        /// <returns>A list (read-only) that includes all the GitHub App installations accessible to the current authenticated user.</returns>
        Task<IReadOnlyList<Installation>> GetAllInstallationsForCurrent();

        /// <summary>
        /// List installations that are accessible to the authenticated user.
        /// </summary>
        /// <param name="options"></param>
        /// <returns>A list (read-only) that includes all the GitHub App installations accessible to the current authenticated user.</returns>
        Task<IReadOnlyList<Installation>> GetAllInstallationsForCurrent(ApiOptions options);
    }
}