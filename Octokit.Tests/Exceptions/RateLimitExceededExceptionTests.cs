using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Exceptions
{
    public class RateLimitExceededExceptionTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ParsesRateLimitsFromHeaders()
            {
                var response = new ApiResponse<object> { StatusCode = HttpStatusCode.Forbidden };
                response.Headers.Add("X-RateLimit-Limit", "100");
                response.Headers.Add("X-RateLimit-Remaining", "42");
                response.Headers.Add("X-RateLimit-Reset", "1372700873");
                response.ApiInfo = CreateApiInfo(response);
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
                var response = new ApiResponse<object> { StatusCode = HttpStatusCode.Forbidden };
                response.Headers.Add("X-RateLimit-Limit", "XXX");
                response.Headers.Add("X-RateLimit-Remaining", "XXXX");
                response.Headers.Add("X-RateLimit-Reset", "XXXX");
                response.ApiInfo = CreateApiInfo(response);
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
                var response = new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.Forbidden
                };
                response.ApiInfo = CreateApiInfo(response);
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

#if !NETFX_CORE
            [Fact]
            public void CanPopulateObjectFromSerializedData()
            {
                var response = new ApiResponse<object> { StatusCode = HttpStatusCode.Forbidden };
                response.Headers.Add("X-RateLimit-Limit", "100");
                response.Headers.Add("X-RateLimit-Remaining", "42");
                response.Headers.Add("X-RateLimit-Reset", "1372700873");
                response.ApiInfo = CreateApiInfo(response);

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

        static ApiInfo CreateApiInfo(IResponse response)
        {
            return new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit(response.Headers) );
        }
    }
}
