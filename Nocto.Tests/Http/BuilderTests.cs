using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Nocto.Http;
using Xunit;

namespace Nocto.Tests.Http
{
    public class BuilderTests
    {
        public class TheRunMethod
        {
            [Fact]
            public void FreezesHandlerCollections()
            {
                var request = new Func<IApplication, IApplication>(a => Substitute.For<IApplication>());
                var response = new Func<IApplication, IApplication>(a => Substitute.For<IApplication>());
                var app = Substitute.For<IApplication>();
                app.Invoke(Args.Environment<string>()).Returns(Task.FromResult(app));
                var builder = new Builder();
                builder.Use(request);
                builder.Use(response);

                builder.Run(app);

                Assert.Throws<NotSupportedException>(() => builder.Use(a => Substitute.For<IApplication>()));
                Assert.Throws<NotSupportedException>(() => builder.Run(Substitute.For<IApplication>()));
            }

            [Fact]
            public void SetsUpHandlersInPriorityOrder()
            {
                // The builder sets up a chain of apps to be called just like Rack
                // The inner most app is always an adapter which must produce the actual response
                // The call tree for a basic middleware stack will look like this:
                //
                // request( response ( adapter(env) ) )

                var called = new List<IApplication>();
                MockApplication requestHandler = null;
                MockApplication responseHandler = null;
                var env = new StubEnvironment();
                var request = new Func<IApplication, IApplication>(a => requestHandler = new MockApplication(a, called));
                var response = new Func<IApplication, IApplication>(a => responseHandler = new MockApplication(a, called));

                var adapter = Substitute.For<IApplication>();
                adapter.Invoke(env).Returns(_ =>
                {
                    called.Add(adapter);
                    return Task.FromResult(adapter);
                });
                var builder = new Builder();
                builder.Use(request);
                builder.Use(response);

                var app = builder.Run(adapter);

                app.Should().Be(requestHandler);
                app.Invoke(env);
                called.Count.Should().Be(3);
                called[0].Should().Be(requestHandler);
                called[1].Should().Be(responseHandler);
                called[2].Should().Be(adapter);
            }

            [Fact]
            public void ThrowsIfRunIsCalledTwice()
            {
                var builder = new Builder();
                builder.Run(null);

                Assert.Throws<NotSupportedException>(() => builder.Run(null));
            }

            public void UsesHttpClientAdapterIfNoneIsSpecified()
            {
                var builder = new Builder();

                builder.Run(null).GetType().Should().Be(typeof(HttpClientAdapter));
            }

            public class MockApplication : IApplication
            {
                readonly IApplication app;
                readonly List<IApplication> called;

                public MockApplication(IApplication app, List<IApplication> called)
                {
                    this.app = app;
                    this.called = called;
                }

                public Task<IApplication> Invoke<T>(Environment<T> environment)
                {
                    called.Add(this);
                    return app.Invoke(environment);
                }
            }
        }

        public class TheUseMethod
        {
            [Fact]
            public void AddsToCollectionOfHandlers()
            {
                var application = Substitute.For<IApplication>();
                var handler = new Func<IApplication, IApplication>(a => application);
                var builder = new Builder();

                builder.Use(handler);

                builder.Handlers.Contains(handler).Should().BeTrue();
                builder.Handlers[0](Substitute.For<IApplication>()).Should().Be(application);
            }

            [Fact]
            public void ThrowsWithBadParams()
            {
                Assert.Throws<ArgumentNullException>(() => new Builder().Use(null));
            }
        }
    }
}
