using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Xunit;

namespace Octokit.Tests.Http
{
    public class RateLimitTests
    {
        public class TheConstructor
        {
            public void Foo()
            {
                Console.WriteLine(new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).Ticks);
            }

            [Fact]
            public void ParsesRateLimitsFromHeaders()
            {
                var headers = new Dictionary<string, string> 
                {
                    { "X-RateLimit-Limit", "100" },
                    { "X-RateLimit-Remaining", "42" },
                    { "X-RateLimit-Reset", "1372700873" }
                };

                var rateLimit = new RateLimit(headers);

                Assert.Equal(100, rateLimit.Limit);
                Assert.Equal(42, rateLimit.Remaining);
                var expectedReset = DateTimeOffset.ParseExact(
                    "Mon 01 Jul 2013 5:47:53 PM -00:00",
                    "ddd dd MMM yyyy h:mm:ss tt zzz",
                    CultureInfo.InvariantCulture);
                Assert.Equal(expectedReset, rateLimit.Reset);
            }

            [Fact]
            public void HandlesInvalidHeaderValues()
            {
                var headers = new Dictionary<string, string> 
                {
                    { "X-RateLimit-Limit", "1234scoobysnacks1234" },
                    { "X-RateLimit-Remaining", "xanadu" },
                    { "X-RateLimit-Reset", "garbage" }
                };

                var rateLimit = new RateLimit(headers);

                Assert.Equal(0, rateLimit.Limit);
                Assert.Equal(0, rateLimit.Remaining);
                var expectedReset = DateTimeOffset.ParseExact(
                    "Thu 01 Jan 1970 0:00:00 AM -00:00",
                    "ddd dd MMM yyyy h:mm:ss tt zzz",
                    CultureInfo.InvariantCulture);
                Assert.Equal(expectedReset, rateLimit.Reset);
            }

            [Fact]
            public void HandlesMissingHeaderValues()
            {
                var headers = new Dictionary<string, string>();

                var rateLimit = new RateLimit(headers);

                Assert.Equal(0, rateLimit.Limit);
                Assert.Equal(0, rateLimit.Remaining);
                var expectedReset = DateTimeOffset.ParseExact(
                    "Thu 01 Jan 1970 0:00:00 AM -00:00",
                    "ddd dd MMM yyyy h:mm:ss tt zzz",
                    CultureInfo.InvariantCulture);
                Assert.Equal(expectedReset, rateLimit.Reset);
            }

#if !NETFX_CORE
            [Fact]
            public void CanPopulateObjectFromSerializedData()
            {
                var headers = new Dictionary<string, string> 
                {
                    { "X-RateLimit-Limit", "100" },
                    { "X-RateLimit-Remaining", "42" },
                    { "X-RateLimit-Reset", "1372700873" }
                };

                var rateLimit = new RateLimit(headers);

                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, rateLimit);
                    stream.Position = 0;
                    var deserialized = (RateLimit) formatter.Deserialize(stream);

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
            [Fact]
            public void EnsuresHeadersNotNull()
            {
                Assert.Throws<ArgumentNullException>(() => new RateLimit(null));
            }
        }
    }
}