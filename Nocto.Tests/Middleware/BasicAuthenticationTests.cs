using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Nocto.Http;
using Nocto.Tests.TestHelpers;
using Xunit;

namespace Nocto.Tests
{
    public class BasicAuthenticationTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new BasicAuthentication(null, "login", "password"));
                Assert.Throws<ArgumentNullException>(() => new BasicAuthentication(Mock.Of<IApplication>(), null, "password"));
                Assert.Throws<ArgumentException>(() => new BasicAuthentication(Mock.Of<IApplication>(), "", "password"));
                Assert.Throws<ArgumentNullException>(() => new BasicAuthentication(Mock.Of<IApplication>(), "login", null));
                Assert.Throws<ArgumentException>(() => new BasicAuthentication(Mock.Of<IApplication>(), "login", ""));
            }
        }

        public class TheBeforeMethod
        {
            [Fact]
            public async Task SetsRequestHeader()
            {
                var env = new StubEnv();
                var app = MoqExtensions.ApplicationMock();
                var h = new BasicAuthentication(app.Object, "tclem", "pwd");

                await h.Call(env);

                env.Request.Headers.Should().ContainKey("Authorization");
                env.Request.Headers["Authorization"].Should().Be("Basic dGNsZW06cHdk");
            }
        }
    }
}
