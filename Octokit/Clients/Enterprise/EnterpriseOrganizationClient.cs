using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Enterprise Organization API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise/orgs/">Enterprise Organization API documentation</a> for more information.
    ///</remarks>
    public class EnterpriseOrganizationClient : ApiClient, IEnterpriseOrganizationClient
    {
        public EnterpriseOrganizationClient(IApiConnection apiConnection)
            : base(apiConnection)
        { }

        /// <summary>
        /// Creates an Organization on a GitHub Enterprise appliance (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/enterprise/orgs/#create-an-organization
        /// </remarks>
        /// <param name="newOrganization">A <see cref="NewOrganization"/> instance describing the organization to be created</param>
        /// <returns>The <see cref="Organization"/> created.</returns>
        public async Task<Organization> Create(NewOrganization newOrganization)
        {
            Ensure.ArgumentNotNull(newOrganization, "newOrganization");

            var endpoint = ApiUrls.EnterpriseOrganization();

            return await ApiConnection.Post<Organization>(endpoint, newOrganization);
        }
    }
}
