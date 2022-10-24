using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using System.Globalization;

namespace Octokit.Tests.Clients
{
    public class RateLimitClientTests
    {
        public class TheGetResourceRateLimitsMethod
        {
            [Fact]
            public async Task RequestsTheResourceRateLimitEndpoint()
            {
                var rateLimit = new MiscellaneousRateLimit(
                     new ResourceRateLimit(
                         new RateLimit(5000, 4999, 1372700873),
                         new RateLimit(30, 18, 1372700873)
                     ),
                     new RateLimit(100, 75, 1372700873)
                 );
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Get<MiscellaneousRateLimit>(Arg.Is<Uri>(u => u.ToString() == "rate_limit")).Returns(Task.FromResult(rateLimit));

                var client = new RateLimitClient(apiConnection);

                var result = await client.GetRateLimits();

                // Test the core limits
                Assert.Equal(5000, result.Resources.Core.Limit);
                Assert.Equal(4999, result.Resources.Core.Remaining);
                Assert.Equal(1372700873, result.Resources.Core.ResetAsUtcEpochSeconds);
                var expectedReset = DateTimeOffset.ParseExact(
                    "Mon 01 Jul 2013 5:47:53 PM -00:00",
                    "ddd dd MMM yyyy h:mm:ss tt zzz",
                    CultureInfo.InvariantCulture);
                Assert.Equal(expectedReset, result.Resources.Core.Reset);

                // Test the search limits
                Assert.Equal(30, result.Resources.Search.Limit);
                Assert.Equal(18, result.Resources.Search.Remaining);
                Assert.Equal(1372700873, result.Resources.Search.ResetAsUtcEpochSeconds);
                expectedReset = DateTimeOffset.ParseExact(
                    "Mon 01 Jul 2013 5:47:53 PM -00:00",
                    "ddd dd MMM yyyy h:mm:ss tt zzz",
                    CultureInfo.InvariantCulture);
                Assert.Equal(expectedReset, result.Resources.Search.Reset);

                // Test the depreciated rate limits
                Assert.Equal(100, result.Rate.Limit);
                Assert.Equal(75, result.Rate.Remaining);
                Assert.Equal(1372700873, result.Rate.ResetAsUtcEpochSeconds);
                expectedReset = DateTimeOffset.ParseExact(
                    "Mon 01 Jul 2013 5:47:53 PM -00:00",
                    "ddd dd MMM yyyy h:mm:ss tt zzz",
                    CultureInfo.InvariantCulture);
                Assert.Equal(expectedReset, result.Rate.Reset);

                apiConnection.Received()
                    .Get<MiscellaneousRateLimit>(Arg.Is<Uri>(u => u.ToString() == "rate_limit"));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new RateLimitClient(null));
            }
        }
    }
}
