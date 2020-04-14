using System.Collections.Generic;
using System.Net;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Exceptions
{
    public class AbuseExceptionTests
    {
        public class TheConstructor
        {
            public class Message
            {
                [Fact]
                public void ContainsAbuseMessageFromApi()
                {
                    const string responseBody = "{\"message\":\"You have triggered an abuse detection mechanism. Please wait a few minutes before you try again.\"," +
                                                "\"documentation_url\":\"https://developer.github.com/v3/#abuse-rate-limits\"}";

                    var response = CreateResponse(
                        HttpStatusCode.Forbidden,
                        responseBody);

                    var abuseException = new AbuseException(response);

                    Assert.Equal("You have triggered an abuse detection mechanism. Please wait a few minutes before you try again.", abuseException.ApiError.Message);
                }

                [Fact]
                public void HasDefaultMessage()
                {
                    var response = CreateResponse(HttpStatusCode.Forbidden);
                    var abuseException = new AbuseException(response);

                    Assert.Equal("Request Forbidden - Abuse Detection", abuseException.Message);
                }
            }

            public class RetryAfterSeconds
            {
                public class HappyPath
                {
                    [Fact]
                    public void WithRetryAfterHeader_PopulatesRetryAfterSeconds()
                    {
                        var headerDictionary = new Dictionary<string, string> { { "Retry-After", "30" } };

                        var response = CreateResponse(HttpStatusCode.Forbidden, headerDictionary);
                        var abuseException = new AbuseException(response);

                        Assert.Equal(30, abuseException.RetryAfterSeconds);
                    }

                    [Fact]
                    public void WithRetryAfterCaseInsensitiveHeader_PopulatesRetryAfterSeconds()
                    {
                        var headerDictionary = new Dictionary<string, string> { { "retry-after", "20" } };

                        var response = CreateResponse(HttpStatusCode.Forbidden, headerDictionary);
                        var abuseException = new AbuseException(response);

                        Assert.Equal(20, abuseException.RetryAfterSeconds);
                    }

                    [Fact]
                    public void WithZeroHeaderValue_RetryAfterSecondsIsZero()
                    {
                        var headerDictionary = new Dictionary<string, string> { { "Retry-After", "0" } };

                        var response = CreateResponse(HttpStatusCode.Forbidden, headerDictionary);
                        var abuseException = new AbuseException(response);

                        Assert.Equal(0, abuseException.RetryAfterSeconds);
                    }
                }

                public class UnhappyPaths
                {
                    [Fact]
                    public void NoRetryAfterHeader_RetryAfterSecondsIsSetToTheDefaultOfNull()
                    {
                        var response = CreateResponse(HttpStatusCode.Forbidden);
                        var abuseException = new AbuseException(response);

                        Assert.False(abuseException.RetryAfterSeconds.HasValue);
                    }

                    [Theory]
                    [InlineData(null)]
                    [InlineData("")]
                    [InlineData("    ")]
                    public void EmptyHeaderValue_RetryAfterSecondsDefaultsToNull(string emptyValueToTry)
                    {
                        var headerDictionary = new Dictionary<string, string> { { "Retry-After", emptyValueToTry } };

                        var response = CreateResponse(HttpStatusCode.Forbidden, headerDictionary);
                        var abuseException = new AbuseException(response);

                        Assert.False(abuseException.RetryAfterSeconds.HasValue);
                    }

                    [Fact]
                    public void NonParseableIntHeaderValue_RetryAfterSecondsDefaultsToNull()
                    {
                        var headerDictionary = new Dictionary<string, string> { { "Retry-After", "ABC" } };

                        var response = CreateResponse(HttpStatusCode.Forbidden, headerDictionary);
                        var abuseException = new AbuseException(response);

                        Assert.False(abuseException.RetryAfterSeconds.HasValue);
                    }

                    [Fact]
                    public void NegativeHeaderValue_RetryAfterSecondsDefaultsToNull()
                    {
                        var headerDictionary = new Dictionary<string, string> { { "Retry-After", "-123" } };

                        var response = CreateResponse(HttpStatusCode.Forbidden, headerDictionary);
                        var abuseException = new AbuseException(response);

                        Assert.False(abuseException.RetryAfterSeconds.HasValue);
                    }
                }
            }
        }
    }
}
