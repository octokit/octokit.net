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
            License = new ObservableEnterpriseLicenseClient(client);
            Organization = new ObservableEnterpriseOrganizationClient(client);
            SearchIndexing = new ObservableEnterpriseSearchIndexingClient(client);
        }

        /// <summary>
        /// A client for GitHub's Enterprise Admin Stats API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/enterprise/admin_stats/">Enterprise Admin Stats API documentation</a> for more information.
        ///</remarks>
        public IObservableEnterpriseAdminStatsClient AdminStats { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise License API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/license/">Enterprise License API documentation</a> for more information.
        ///</remarks>
        public IObservableEnterpriseLicenseClient License { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise Organization API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/orgs/">Enterprise Organization API documentation</a> for more information.
        ///</remarks>
        public IObservableEnterpriseOrganizationClient Organization { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise Search Indexing API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/search_indexing/">Enterprise Search Indexing API documentation</a> for more information.
        ///</remarks>
        public IObservableEnterpriseSearchIndexingClient SearchIndexing { get; private set; }
    }
}
