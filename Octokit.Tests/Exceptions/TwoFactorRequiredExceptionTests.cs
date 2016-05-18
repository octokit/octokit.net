using System.Collections.Generic;
using System.Net;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Exceptions
{
    public class TwoFactorRequiredExceptionTests
    {
        public class TheCtor
        {
            [Fact]
            public void SetsDefaultMessage()
            {
                var response = new Response(HttpStatusCode.Unauthorized, null, new Dictionary<string, string>(), "application/json");

                var exception = new TwoFactorRequiredException(response, TwoFactorType.Sms);

                Assert.Equal("Two-factor authentication code is required", exception.Message);
            }
        }
    }
}
