using System.Net;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Exceptions
{
    public class ForbiddenExceptionTests
    {
        public class TheConstructor
        {
            [Fact]
            public void IdentifiesMaxLoginAttepmtsExceededReason()
            {
                const string responseBody = "{\"message\":\"YOU SHALL NOT PASS!\"," +
                                            "\"documentation_url\":\"http://developer.github.com/v3\"}";
                var response = new ApiResponse<object> { Body = responseBody, StatusCode = HttpStatusCode.Forbidden };
                var forbiddenException = new ForbiddenException(response);

                Assert.Equal("YOU SHALL NOT PASS!", forbiddenException.ApiError.Message);
            }

            [Fact]
            public void HasDefaultMessage()
            {
                var response = new ApiResponse<object> { StatusCode = HttpStatusCode.Forbidden };
                var forbiddenException = new ForbiddenException(response);

                Assert.Equal("Request Forbidden", forbiddenException.Message);
            }
        }
    }
}
