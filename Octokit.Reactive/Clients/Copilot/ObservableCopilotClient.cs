using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Copilot for Business API.
    /// Allows listing, creating, and deleting Copilot licenses.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/copilot/copilot-business?apiVersion=2022-11-28">Copilot for Business API documentation</a> for more information.
    /// </remarks>
    public class ObservableCopilotClient : IObservableCopilotClient
    {
        private readonly ICopilotClient _client;

        /// <summary>
        /// Instantiates a new GitHub Copilot API client.
        /// </summary>
        /// <param name="client"></param>
        public ObservableCopilotClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));
            
            _client = client.Copilot;
            Licensing = new ObservableCopilotLicenseClient(client);
        }
        
        /// <summary>
        /// Returns a summary of the Copilot for Business configuration for an organization. Includes a seat 
        /// details summary of the current billing cycle, and the mode of seat management.
        /// </summary>
        /// <param name="organization">the organization name to retrieve billing settings for</param>
        /// <returns>A <see cref="BillingSettings"/> instance</returns>
        public IObservable<BillingSettings> GetSummaryForOrganization(string organization)
        {
            Ensure.ArgumentNotNull(organization, nameof(organization));
            
            return _client.GetSummaryForOrganization(organization).ToObservable();
        }
        
        /// <summary>
        /// Client for maintaining Copilot licenses for users in an organization.
        /// </summary>
        public IObservableCopilotLicenseClient Licensing { get; private set; }
    }
}