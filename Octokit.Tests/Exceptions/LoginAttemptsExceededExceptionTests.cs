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
    public class LoginAttemptsExceededExceptionTests
    {
        public class TheConstructor
        {
            [Fact]
            public void SetsDefaultMessage()
            {
                var response = new ApiResponse<object>
                {
                    StatusCode = HttpStatusCode.Forbidden
                };

                var exception = new LoginAttemptsExceededException(response);

                Assert.Equal("Maximum number of login attempts exceeded", exception.Message);
            }
        }
    }
}
