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
    public class TwoFactorRequiredExceptionTests
    {
        public class TheConstructor
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
