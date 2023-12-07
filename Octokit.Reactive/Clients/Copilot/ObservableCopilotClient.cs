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
            License = new ObservableCopilotLicenseClient(client);
        }
        
        /// <summary>
        /// Returns the top level billing settings for an organization. 
        /// </summary>
        /// <param name="organization">the organization name to retrieve billing settings for</param>
        /// <returns>A <see cref="BillingSettings"/> instance</returns>
        public IObservable<BillingSettings> Get(string organization)
        {
            Ensure.ArgumentNotNull(organization, nameof(organization));
            
            return _client.Get(organization).ToObservable();
        }
        
        /// <summary>
        /// Client for maintaining Copilot licenses for users in an organization.
        /// </summary>
        public IObservableCopilotLicenseClient License { get; private set; }
    }
}