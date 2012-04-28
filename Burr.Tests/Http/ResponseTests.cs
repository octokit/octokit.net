using Burr.Http;
using FluentAssertions;
using Xunit;

namespace Burr.Tests.Http
{
    public class ResponseTests
    {
        public class TheConstructor
        {
            [Fact]
            public void InitializesAllRequiredProperties()
            {
                var r = new Response<string>();

                r.Headers.Should().NotBeNull();
            }
        }
    }
}
