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
                    var deserialized = (RateLimit)formatter.Deserialize(stream);

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

        public class TheMethods
        {
            [Fact]
            public void CanClone()
            {
                var original = new RateLimit(100, 42, 1372700873);

                var clone = original.Clone();

                // Note the use of Assert.NotSame tests for value types - this should continue to test should the underlying 
                // model are changed to Object types
                Assert.NotSame(original, clone);
                Assert.Equal(original.Limit, clone.Limit);
                Assert.NotSame(original.Limit, clone.Limit);
                Assert.Equal(original.Remaining, clone.Remaining);
                Assert.NotSame(original.Remaining, clone.Remaining);
                Assert.Equal(original.ResetAsUtcEpochSeconds, clone.ResetAsUtcEpochSeconds);
                Assert.NotSame(original.ResetAsUtcEpochSeconds, clone.ResetAsUtcEpochSeconds);
                Assert.Equal(original.Reset, clone.Reset);
                Assert.NotSame(original.Reset, clone.Reset);
            }
        }
    }
}