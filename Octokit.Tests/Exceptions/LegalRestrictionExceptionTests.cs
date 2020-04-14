using System.Net;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Exceptions
{
    public class LegalRestrictionExceptionTests
    {
        [Fact]
        public void HasDefaultMessage()
        {
            var response = CreateResponse((HttpStatusCode)451);
            var legalRestrictionException = new LegalRestrictionException(response);

            Assert.Equal("Resource taken down due to a DMCA notice.", legalRestrictionException.Message);
        }
    }
}
