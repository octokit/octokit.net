using System;
using System.Threading.Tasks;
using Burr.Http;
using Burr.Tests.TestHelpers;
using FluentAssertions;
using Moq;
using Xunit;

namespace Burr.Tests
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
                var env = new StubEnv();
                var app = MoqExtensions.ApplicationMock();
                var h = new TokenAuthentication(app.Object, "abcda1234a");

                await h.Call(env);

                env.Request.Headers.Should().ContainKey("Authorization");
                env.Request.Headers["Authorization"].Should().Be("Token abcda1234a");
            }
        }
    }
}
