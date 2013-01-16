using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Nocto.Http;
using Nocto.Tests.Helpers;
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
                var connection = new Connection(ExampleUri);

                connection.Builder.Should().BeOfType<Builder>();
            }

            [Fact]
            public void CanUseCustomBuilder()
            {
                var builder = Substitute.For<IBuilder>();
                builder.Run(Args.Application).Returns(Substitute.For<IApplication>());
                var connection = new Connection(ExampleUri);

                connection.Builder = builder;

                connection.App.Should().NotBeNull();
                builder.Run(Args.Application).Received();
            }

            [Fact]
            public void CanUseCustomMiddlewareStack()
            {
                var app = Substitute.For<IApplication>();
                var connection = new Connection(ExampleUri);

                connection.MiddlewareStack = builder => builder.Run(app);

                connection.App.Should().NotBeNull();
                connection.App.Should().Be(app);
            }
        }

        public class TheGetAsyncMethod
        {
            [Fact]
            public async Task RunsConfiguredAppWithAppropriateEnv()
            {
                var app = Substitute.For<IApplication>();
                app.Invoke(Args.Environment<string>()).Returns(Task.FromResult(app));
                var connection = new Connection(ExampleUri)
                {
                    MiddlewareStack = builder => builder.Run(app)
                };

                await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative));

                app.Received(1).Invoke(Arg.Is<Environment<string>>(x =>
                    x.Request.BaseAddress == ExampleUri &&
                        x.Request.Method == HttpMethod.Get &&
                        x.Request.Endpoint == new Uri("/endpoint", UriKind.Relative)));
            }

            [Fact]
            public async Task CanMakeMutipleRequestsWithSameConnection()
            {
                var app = Substitute.For<IApplication>();
                app.Invoke(Args.Environment<string>()).Returns(Task.FromResult(app));
                var connection = new Connection(ExampleUri)
                {
                    MiddlewareStack = builder => builder.Run(app)
                };

                await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative));
                await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative));
                await connection.GetAsync<string>(new Uri("/endpoint", UriKind.Relative));

                app.Received(3).Invoke(Arg.Is<Environment<string>>(x =>
                    x.Request.BaseAddress == ExampleUri &&
                        x.Request.Method == HttpMethod.Get &&
                        x.Request.Endpoint == new Uri("/endpoint", UriKind.Relative)));
            }
        }

        public class ThePatchAsyncMethod
        {
            [Fact]
            public async Task RunsConfiguredAppWithAppropriateEnv()
            {
                var data = new object();
                var app = Substitute.For<IApplication>();
                app.Invoke(Args.Environment<string>()).Returns(Task.FromResult(app));
                var connection = new Connection(ExampleUri)
                {
                    MiddlewareStack = builder => builder.Run(app)
                };

                await connection.PatchAsync<string>(new Uri("/endpoint", UriKind.Relative), data);

                app.Received(1).Invoke(Arg.Is<Environment<string>>(x =>
                    x.Request.Body == data &&
                        x.Request.BaseAddress == ExampleUri &&
                        x.Request.Method == HttpVerb.Patch &&
                        x.Request.Endpoint == new Uri("/endpoint", UriKind.Relative)));
            }
        }

        public class ThePostAsyncMethod
        {
            [Fact]
            public async Task RunsConfiguredAppWithAppropriateEnv()
            {
                var data = new object();
                var app = Substitute.For<IApplication>();
                app.Invoke(Args.Environment<string>()).Returns(Task.FromResult(app));
                var connection = new Connection(ExampleUri)
                {
                    MiddlewareStack = builder => builder.Run(app)
                };

                await connection.PostAsync<string>(new Uri("/endpoint", UriKind.Relative), data);

                app.Received(1).Invoke(Arg.Is<Environment<string>>(x =>
                    x.Request.Body == data &&
                        x.Request.BaseAddress == ExampleUri &&
                        x.Request.Method == HttpMethod.Post &&
                        x.Request.Endpoint == new Uri("/endpoint", UriKind.Relative)));
            }
        }

        public class TheDeleteAsyncMethod
        {
            [Fact]
            public async Task RunsConfiguredAppWithAppropriateEnv()
            {
                var app = Substitute.For<IApplication>();
                app.Invoke(Args.Environment<string>()).Returns(Task.FromResult(app));
                var connection = new Connection(ExampleUri)
                {
                    MiddlewareStack = builder => builder.Run(app)
                };

                await connection.DeleteAsync<string>(new Uri("/endpoint", UriKind.Relative));

                app.Received(1).Invoke(Arg.Is<Environment<string>>(x =>
                    x.Request.BaseAddress == ExampleUri &&
                        x.Request.Method == HttpMethod.Delete &&
                        x.Request.Endpoint == new Uri("/endpoint", UriKind.Relative)));
            }
        }
    }
}
