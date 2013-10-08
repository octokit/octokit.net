using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Http
{
    public class RequestTests
    {
        public class TheConstructor
        {
            [Fact]
            public void InitializesAllRequiredProperties()
            {
                var r = new Request();

                Assert.NotNull(r.Headers);
            }
        }
    }
}
