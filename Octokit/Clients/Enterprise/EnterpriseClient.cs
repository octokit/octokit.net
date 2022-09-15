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
            Ldap = new EnterpriseLdapClient(apiConnection);
            License = new EnterpriseLicenseClient(apiConnection);
            ManagementConsole = new EnterpriseManagementConsoleClient(apiConnection);
            Organization = new EnterpriseOrganizationClient(apiConnection);
            SearchIndexing = new EnterpriseSearchIndexingClient(apiConnection);
            PreReceiveEnvironment = new EnterprisePreReceiveEnvironmentsClient(apiConnection);
            PreReceiveHook = new EnterprisePreReceiveHooksClient(apiConnection);
        }

        /// <summary>
        /// A client for GitHub's Enterprise Admin Stats API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/enterprise/admin_stats/">Enterprise Admin Stats API documentation</a> for more information.
        /// </remarks>
        public IEnterpriseAdminStatsClient AdminStats { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise LDAP API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/ldap/">Enterprise LDAP API documentation</a> for more information.
        /// </remarks>
        public IEnterpriseLdapClient Ldap { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise License API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/license/">Enterprise License API documentation</a> for more information.
        /// </remarks>
        public IEnterpriseLicenseClient License { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise Management Console API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/management_console/">Enterprise Management Console API documentation</a> for more information.
        /// </remarks>
        public IEnterpriseManagementConsoleClient ManagementConsole { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise Organization API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/orgs/">Enterprise Organization API documentation</a> for more information.
        /// </remarks>
        public IEnterpriseOrganizationClient Organization { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise Search Indexing API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/search_indexing/">Enterprise Search Indexing API documentation</a> for more information.
        /// </remarks>
        public IEnterpriseSearchIndexingClient SearchIndexing { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise Pre-receive Environments API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/">Enterprise Pre-receive Environments API documentation</a> for more information.
        ///</remarks>
        public IEnterprisePreReceiveEnvironmentsClient PreReceiveEnvironment { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise Pre-receive Hooks API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/enterprise-server/rest/reference/enterprise-admin#pre-receive-hooks">Enterprise Pre-receive Hooks API documentation</a> for more information.
        ///</remarks>
        public IEnterprisePreReceiveHooksClient PreReceiveHook { get; private set; }
    }
}
