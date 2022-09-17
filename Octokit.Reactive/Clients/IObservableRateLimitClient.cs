using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's rate-limit APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/rate-limit">Rate-Limit API documentation</a> for more details.
    /// </remarks>
    public interface IObservableRateLimitClient
    {
        /// <summary>
        /// Gets API Rate Limits (API service rather than header info).
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="MiscellaneousRateLimit"/> of Rate Limits.</returns>
        IObservable<MiscellaneousRateLimit> GetRateLimits();
    }
}
