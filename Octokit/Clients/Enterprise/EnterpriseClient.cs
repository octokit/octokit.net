namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Enterprise API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/enterprise/">Enterprise API documentation</a> for more information.
    /// </remarks>
    public class EnterpriseClient : ApiClient, IEnterpriseClient
    {
        /// <summary>
        /// Instantiates a new GitHub Enterprise API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public EnterpriseClient(IApiConnection apiConnection) : base(apiConnection)
        {
            AdminStats = new EnterpriseAdminStatsClient(apiConnection);
            License = new EnterpriseLicenseClient(apiConnection);
            Organization = new EnterpriseOrganizationClient(apiConnection);
            SearchIndexing = new EnterpriseSearchIndexingClient(apiConnection);
        }

        /// <summary>
        /// A client for GitHub's Enterprise Admin Stats API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/enterprise/admin_stats/">Enterprise Admin Stats API documentation</a> for more information.
        ///</remarks>
        public IEnterpriseAdminStatsClient AdminStats { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise License API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/license/">Enterprise License API documentation</a> for more information.
        ///</remarks>
        public IEnterpriseLicenseClient License { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise Organization API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/orgs/">Enterprise Organization API documentation</a> for more information.
        ///</remarks>
        public IEnterpriseOrganizationClient Organization { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise Search Indexing API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/search_indexing/">Enterprise Search Indexing API documentation</a> for more information.
        ///</remarks>
        public IEnterpriseSearchIndexingClient SearchIndexing { get; private set; }
    }
}
