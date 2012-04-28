using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Burr.Http;
using Xunit;
using Xunit.Extensions;
using FluentAssertions;
using Moq;

namespace Burr.Tests.Http
{
    public class ConnectionTests
    {
        const string ExampleUrl = "http://example.com";
        static Uri ExampleUri = new Uri(ExampleUrl);

        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new Connection(null));
            }

            [Fact]
            public void DefaultsToStandardImplementations()
            {
                var c = new Connection(ExampleUri);

                c.Builder.Should().BeOfType<Builder>();
            }

            [Fact]
            public void CanUseCustomBuilder()
            {
                var builder = new Mock<IBuilder>();
                builder.Setup(x => x.Run(It.IsAny<IApplication>())).Returns(Mock.Of<IApplication>());
                var c = new Connection(ExampleUri);

                c.Builder = builder.Object;

                c.App.Should().NotBeNull();
                builder.Verify(x => x.Run(It.IsAny<IApplication>()));
            }

            [Fact]
            public void CanUseCustomMiddlewareStack()
            {
                var app = new Mock<IApplication>();
                var c = new Connection(ExampleUri);
                
                c.MiddlewareStack = builder => builder.Run(app.Object);

                c.App.Should().NotBeNull();
                c.App.Should().Be(app.Object);
            }
        }

        public class TheGetAsyncMethod
        {
            [Fact]
            public async Task RunsConfigureAppWithAppropriateEnv()
            {
                var app = new Mock<IApplication>();
                app.Setup(x => x.Call(It.IsAny<Env<string>>())).Returns(Task.FromResult<IApplication>(app.Object));
                var c = new Connection(ExampleUri);
                c.MiddlewareStack = builder => builder.Run(app.Object);

                var res = await c.GetAsync<string>("/endpoint");

                app.Verify(p => p.Call(It.Is<Env<string>>(x =>
                        x.Request.BaseAddress == ExampleUri &&
                        x.Request.Method == "GET" &&
                        x.Request.Endpoint == "/endpoint")), Times.Once());
            }

            [Fact]
            public async Task CanMakeMutipleRequestsWithSameConnection()
            {
                var app = new Mock<IApplication>();
                app.Setup(x => x.Call(It.IsAny<Env<string>>())).Returns(Task.FromResult<IApplication>(app.Object));
                var c = new Connection(ExampleUri);
                c.MiddlewareStack = builder => builder.Run(app.Object);

                var res = await c.GetAsync<string>("/endpoint");
                res = await c.GetAsync<string>("/endpoint");
                res = await c.GetAsync<string>("/endpoint");

                app.Verify(p => p.Call(It.Is<Env<string>>(x =>
                        x.Request.BaseAddress == ExampleUri &&
                        x.Request.Method == "GET" &&
                        x.Request.Endpoint == "/endpoint")), Times.Exactly(3));
            }
        }
    }
}
