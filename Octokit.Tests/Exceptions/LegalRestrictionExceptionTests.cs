using System.Collections.Generic;
using System.Net;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Exceptions
{
    public class LegalRestrictionExceptionTests
    {
        [Fact]
        public void HasDefaultMessage()
        {
            var response = new Response((HttpStatusCode)451, null, new Dictionary<string, string>(), "application/json");
            var legalRestrictionException = new LegalRestrictionException(response);

            Assert.Equal("Resource taken down due to a DMCA notice.", legalRestrictionException.Message);
        }
    }
}