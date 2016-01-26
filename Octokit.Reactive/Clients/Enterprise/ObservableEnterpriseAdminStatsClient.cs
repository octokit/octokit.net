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
        /// Gets GitHub Enterprise Issue statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsIssues"/> statistics.</returns>
        public IObservable<AdminStatsIssues> GetStatisticsIssues()
        {
            return _client.GetStatisticsIssues().ToObservable();
        }

        /// <summary>
        /// Gets GitHub Enterprise Hook statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsHooks"/> statistics.</returns>
        public IObservable<AdminStatsHooks> GetStatisticsHooks()
        {
            return _client.GetStatisticsHooks().ToObservable();
        }

        /// <summary>
        /// Gets GitHub Enterprise Milestone statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsMilestones"/> statistics.</returns>
        public IObservable<AdminStatsMilestones> GetStatisticsMilestones()
        {
            return _client.GetStatisticsMilestones().ToObservable();
        }

        /// <summary>
        /// Gets GitHub Enterprise Organization statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsOrgs"/> statistics.</returns>
        public IObservable<AdminStatsOrgs> GetStatisticsOrgs()
        {
            return _client.GetStatisticsOrgs().ToObservable();
        }

        /// <summary>
        /// Gets GitHub Enterprise Comment statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsComments"/> statistics.</returns>
        public IObservable<AdminStatsComments> GetStatisticsComments()
        {
            return _client.GetStatisticsComments().ToObservable();
        }

        /// <summary>
        /// Gets GitHub Enterprise Pages statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsPages"/> statistics.</returns>
        public IObservable<AdminStatsPages> GetStatisticsPages()
        {
            return _client.GetStatisticsPages().ToObservable();
        }

        /// <summary>
        /// Gets GitHub Enterprise User statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsUsers"/> statistics.</returns>
        public IObservable<AdminStatsUsers> GetStatisticsUsers()
        {
            return _client.GetStatisticsUsers().ToObservable();
        }

        /// <summary>
        /// Gets GitHub Enterprise Gist statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsGists"/> statistics.</returns>
        public IObservable<AdminStatsGists> GetStatisticsGists()
        {
            return _client.GetStatisticsGists().ToObservable();
        }

        /// <summary>
        /// Gets GitHub Enterprise PullRequest statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsPulls"/> statistics.</returns>
        public IObservable<AdminStatsPulls> GetStatisticsPulls()
        {
            return _client.GetStatisticsPulls().ToObservable();
        }

        /// <summary>
        /// Gets GitHub Enterprise Repository statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsRepos"/> statistics.</returns>
        public IObservable<AdminStatsRepos> GetStatisticsRepos()
        {
            return _client.GetStatisticsRepos().ToObservable();
        }

        /// <summary>
        /// Gets GitHub Enterprise statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStats"/> collection of statistics.</returns>
        public IObservable<AdminStats> GetStatisticsAll()
        {
            return _client.GetStatisticsAll().ToObservable();
        }
    }
}
