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
            Ensure.ArgumentNotNull(client, nameof(client));

            AdminStats = new ObservableEnterpriseAdminStatsClient(client);
            Ldap = new ObservableEnterpriseLdapClient(client);
            License = new ObservableEnterpriseLicenseClient(client);
            ManagementConsole = new ObservableEnterpriseManagementConsoleClient(client);
            Organization = new ObservableEnterpriseOrganizationClient(client);
            SearchIndexing = new ObservableEnterpriseSearchIndexingClient(client);
            PreReceiveEnvironment = new ObservableEnterprisePreReceiveEnvironmentsClient(client);
        }

        /// <summary>
        /// A client for GitHub's Enterprise Admin Stats API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/enterprise/admin_stats/">Enterprise Admin Stats API documentation</a> for more information.
        /// </remarks>
        public IObservableEnterpriseAdminStatsClient AdminStats { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise LDAP API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/ldap/">Enterprise LDAP API documentation</a> for more information.
        /// </remarks>
        public IObservableEnterpriseLdapClient Ldap { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise License API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/license/">Enterprise License API documentation</a> for more information.
        /// </remarks>
        public IObservableEnterpriseLicenseClient License { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise Management Console API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/management_console/">Enterprise Management Console API documentation</a> for more information.
        /// </remarks>
        public IObservableEnterpriseManagementConsoleClient ManagementConsole { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise Organization API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/orgs/">Enterprise Organization API documentation</a> for more information.
        /// </remarks>
        public IObservableEnterpriseOrganizationClient Organization { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise Search Indexing API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/search_indexing/">Enterprise Search Indexing API documentation</a> for more information.
        /// </remarks>
        public IObservableEnterpriseSearchIndexingClient SearchIndexing { get; private set; }

        /// <summary>
        /// A client for GitHub's Enterprise Pre-receive Environments API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/">Enterprise Pre-receive Environments API documentation</a> for more information.
        ///</remarks>
        public IObservableEnterprisePreReceiveEnvironmentsClient PreReceiveEnvironment { get; private set; }
    }
}
