using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burr.Handlers;
using Burr.Http;
using Burr.Tests.TestHelpers;
using Moq;
using Xunit;
using Xunit.Extensions;
using FluentAssertions;

namespace Burr.Tests.Handlers
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
