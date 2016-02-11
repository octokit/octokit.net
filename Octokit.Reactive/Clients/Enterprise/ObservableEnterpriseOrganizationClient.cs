using System;
using System.Reactive.Threading.Tasks;
using Octokit;


namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Enterprise Organization API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise/orgs/">Enterprise Organization API documentation</a> for more information.
    ///</remarks>
    public class ObservableEnterpriseOrganizationClient : IObservableEnterpriseOrganizationClient
    {
        readonly IEnterpriseOrganizationClient _client;

        public ObservableEnterpriseOrganizationClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Enterprise.Organization;
        }

        /// <summary>
        /// Creates an Organization on a GitHub Enterprise appliance (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/orgs/#create-an-organization
        /// </remarks>
        /// <param name="newOrganization">A <see cref="NewOrganization"/> instance describing the organization to be created</param>
        /// <returns>The <see cref="Organization"/> created.</returns>
        public IObservable<Organization> Create(NewOrganization newOrganization)
        {
            return _client.Create(newOrganization).ToObservable();
        }
    }
}
