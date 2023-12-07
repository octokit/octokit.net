using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// Access GitHub's Copilot for Business API.
    /// </summary>
    public interface IObservableCopilotClient
    {
        /// <summary>
        /// Returns the top level billing settings for an organization. 
        /// </summary>
        /// <param name="organization">the organization name to retrieve billing settings for</param>
        /// <returns>A <see cref="BillingSettings"/> instance</returns>
        IObservable<BillingSettings> Get(string organization);
        
        /// <summary>
        /// For checking and managing licenses for GitHub Copilot for Business
        /// </summary>
        IObservableCopilotLicenseClient License { get; }
    }
}
