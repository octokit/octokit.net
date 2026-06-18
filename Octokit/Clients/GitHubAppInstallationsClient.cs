using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub Applications Installations API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/apps/installations/">GitHub Apps Installations API documentation</a> for more information.
    /// </remarks>
    public class GitHubAppInstallationsClient : ApiClient, IGitHubAppInstallationsClient
    {
        public GitHubAppInstallationsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// List repositories of the authenticated GitHub App Installation (requires GitHubApp Installation-Token auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories</remarks>
        [ManualRoute("GET", "/installation/repositories")]
        public Task<RepositoriesResponse> GetAllRepositoriesForCurrent(CancellationToken cancellationToken = default)
        {
            return GetAllRepositoriesForCurrent(ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List repositories of the authenticated GitHub App Installation (requires GitHubApp Installation-Token auth).
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories</remarks>
        [ManualRoute("GET", "/installation/repositories")]
        public async Task<RepositoriesResponse> GetAllRepositoriesForCurrent(ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var results = await ApiConnection.GetAll<RepositoriesResponse>(ApiUrls.InstallationRepositories(), null, options, cancellationToken).ConfigureAwait(false);

            return new RepositoriesResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.Repositories).ToList());
        }

        /// <summary>
        /// List repositories accessible to the user for an installation (requires GitHubApp User-To-Server Auth).
        /// </summary>
        /// <param name="installationId">The Id of the installation</param>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories-accessible-to-the-user-for-an-installation</remarks>
        [ManualRoute("GET", "/user/installation/{id}/repositories")]
        public Task<RepositoriesResponse> GetAllRepositoriesForCurrentUser(long installationId, CancellationToken cancellationToken = default)
        {
            return GetAllRepositoriesForCurrentUser(installationId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// List repositories accessible to the user for an installation (requires GitHubApp User-To-Server Auth).
        /// </summary>
        /// <param name="installationId">The Id of the installation</param>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories-accessible-to-the-user-for-an-installation</remarks>
        [ManualRoute("GET", "/user/installation/{id}/repositories")]
        public async Task<RepositoriesResponse> GetAllRepositoriesForCurrentUser(long installationId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var results = await ApiConnection.GetAll<RepositoriesResponse>(ApiUrls.UserInstallationRepositories(installationId), null, options, cancellationToken).ConfigureAwait(false);

            return new RepositoriesResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.Repositories).ToList());
        }
    }
}
