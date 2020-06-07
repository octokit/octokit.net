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
        [ManualRoute("GET", "/enterprise/stats/issues")]
        public Task<AdminStatsIssues> GetStatisticsIssues()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsIssues();

            return ApiConnection.Get<AdminStatsIssues>(endpoint);
        }

        /// <summary>
        /// Gets GitHub Enterprise Hook statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsHooks"/> statistics.</returns>
        [ManualRoute("GET", "/enterprise/stats/hooks")]
        public Task<AdminStatsHooks> GetStatisticsHooks()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsHooks();

            return ApiConnection.Get<AdminStatsHooks>(endpoint);
        }

        /// <summary>
        /// Gets GitHub Enterprise Milestone statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsMilestones"/> statistics.</returns>
        [ManualRoute("GET", "/enterprise/stats/milestones")]
        public Task<AdminStatsMilestones> GetStatisticsMilestones()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsMilestones();

            return ApiConnection.Get<AdminStatsMilestones>(endpoint);
        }

        /// <summary>
        /// Gets GitHub Enterprise Organization statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsOrgs"/> statistics.</returns>
        [ManualRoute("GET", "/enterprise/stats/orgs")]
        public Task<AdminStatsOrgs> GetStatisticsOrgs()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsOrgs();

            return ApiConnection.Get<AdminStatsOrgs>(endpoint);
        }

        /// <summary>
        /// Gets GitHub Enterprise Comment statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsComments"/> statistics.</returns>
        [ManualRoute("GET", "/enterprise/stats/comments")]
        public Task<AdminStatsComments> GetStatisticsComments()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsComments();

            return ApiConnection.Get<AdminStatsComments>(endpoint);
        }

        /// <summary>
        /// Gets GitHub Enterprise Pages statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsPages"/> statistics.</returns>
        [ManualRoute("GET", "/enterprise/stats/pages")]
        public Task<AdminStatsPages> GetStatisticsPages()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsPages();

            return ApiConnection.Get<AdminStatsPages>(endpoint);
        }

        /// <summary>
        /// Gets GitHub Enterprise User statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsUsers"/> statistics.</returns>
        [ManualRoute("GET", "/enterprise/stats/users")]
        public Task<AdminStatsUsers> GetStatisticsUsers()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsUsers();

            return ApiConnection.Get<AdminStatsUsers>(endpoint);
        }

        /// <summary>
        /// Gets GitHub Enterprise Gist statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsGists"/> statistics.</returns>
        [ManualRoute("GET", "/enterprise/stats/comments")]
        public Task<AdminStatsGists> GetStatisticsGists()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsGists();

            return ApiConnection.Get<AdminStatsGists>(endpoint);
        }

        /// <summary>
        /// Gets GitHub Enterprise PullRequest statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsPulls"/> statistics.</returns>
        [ManualRoute("GET", "/enterprise/stats/pulls")]
        public Task<AdminStatsPulls> GetStatisticsPulls()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsPulls();

            return ApiConnection.Get<AdminStatsPulls>(endpoint);
        }

        /// <summary>
        /// Gets GitHub Enterprise Repository statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStatsRepos"/> statistics.</returns>
        [ManualRoute("GET", "/enterprise/stats/repos")]
        public Task<AdminStatsRepos> GetStatisticsRepos()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsRepos();

            return ApiConnection.Get<AdminStatsRepos>(endpoint);
        }

        /// <summary>
        /// Gets GitHub Enterprise statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStats"/> collection of statistics.</returns>
        [ManualRoute("GET", "/enterprise/stats/all")]
        public Task<AdminStats> GetStatisticsAll()
        {
            var endpoint = ApiUrls.EnterpriseAdminStatsAll();

            return ApiConnection.Get<AdminStats>(endpoint);
        }
    }
}
