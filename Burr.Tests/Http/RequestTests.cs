using Burr.Http;
using FluentAssertions;
using Xunit;

namespace Burr.Tests.Http
{
    public class RequestTests
    {
        public class TheConstructor
        {
            [Fact]
            public void InitializesAllRequiredProperties()
            {
                var r = new Request();

                r.Headers.Should().NotBeNull();
            }
        }
    }
}
