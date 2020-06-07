using System.Net;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Exceptions
{
    public class TwoFactorRequiredExceptionTests
    {
        public class TheCtor
        {
            [Fact]
            public void SetsDefaultMessage()
            {
                var response = CreateResponse(HttpStatusCode.Unauthorized);

                var exception = new TwoFactorRequiredException(response, TwoFactorType.Sms);

                Assert.Equal("Two-factor authentication code is required", exception.Message);
            }
        }
    }
}
