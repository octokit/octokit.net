using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Nocto.Http;
using Nocto.Tests.TestHelpers;
using Xunit;

namespace Nocto.Tests
{
    public class TokenAuthenticationTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new TokenAuthentication(null, "token"));
                Assert.Throws<ArgumentNullException>(() => new TokenAuthentication(Mock.Of<IApplication>(), null));
                Assert.Throws<ArgumentException>(() => new TokenAuthentication(Mock.Of<IApplication>(), ""));
            }
        }

        public class TheBeforeMethod
        {
            [Fact]
            public async Task SetsRequestHeader()
            {
                var env = new StubEnvironment();
                var app = MoqExtensions.ApplicationMock();
                var h = new TokenAuthentication(app.Object, "abcda1234a");

                await h.Invoke(env);

                env.Request.Headers.Should().ContainKey("Authorization");
                env.Request.Headers["Authorization"].Should().Be("Token abcda1234a");
            }
        }
    }
}
