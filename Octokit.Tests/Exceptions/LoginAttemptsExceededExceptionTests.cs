using System.Net;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Exceptions
{
    public class LoginAttemptsExceededExceptionTests
    {
        public class TheConstructor
        {
            [Fact]
            public void SetsDefaultMessage()
            {
                var response = CreateResponse(HttpStatusCode.Forbidden);

                var exception = new LoginAttemptsExceededException(response);

                Assert.Equal("Maximum number of login attempts exceeded", exception.Message);
            }
        }
    }
}
