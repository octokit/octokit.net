using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Enterprise Admin Stats API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise/admin_stats/">Enterprise Admin Stats API documentation</a> for more information.
    ///</remarks>
    public class ObservableEnterpriseAdminStatsClient : IObservableEnterpriseAdminStatsClient
    {
        readonly IEnterpriseAdminStatsClient _client;

        public ObservableEnterpriseAdminStatsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Enterprise.AdminStats;
        }

        /// <summary>
        /// Gets GitHub Enterprise statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        ///https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStats"/> collection for the requested <see cref="AdminStatsType"/> type.</returns>
        public IObservable<AdminStats> GetStatistics(AdminStatsType type)
        {
            return _client.GetStatistics(type).ToObservable();
        }
    }
}
