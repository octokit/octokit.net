namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Enterprise API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/enterprise/">Enterprise API documentation</a> for more information.
    /// </remarks>
    public interface IEnterpriseClient
    {
        /// <summary>
        /// A client for GitHub's Enterprise Admin Stats API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/enterprise/admin_stats/">Enterprise Admin Stats API documentation</a> for more information.
        /// </remarks>
        IEnterpriseAdminStatsClient AdminStats { get; }

        /// <summary>
        /// A client for GitHub's Enterprise LDAP API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/ldap/">Enterprise LDAP API documentation</a> for more information.
        /// </remarks>
        IEnterpriseLdapClient Ldap { get; }

        /// <summary>
        /// A client for GitHub's Enterprise License API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/license/">Enterprise License API documentation</a> for more information.
        /// </remarks>
        IEnterpriseLicenseClient License { get; }

        /// <summary>
        /// A client for GitHub's Enterprise Management Console API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/management_console/">Enterprise Management Console API documentation</a> for more information.
        /// </remarks>
        IEnterpriseManagementConsoleClient ManagementConsole { get; }

        /// <summary>
        /// A client for GitHub's Enterprise Organization API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/orgs/">Enterprise Organization API documentation</a> for more information.
        /// </remarks>
        IEnterpriseOrganizationClient Organization { get; }

        /// <summary>
        /// A client for GitHub's Enterprise Search Indexing API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/search_indexing/">Enterprise Search Indexing API documentation</a> for more information.
        /// </remarks>
        IEnterpriseSearchIndexingClient SearchIndexing { get; }

        /// <summary>
        /// A client for GitHub's Enterprise Pre-receive Environments API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise-admin/pre_receive_environments/">Enterprise Pre-receive Environments API documentation</a> for more information.
        ///</remarks>
        IEnterprisePreReceiveEnvironmentsClient PreReceiveEnvironment { get; }

        /// <summary>
        /// A client for GitHub's Enterprise Pre-receive Hooks API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/enterprise-server@3.3/rest/reference/enterprise-admin#pre-receive-hooks">Enterprise Pre-receive Hooks API documentation</a> for more information.
        ///</remarks>
        IEnterprisePreReceiveHooksClient PreReceiveHook { get; }
    }
}
