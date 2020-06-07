using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
#if !NO_SERIALIZABLE
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
#endif
using Xunit;

using static Octokit.Internal.TestSetup;

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
                    {"X-RateLimit-Reset", "XXXX"}
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
            }

#if !NO_SERIALIZABLE
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

                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, exception);
                    stream.Position = 0;
                    var deserialized = (RateLimitExceededException)formatter.Deserialize(stream);

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
#endif
        }
    }
}
