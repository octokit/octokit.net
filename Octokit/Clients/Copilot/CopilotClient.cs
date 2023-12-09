using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Copilot for Business API.
    /// Allows listing, creating, and deleting Copilot licenses.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/copilot/copilot-business?apiVersion=2022-11-28">Copilot for Business API documentation</a> for more information.
    /// </remarks>
    public class CopilotClient : ApiClient, ICopilotClient
    {
        /// <summary>
        /// Instantiates a new GitHub Copilot API client.
        /// </summary>
        /// <param name="apiConnection"></param>
        public CopilotClient(IApiConnection apiConnection) : base(apiConnection)
        {
            Licensing = new CopilotLicenseClient(apiConnection);
        }
        
        /// <summary>
        /// Returns a summary of the Copilot for Business configuration for an organization. Includes a seat 
        /// details summary of the current billing cycle, and the mode of seat management.
        /// </summary>
        /// <param name="organization">the organization name to retrieve billing settings for</param>
        /// <returns>A <see cref="BillingSettings"/> instance</returns>
        [ManualRoute("GET", "/orgs/{org}/copilot/billing")]
        public async Task<BillingSettings> GetSummaryForOrganization(string organization)
        {
            Ensure.ArgumentNotNull(organization, nameof(organization));
            
            return await ApiConnection.Get<BillingSettings>(ApiUrls.CopilotBillingSettings(organization));
        }
        
        /// <summary>
        /// Client for maintaining Copilot licenses for users in an organization.
        /// </summary>
        public ICopilotLicenseClient Licensing { get; private set; }
    }
}