using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Nocto.Http;
using Nocto.Tests.TestHelpers;
using Xunit;

namespace Nocto.Tests.Http
{
    public class ConnectionTests
    {
        const string ExampleUrl = "http://example.com";
        static readonly Uri ExampleUri = new Uri(ExampleUrl);

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
            public async Task RunsConfiguredAppWithAppropriateEnv()
            {
                var app = MoqExtensions.ApplicationMock();
                var c = new Connection(ExampleUri);
                c.MiddlewareStack = builder => builder.Run(app.Object);

                var res = await c.GetAsync<string>(new Uri("/endpoint", UriKind.Relative));

                app.Verify(p => p.Invoke(It.Is<Environment<string>>(x =>
                    x.Request.BaseAddress == ExampleUri &&
                        x.Request.Method == "GET" &&
                        x.Request.Endpoint == new Uri("/endpoint", UriKind.Relative))), Times.Once());
            }

            [Fact]
            public async Task CanMakeMutipleRequestsWithSameConnection()
            {
                var app = MoqExtensions.ApplicationMock();
                var c = new Connection(ExampleUri);
                c.MiddlewareStack = builder => builder.Run(app.Object);

                var res = await c.GetAsync<string>(new Uri("/endpoint", UriKind.Relative));
                res = await c.GetAsync<string>(new Uri("/endpoint", UriKind.Relative));
                res = await c.GetAsync<string>(new Uri("/endpoint", UriKind.Relative));

                app.Verify(p => p.Invoke(It.Is<Environment<string>>(x =>
                    x.Request.BaseAddress == ExampleUri &&
                        x.Request.Method == "GET" &&
                        x.Request.Endpoint == new Uri("/endpoint", UriKind.Relative))), Times.Exactly(3));
            }
        }

        public class ThePatchAsyncMethod
        {
            [Fact]
            public async Task RunsConfiguredAppWithAppropriateEnv()
            {
                var o = new object();
                var app = MoqExtensions.ApplicationMock();
                var c = new Connection(ExampleUri);
                c.MiddlewareStack = builder => builder.Run(app.Object);

                var res = await c.PatchAsync<string>(new Uri("/endpoint", UriKind.Relative), o);

                app.Verify(p => p.Invoke(It.Is<Environment<string>>(x =>
                    x.Request.Body == o &&
                        x.Request.BaseAddress == ExampleUri &&
                        x.Request.Method == "PATCH" &&
                        x.Request.Endpoint == new Uri("/endpoint", UriKind.Relative))), Times.Once());
            }
        }

        public class ThePostAsyncMethod
        {
            [Fact]
            public async Task RunsConfiguredAppWithAppropriateEnv()
            {
                var o = new object();
                var app = MoqExtensions.ApplicationMock();
                var c = new Connection(ExampleUri);
                c.MiddlewareStack = builder => builder.Run(app.Object);

                var res = await c.PostAsync<string>(new Uri("/endpoint", UriKind.Relative), o);

                app.Verify(p => p.Invoke(It.Is<Environment<string>>(x =>
                    x.Request.Body == o &&
                        x.Request.BaseAddress == ExampleUri &&
                        x.Request.Method == "POST" &&
                        x.Request.Endpoint == new Uri("/endpoint", UriKind.Relative))), Times.Once());
            }
        }

        public class TheDeleteAsyncMethod
        {
            [Fact]
            public async Task RunsConfiguredAppWithAppropriateEnv()
            {
                var o = new object();
                var app = MoqExtensions.ApplicationMock();
                var c = new Connection(ExampleUri);
                c.MiddlewareStack = builder => builder.Run(app.Object);

                await c.DeleteAsync<string>(new Uri("/endpoint", UriKind.Relative));

                app.Verify(p => p.Invoke(It.Is<Environment<string>>(x =>
                    x.Request.BaseAddress == ExampleUri &&
                        x.Request.Method == "DELETE" &&
                        x.Request.Endpoint == new Uri("/endpoint", UriKind.Relative))), Times.Once());
            }
        }
    }
}
