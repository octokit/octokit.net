using System;

namespace Octokit.Reactive
{
    public interface IObservableGitHubAppsInstallationsClient
    {
        /// <summary>
        /// List repositories of the authenticated GitHub App (requires GitHubApp JWT token auth).
        /// </summary>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories</remarks>
        IObservable<RepositoriesResponse> GetAllRepositoriesForCurrent();

        /// <summary>
        /// List repositories of the authenticated GitHub App (requires GitHubApp JWT token auth).
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories</remarks>
        IObservable<RepositoriesResponse> GetAllRepositoriesForCurrent(ApiOptions options);

        /// <summary>
        /// List repositories accessible to the user for an installation.
        /// </summary>
        /// <param name="installationId">The id of the installation</param>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories-accessible-to-the-user-for-an-installation</remarks>
        IObservable<RepositoriesResponse> GetAllRepositoriesForUser(long installationId);

        /// <summary>
        /// List repositories accessible to the user for an installation.
        /// </summary>
        /// <param name="installationId">The id of the installation</param>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>https://developer.github.com/v3/apps/installations/#list-repositories-accessible-to-the-user-for-an-installation</remarks>
        IObservable<RepositoriesResponse> GetAllRepositoriesForUser(long installationId, ApiOptions options);
    }
}