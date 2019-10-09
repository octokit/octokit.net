using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub Applications Installations API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/apps/installations/">GitHub Apps Installations API documentation</a> for more information.
    /// </remarks>
    public interface IGitHubAppInstallationsClient
    {
        /// <summary>
        /// List repositories of the authenticated GitHub App Installation (requires GitHubApp Installation-Token auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories</remarks>
        Task<RepositoriesResponse> GetAllRepositoriesForCurrent();

        /// <summary>
        /// List repositories of the authenticated GitHub App Installation (requires GitHubApp Installation-Token auth).
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories</remarks>
        Task<RepositoriesResponse> GetAllRepositoriesForCurrent(ApiOptions options);

        /// <summary>
        /// List repositories accessible to the user for an installation (requires GitHubApp User-To-Server Auth).
        /// </summary>
        /// <param name="installationId">The Id of the installation</param>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories-accessible-to-the-user-for-an-installation</remarks>
        Task<RepositoriesResponse> GetAllRepositoriesForCurrentUser(long installationId);

        /// <summary>
        /// List repositories accessible to the user for an installation (requires GitHubApp User-To-Server Auth).
        /// </summary>
        /// <param name="installationId">The Id of the installation</param>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories-accessible-to-the-user-for-an-installation</remarks>
        Task<RepositoriesResponse> GetAllRepositoriesForCurrentUser(long installationId, ApiOptions options);
    }
}