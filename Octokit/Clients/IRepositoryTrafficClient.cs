using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Traffic API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/traffic/">Repository Traffic API documentation</a> for more information.
    /// </remarks>
    public interface IRepositoryTrafficClient
    {
        /// <summary>
        /// List the top 10 referrers over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-referrers</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ExcludeFromPaginationApiOptionsConventionTest("Pagination not supported by GitHub API (tested 29/08/2017)")]
        Task<IReadOnlyList<RepositoryTrafficReferrer>> GetAllReferrers(string owner, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// List the top 10 referrers over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-referrers</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ExcludeFromPaginationApiOptionsConventionTest("Pagination not supported by GitHub API (tested 29/08/2017)")]
        Task<IReadOnlyList<RepositoryTrafficReferrer>> GetAllReferrers(long repositoryId, CancellationToken cancellationToken = default);

        /// <summary>
        /// List the top 10 popular contents over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-paths</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ExcludeFromPaginationApiOptionsConventionTest("Pagination not supported by GitHub API (tested 29/08/2017)")]
        Task<IReadOnlyList<RepositoryTrafficPath>> GetAllPaths(string owner, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// List the top 10 popular contents over the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#list-paths</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ExcludeFromPaginationApiOptionsConventionTest("Pagination not supported by GitHub API (tested 29/08/2017)")]
        Task<IReadOnlyList<RepositoryTrafficPath>> GetAllPaths(long repositoryId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the total number of views and breakdown per day or week for the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#views</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="per">Breakdown per day or week</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<RepositoryTrafficViewSummary> GetViews(string owner, string name, RepositoryTrafficRequest per, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the total number of views and breakdown per day or week for the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#views</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="per">Breakdown per day or week</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<RepositoryTrafficViewSummary> GetViews(long repositoryId, RepositoryTrafficRequest per, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the total number of clones and breakdown per day or week for the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#clones</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="per">Breakdown per day or week</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<RepositoryTrafficCloneSummary> GetClones(string owner, string name, RepositoryTrafficRequest per, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the total number of clones and breakdown per day or week for the last 14 days
        /// </summary>
        /// <remarks>https://developer.github.com/v3/repos/traffic/#clones</remarks>
        /// <param name="repositoryId">The owner of the repository</param>
        /// <param name="per">Breakdown per day or week</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<RepositoryTrafficCloneSummary> GetClones(long repositoryId, RepositoryTrafficRequest per, CancellationToken cancellationToken = default);
    }
}
