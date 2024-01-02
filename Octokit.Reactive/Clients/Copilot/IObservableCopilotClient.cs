﻿using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// Access GitHub's Copilot for Business API.
    /// </summary>
    public interface IObservableCopilotClient
    {
        /// <summary>
        /// Returns a summary of the Copilot for Business configuration for an organization. Includes a seat 
        /// details summary of the current billing cycle, and the mode of seat management.
        /// </summary>
        /// <param name="organization">the organization name to retrieve billing settings for</param>
        /// <returns>A <see cref="BillingSettings"/> instance</returns>
        IObservable<BillingSettings> GetSummaryForOrganization(string organization);
        
        /// <summary>
        /// For checking and managing licenses for GitHub Copilot for Business
        /// </summary>
        IObservableCopilotLicenseClient Licensing { get; }
    }
}
