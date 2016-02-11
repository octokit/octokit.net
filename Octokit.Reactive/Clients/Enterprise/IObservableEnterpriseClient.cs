namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Enterprise API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/enterprise/">Enterprise API documentation</a> for more information.
    /// </remarks>
    public interface IObservableEnterpriseClient
    {
        /// <summary>
        /// A client for GitHub's Enterprise Admin Stats API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/enterprise/admin_stats/">Enterprise Admin Stats API documentation</a> for more information.
        ///</remarks>
        IObservableEnterpriseAdminStatsClient AdminStats { get; }

        /// <summary>
        /// A client for GitHub's Enterprise License API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/license/">Enterprise License API documentation</a> for more information.
        ///</remarks>
        IObservableEnterpriseLicenseClient License { get; }

        /// <summary>
        /// A client for GitHub's Enterprise Organization API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/orgs/">Enterprise Organization API documentation</a> for more information.
        ///</remarks>
        IObservableEnterpriseOrganizationClient Organization { get; }

        /// <summary>
        /// A client for GitHub's Enterprise Search Indexing API
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/enterprise/search_indexing/">Enterprise Search Indexing API documentation</a> for more information.
        ///</remarks>
        IObservableEnterpriseSearchIndexingClient SearchIndexing { get; }
    }
}
