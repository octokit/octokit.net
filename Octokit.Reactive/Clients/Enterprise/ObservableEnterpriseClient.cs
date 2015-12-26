namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Enterprise API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/enterprise/">Enterprise API documentation</a> for more information.
    /// </remarks>
    public class ObservableEnterpriseClient : IObservableEnterpriseClient
    {
        public ObservableEnterpriseClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            AdminStats = new ObservableEnterpriseAdminStatsClient(client);
        }

        /// <summary>
        /// A client for GitHub's Enterprise Admin Stats API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/enterprise/admin_stats/">Enterprise Admin Stats API documentation</a> for more information.
        ///</remarks>
        public IObservableEnterpriseAdminStatsClient AdminStats { get; private set; }
    }
}
