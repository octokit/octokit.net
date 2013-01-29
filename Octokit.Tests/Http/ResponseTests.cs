using FluentAssertions;
using Octokit.Http;
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

                r.Headers.Should().NotBeNull();
            }
        }
    }
}
