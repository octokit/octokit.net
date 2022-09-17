using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's rate-limit APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/rate-limit">Rate-Limit API documentation</a> for more details.
    /// </remarks>
    public class ObservableRateLimitClient : IObservableRateLimitClient
    {
        private readonly IRateLimitClient _client;

        public ObservableRateLimitClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.RateLimit;
        }

        /// <summary>
        /// Gets API Rate Limits (API service rather than header info).
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="MiscellaneousRateLimit"/> of Rate Limits.</returns>
        public IObservable<MiscellaneousRateLimit> GetRateLimits()
        {
            return _client.GetRateLimits().ToObservable();
        }
    }
}
