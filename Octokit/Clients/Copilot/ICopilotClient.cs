using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Access GitHub's Copilot for Business API.
    /// </summary>
    public interface ICopilotClient
    {
        /// <summary>
        /// Returns a summary of the Copilot for Business configuration for an organization. Includes a seat 
        /// details summary of the current billing cycle, and the mode of seat management.
        /// </summary>
        /// <param name="organization">the organization name to retrieve billing settings for</param>
        /// <returns>A <see cref="BillingSettings"/> instance</returns>
        Task<BillingSettings> GetSummaryForOrganization(string organization);
        
        /// <summary>
        /// For checking and managing licenses for GitHub Copilot for Business
        /// </summary>
        ICopilotLicenseClient Licensing { get; }
    }
}
