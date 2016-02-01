using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Enterprise Admin Stats API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise/admin_stats/">Enterprise Admin Stats API documentation</a> for more information.
    ///</remarks>
    public class EnterpriseAdminStatsClient : ApiClient, IEnterpriseAdminStatsClient
    {
        public EnterpriseAdminStatsClient(IApiConnection apiConnection)
            : base(apiConnection)
        { }

        /// <summary>
        /// Gets GitHub Enterprise Issue statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsIssues"/> statistics.</returns>
        public async Task<AdminStatsIssues> GetStatisticsIssues()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsIssues();

            return await ApiConnection.Get<AdminStatsIssues>(endpoint)
                                                    .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets GitHub Enterprise Hook statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsHooks"/> statistics.</returns>
        public async Task<AdminStatsHooks> GetStatisticsHooks()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsHooks();

            return await ApiConnection.Get<AdminStatsHooks>(endpoint)
                                                   .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets GitHub Enterprise Milestone statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsMilestones"/> statistics.</returns>
        public async Task<AdminStatsMilestones> GetStatisticsMilestones()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsMilestones();

            return await ApiConnection.Get<AdminStatsMilestones>(endpoint)
                                                        .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets GitHub Enterprise Organization statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsOrgs"/> statistics.</returns>
        public async Task<AdminStatsOrgs> GetStatisticsOrgs()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsOrgs();

            return await ApiConnection.Get<AdminStatsOrgs>(endpoint)
                                                  .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets GitHub Enterprise Comment statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsComments"/> statistics.</returns>
        public async Task<AdminStatsComments> GetStatisticsComments()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsComments();

            return await ApiConnection.Get<AdminStatsComments>(endpoint)
                                                      .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets GitHub Enterprise Pages statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsPages"/> statistics.</returns>
        public async Task<AdminStatsPages> GetStatisticsPages()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsPages();

            return await ApiConnection.Get<AdminStatsPages>(endpoint)
                                                   .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets GitHub Enterprise User statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsUsers"/> statistics.</returns>
        public async Task<AdminStatsUsers> GetStatisticsUsers()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsUsers();

            return await ApiConnection.Get<AdminStatsUsers>(endpoint)
                                                   .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets GitHub Enterprise Gist statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsGists"/> statistics.</returns>
        public async Task<AdminStatsGists> GetStatisticsGists()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsGists();

            return await ApiConnection.Get<AdminStatsGists>(endpoint)
                                                   .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets GitHub Enterprise PullRequest statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsPulls"/> statistics.</returns>
        public async Task<AdminStatsPulls> GetStatisticsPulls()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsPulls();

            return await ApiConnection.Get<AdminStatsPulls>(endpoint)
                                                   .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets GitHub Enterprise Repository statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsRepos"/> statistics.</returns>
        public async Task<AdminStatsRepos> GetStatisticsRepos()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsRepos();

            return await ApiConnection.Get<AdminStatsRepos>(endpoint)
                                                   .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets GitHub Enterprise statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStats"/> collection of statistics.</returns>
        public async Task<AdminStats> GetStatisticsAll()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsAll();

            return await ApiConnection.Get<AdminStats>(endpoint)
                                          .ConfigureAwait(false);
        }
    }
}
