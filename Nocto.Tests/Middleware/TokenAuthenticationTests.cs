using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Nocto.Http;
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
                Assert.Throws<ArgumentNullException>(() => new TokenAuthentication(Substitute.For<IApplication>(), null));
                Assert.Throws<ArgumentException>(() => new TokenAuthentication(Substitute.For<IApplication>(), ""));
            }
        }

        public class TheBeforeMethod
        {
            [Fact]
            public async Task SetsRequestHeader()
            {
                var env = new StubEnvironment();
                var app = Substitute.For<IApplication>();
                app.Invoke(Args.Environment<string>()).Returns(Task.FromResult(app));
                var authenticator = new TokenAuthentication(app, "abcda1234a");

                await authenticator.Invoke(env);

                env.Request.Headers.Should().ContainKey("Authorization");
                env.Request.Headers["Authorization"].Should().Be("Token abcda1234a");
            }
        }
    }
}
