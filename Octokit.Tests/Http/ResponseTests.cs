using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Http
{
    public class ResponseTests
    {
        public class TheConstructor
        {
            [Fact]
            public void InitializesAllRequiredProperties()
            {
                var r = new ApiResponse<string>();

                Assert.NotNull(r.Headers);
            }
        }
    }
}
