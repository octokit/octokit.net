using FluentAssertions;
using Nocto.Http;
using Xunit;

namespace Nocto.Tests.Http
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
