using FluentAssertions;
using Nocto.Http;
using Xunit;

namespace Nocto.Tests.Http
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
