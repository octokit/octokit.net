using FluentAssertions;
using Octopi.Http;
using Xunit;

namespace Octopi.Tests.Http
{
    public class ResponseTests
    {
        public class TheConstructor
        {
            [Fact]
            public void InitializesAllRequiredProperties()
            {
                var r = new GitHubResponse<string>();

                r.Headers.Should().NotBeNull();
            }
        }
    }
}
