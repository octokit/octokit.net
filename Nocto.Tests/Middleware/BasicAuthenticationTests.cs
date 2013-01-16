using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Nocto.Http;
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
                Assert.Throws<ArgumentNullException>(() => new BasicAuthentication(Substitute.For<IApplication>(), null, "password"));
                Assert.Throws<ArgumentException>(() => new BasicAuthentication(Substitute.For<IApplication>(), "", "password"));
                Assert.Throws<ArgumentNullException>(() => new BasicAuthentication(Substitute.For<IApplication>(), "login", null));
                Assert.Throws<ArgumentException>(() => new BasicAuthentication(Substitute.For<IApplication>(), "login", ""));
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
                var authenticator = new BasicAuthentication(app, "tclem", "pwd");

                await authenticator.Invoke(env);

                env.Request.Headers.Should().ContainKey("Authorization");
                env.Request.Headers["Authorization"].Should().Be("Basic dGNsZW06cHdk");
            }
        }
    }
}
