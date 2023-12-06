using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Access GitHub's Copilot for Business API.
    /// </summary>
    public interface ICopilotClient
    {
        /// <summary>
        /// Returns the top level billing settings for an organization. 
        /// </summary>
        /// <param name="organization">the organization name to retrieve billing settings for</param>
        /// <returns>A <see cref="BillingSettings"/> instance</returns>
        Task<BillingSettings> Get(string organization);
        
        /// <summary>
        /// For checking and managing licenses for GitHub Copilot for Business
        /// </summary>
        ICopilotLicenseClient License { get; }
    }
}
