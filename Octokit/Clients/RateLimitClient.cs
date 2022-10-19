using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's rate-limit APIs.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/rate-limit">Rate-Limit API documentation</a> for more details.
    /// </remarks>
    public class RateLimitClient : ApiClient, IRateLimitClient
    {
        /// <summary>
        ///     Initializes a new GitHub rate-limit API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public RateLimitClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets API Rate Limits (API service rather than header info).
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>An <see cref="MiscellaneousRateLimit"/> of Rate Limits.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        [ManualRoute("GET", "/rate_limit")]
        public Task<MiscellaneousRateLimit> GetRateLimits()
        {
            return ApiConnection.Get<MiscellaneousRateLimit>(ApiUrls.RateLimit());
        }
    }
}