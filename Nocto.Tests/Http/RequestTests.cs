using FluentAssertions;
using Octopi.Http;
using Xunit;

namespace Octopi.Tests.Http
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
