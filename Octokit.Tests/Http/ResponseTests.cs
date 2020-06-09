using System.Net;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Http
{
    public class ResponseTests
    {
        public class TheConstructor
        {
            [Fact]
            public void InitializesAllRequiredProperties()
            {
                var r = CreateResponse(HttpStatusCode.OK);

                Assert.NotNull(r.Headers);
            }
        }
    }
}
