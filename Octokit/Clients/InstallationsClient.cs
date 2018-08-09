﻿using System.Linq;
using System.Threading.Tasks;

namespace Octokit.Clients
{
    /// <summary>
    /// A client for GitHub Applications Installations API. Provides the methods required to get GitHub Apps installations.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/apps/installations/">GitHub Apps Installations API documentation</a> for more information.
    /// </remarks>
    public class GitHubAppsInstallationsClient : ApiClient, IGitHubAppsInstallationsClient
    {
        public GitHubAppsInstallationsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// List repositories of the authenticated GitHub App (requires GitHubApp JWT token auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories</remarks>
        public async Task<RepositoriesResponse> GetAllRepositoriesForCurrent()
        {
            var results = await ApiConnection.GetAll<RepositoriesResponse>(ApiUrls.InstallationRepositories(), null, AcceptHeaders.GitHubAppsPreview).ConfigureAwait(false);

            return new RepositoriesResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.Repositories).ToList());
        }

        /// <summary>
        /// List repositories of the authenticated GitHub App (requires GitHubApp JWT token auth).
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories</remarks>
        public async Task<RepositoriesResponse> GetAllRepositoriesForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var results = await ApiConnection.GetAll<RepositoriesResponse>(ApiUrls.InstallationRepositories(), null, AcceptHeaders.GitHubAppsPreview, options).ConfigureAwait(false);

            return new RepositoriesResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.Repositories).ToList());
        }

        /// <summary>
        /// List repositories accessible to the user for an installation.
        /// </summary>
        /// <param name="installationId"></param>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories-accessible-to-the-user-for-an-installation</remarks>
        public async Task<RepositoriesResponse> GetAllRepositoriesForUser(long installationId)
        {
            var results = await ApiConnection.GetAll<RepositoriesResponse>(ApiUrls.UserInstallationRepositories(installationId), null, AcceptHeaders.GitHubAppsPreview).ConfigureAwait(false);

            return new RepositoriesResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.Repositories).ToList());
        }

        /// <summary>
        /// List repositories accessible to the user for an installation.
        /// </summary>
        /// <param name="installationId"></param>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories-accessible-to-the-user-for-an-installation</remarks>
        public async Task<RepositoriesResponse> GetAllRepositoriesForUser(long installationId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var results = await ApiConnection.GetAll<RepositoriesResponse>(ApiUrls.UserInstallationRepositories(installationId), null, AcceptHeaders.GitHubAppsPreview, options).ConfigureAwait(false);

            return new RepositoriesResponse(
                results.Count > 0 ? results.Max(x => x.TotalCount) : 0,
                results.SelectMany(x => x.Repositories).ToList());
        }
    }
}