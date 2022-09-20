using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class RateLimitClientTests
    {
        [IntegrationTest]
        public async Task CanRetrieveResourceRateLimits()
        {
            var github = Helper.GetAuthenticatedClient();

            var result = await github.RateLimit.GetRateLimits();

            // Test the core limits
            Assert.True(result.Resources.Core.Limit > 0);
            Assert.True(result.Resources.Core.Remaining > -1);
            Assert.True(result.Resources.Core.Remaining <= result.Resources.Core.Limit);
            Assert.True(result.Resources.Core.ResetAsUtcEpochSeconds > 0);
            Assert.NotEqual(default, result.Resources.Core.Reset);

            // Test the search limits
            Assert.True(result.Resources.Search.Limit > 0);
            Assert.True(result.Resources.Search.Remaining > -1);
            Assert.True(result.Resources.Search.Remaining <= result.Resources.Search.Limit);
            Assert.True(result.Resources.Search.ResetAsUtcEpochSeconds > 0);
            Assert.NotEqual(default, result.Resources.Search.Reset);

            // Test the depreciated rate limits
            Assert.True(result.Rate.Limit > 0);
            Assert.True(result.Rate.Remaining > -1);
            Assert.True(result.Rate.Remaining <= result.Rate.Limit);
            Assert.True(result.Resources.Search.ResetAsUtcEpochSeconds > 0);
            Assert.NotEqual(default, result.Resources.Search.Reset);
        }
    }
}