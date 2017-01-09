using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Exceptions
{
    public class AbuseExceptionTests
    {
        [Fact]
        public void PostsAbuseMessageFromApi()
        {

            const string responseBody = "{\"message\":\"You have triggered an abuse detection mechanism. Please wait a few minutes before you try again.\"," +
                                        "\"documentation_url\":\"https://developer.github.com/v3/#abuse-rate-limits\"}";

            var response = new Response(
                HttpStatusCode.Forbidden,
                responseBody,
                new Dictionary<string, string>(),
                "application/json");
            var abuseException = new AbuseException(response);

            Assert.Equal("You have triggered an abuse detection mechanism. Please wait a few minutes before you try again.", abuseException.ApiError.Message);
        }

        [Fact]
        public void HasDefaultMessage()
        {
            var response = new Response(HttpStatusCode.Forbidden, null, new Dictionary<string, string>(), "application/json");
            var abuseException = new AbuseException(response);

            Assert.Equal("Request Forbidden - Abuse Detection", abuseException.Message);
        }

        public class RetryAfterHeaderHandling
        {
            [Fact]
            public void WithRetryAfterHeader_PopulatesRetryAfterSeconds()
            {
                var headerDictionary = new Dictionary<string, string>();
                headerDictionary.Add("Retry-After", "60");

                var response = new Response(HttpStatusCode.Forbidden, null, headerDictionary, "application/json");
                var abuseException = new AbuseException(response);

                Assert.Equal(60, abuseException.RetryAfterSeconds);
            }

            [Fact]
            public void NoHeader_RetryAfterSecondsDefaultsTo60()
            {
                throw new NotImplementedException();
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("    ")]
            public void EmptyHeaderValue_RetryAfterSecondsDefaultsTo60()
            {
                throw new NotImplementedException();
            }

            [Fact]
            public void NonParseableHeaderValue_RetryAfterSecondsDefaultsTo60()
            {
                throw new NotImplementedException();
            }

            [Fact]
            public void NegativeHeaderValue_RetryAfterSecondsDefaultsTo60()
            {
                throw new NotImplementedException();
            }
        }
    }
}
