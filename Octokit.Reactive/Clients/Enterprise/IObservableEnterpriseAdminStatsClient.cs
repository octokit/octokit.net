using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Enterprise Admin Stats API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise/admin_stats/">Enterprise Admin Stats API documentation</a> for more information.
    ///</remarks>
    public interface IObservableEnterpriseAdminStatsClient
    {
        /// <summary>
        /// Gets GitHub Enterprise statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        ///https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStats"/> collection for the requested <see cref="AdminStatsType"/> type.</returns>
        IObservable<AdminStats> GetStatistics(AdminStatsType type);
    }
}
