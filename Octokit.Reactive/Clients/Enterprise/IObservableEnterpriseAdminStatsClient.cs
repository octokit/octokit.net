using System;
using System.Diagnostics.CodeAnalysis;

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
        /// Gets GitHub Enterprise Issue statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsIssues"/> statistics.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<AdminStatsIssues> GetStatisticsIssues();

        /// <summary>
        /// Gets GitHub Enterprise Hook statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsHooks"/> statistics.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<AdminStatsHooks> GetStatisticsHooks();

        /// <summary>
        /// Gets GitHub Enterprise Milestone statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsMilestones"/> statistics.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<AdminStatsMilestones> GetStatisticsMilestones();

        /// <summary>
        /// Gets GitHub Enterprise Organization statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsOrgs"/> statistics.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<AdminStatsOrgs> GetStatisticsOrgs();

        /// <summary>
        /// Gets GitHub Enterprise Comment statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsComments"/> statistics.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<AdminStatsComments> GetStatisticsComments();

        /// <summary>
        /// Gets GitHub Enterprise Pages statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsPages"/> statistics.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<AdminStatsPages> GetStatisticsPages();

        /// <summary>
        /// Gets GitHub Enterprise User statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsUsers"/> statistics.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<AdminStatsUsers> GetStatisticsUsers();

        /// <summary>
        /// Gets GitHub Enterprise Gist statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsGists"/> statistics.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<AdminStatsGists> GetStatisticsGists();

        /// <summary>
        /// Gets GitHub Enterprise PullRequest statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsPulls"/> statistics.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<AdminStatsPulls> GetStatisticsPulls();

        /// <summary>
        /// Gets GitHub Enterprise Repository statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsRepos"/> statistics.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<AdminStatsRepos> GetStatisticsRepos();

        /// <summary>
        /// Gets GitHub Enterprise statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStats"/> collection of statistics.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<AdminStats> GetStatisticsAll();
    }
}
