using Octokit.Internal;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Xunit;

using static Octokit.Internal.TestSetup;
using Octokit.Tests.Helpers;

namespace Octokit.Tests.Exceptions
{
    public class RateLimitExceededExceptionTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ParsesRateLimitsFromHeaders()
            {
                var headers = new Dictionary<string, string>
                {
                    {"X-RateLimit-Limit", "100"},
                    {"X-RateLimit-Remaining", "42"},
                    {"X-RateLimit-Reset", "1372700873"}
                };
                var response = CreateResponse(HttpStatusCode.Forbidden, headers);

                var exception = new RateLimitExceededException(response);

                Assert.Equal(HttpStatusCode.Forbidden, exception.StatusCode);
                Assert.Equal(100, exception.Limit);
                Assert.Equal(42, exception.Remaining);
                var expectedReset = DateTimeOffset.ParseExact(
                    "Mon 01 Jul 2013 5:47:53 PM -00:00",
                    "ddd dd MMM yyyy h:mm:ss tt zzz",
                    CultureInfo.InvariantCulture);
                Assert.Equal("API Rate Limit exceeded", exception.Message);
                Assert.Equal(expectedReset, exception.Reset);
            }

            [Fact]
            public void HandlesInvalidHeaderValues()
            {
                var headers = new Dictionary<string, string>
                {
                    {"X-RateLimit-Limit", "XXX"},
                    {"X-RateLimit-Remaining", "XXXX"},
                    {"X-RateLimit-Reset", "XXXX"},
                    {"Date", "XXX"},
                    {ApiInfoParser.ReceivedTimeHeaderName, "XXX"},
                };
                var response = CreateResponse(HttpStatusCode.Forbidden, headers);

                var exception = new RateLimitExceededException(response);

                Assert.Equal(HttpStatusCode.Forbidden, exception.StatusCode);
                Assert.Equal(0, exception.Limit);
                Assert.Equal(0, exception.Remaining);
                var expectedReset = DateTimeOffset.ParseExact(
                    "Thu 01 Jan 1970 0:00:00 AM -00:00",
                    "ddd dd MMM yyyy h:mm:ss tt zzz",
                    CultureInfo.InvariantCulture);
                Assert.Equal(expectedReset, exception.Reset);
                Assert.Equal(TimeSpan.Zero, exception.GetRetryAfterTimeSpan());
            }

            [Fact]
            public void HandlesMissingHeaderValues()
            {
                var response = CreateResponse(HttpStatusCode.Forbidden);
                var exception = new RateLimitExceededException(response);

                Assert.Equal(HttpStatusCode.Forbidden, exception.StatusCode);
                Assert.Equal(0, exception.Limit);
                Assert.Equal(0, exception.Remaining);
                var expectedReset = DateTimeOffset.ParseExact(
                    "Thu 01 Jan 1970 0:00:00 AM -00:00",
                    "ddd dd MMM yyyy h:mm:ss tt zzz",
                    CultureInfo.InvariantCulture);
                Assert.Equal(expectedReset, exception.Reset);
                Assert.Equal(TimeSpan.Zero, exception.GetRetryAfterTimeSpan());
            }

            [Fact]
            public void CanPopulateObjectFromSerializedData()
            {
                var headers = new Dictionary<string, string>{
                    {"X-RateLimit-Limit", "100"},
                    {"X-RateLimit-Remaining", "42"},
                    {"X-RateLimit-Reset", "1372700873"}
                };
                var response = CreateResponse(HttpStatusCode.Forbidden, headers);

                var exception = new RateLimitExceededException(response);

                var deserialized = BinaryFormatterExtensions.SerializeAndDeserializeObject(exception);

                Assert.Equal(HttpStatusCode.Forbidden, deserialized.StatusCode);
                Assert.Equal(100, deserialized.Limit);
                Assert.Equal(42, deserialized.Remaining);
                var expectedReset = DateTimeOffset.ParseExact(
                    "Mon 01 Jul 2013 5:47:53 PM -00:00",
                    "ddd dd MMM yyyy h:mm:ss tt zzz",
                    CultureInfo.InvariantCulture);
                Assert.Equal(expectedReset, deserialized.Reset);
            }
        }

        public class GetRetryAfterTimeSpanMethod
        {
            [Fact]
            public void ReturnsSkewedDistanceFromReset()
            {
                // One hour into the future is hopefully long enough to still be
                // in the future at the end of this method
                var resetTime = DateTimeOffset.Now + TimeSpan.FromHours(1);
                var recvDate = new DateTimeOffset(2020, 06, 07, 12, 00, 00, TimeSpan.Zero);
                var skew = TimeSpan.FromSeconds(-5);

                var response = CreateResponse(HttpStatusCode.Forbidden,
                    new Dictionary<string, string>
                    {
                        ["X-RateLimit-Limit"] = "100",
                        ["X-RateLimit-Remaining"] = "0",
                        ["X-RateLimit-Reset"] = resetTime.ToUnixTimeSeconds()
                            .ToString(CultureInfo.InvariantCulture),
                        ["Date"] = (recvDate + skew).ToString("r"),
                        [ApiInfoParser.ReceivedTimeHeaderName] = recvDate.ToString("r"),
                    });
                Assert.Equal(skew, response.ApiInfo.ServerTimeDifference);
                var except = new RateLimitExceededException(response);

                var timeToReset = except.GetRetryAfterTimeSpan();
                Assert.NotEqual(TimeSpan.Zero, timeToReset);
                Assert.InRange(timeToReset, TimeSpan.Zero, TimeSpan.FromHours(1));
            }

            [Fact]
            public void ReturnsZeroIfSkewedResetInPast()
            {
                var beginTime = DateTimeOffset.Now;
                var resetTime = beginTime - TimeSpan.FromHours(1);

                var response = CreateResponse(HttpStatusCode.Forbidden,
                    new Dictionary<string, string>
                    {
                        ["X-RateLimit-Limit"] = "100",
                        ["X-RateLimit-Remaining"] = "0",
                        ["X-RateLimit-Reset"] = resetTime.ToUnixTimeSeconds()
                            .ToString(CultureInfo.InvariantCulture),
                        ["Date"] = beginTime.ToString("r"),
                        [ApiInfoParser.ReceivedTimeHeaderName] = beginTime.ToString("r"),
                    });
                Assert.Equal(TimeSpan.Zero, response.ApiInfo.ServerTimeDifference);
                var except = new RateLimitExceededException(response);

                var timeToReset = except.GetRetryAfterTimeSpan();
                Assert.Equal(TimeSpan.Zero, timeToReset);
            }
        }
    }
}
