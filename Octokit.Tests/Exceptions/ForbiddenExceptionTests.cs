using System.Net;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Exceptions
{
    public class ForbiddenExceptionTests
    {
        public class TheConstructor
        {
            [Fact]
            public void IdentifiesMaxLoginAttemptsExceededReason()
            {
                var responseBody = "{\"message\":\"YOU SHALL NOT PASS!\", \"documentation_url\":\"http://developer.github.com/v3\"}";
                var response = CreateResponse(HttpStatusCode.Forbidden, responseBody);

                var forbiddenException = new ForbiddenException(response);

                Assert.Equal("YOU SHALL NOT PASS!", forbiddenException.ApiError.Message);
            }

            [Fact]
            public void HasDefaultMessage()
            {
                var response = CreateResponse(HttpStatusCode.Forbidden);

                var forbiddenException = new ForbiddenException(response);

                Assert.Equal("Request Forbidden", forbiddenException.Message);
            }
        }
    }
}
