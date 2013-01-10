using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Nocto.Http;
using Nocto.Tests.TestHelpers;
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
                var request = new Func<IApplication, IApplication>(a => new Mock<IApplication>().Object);
                var response = new Func<IApplication, IApplication>(a => new Mock<IApplication>().Object);
                var adapter = MoqExtensions.ApplicationMock();
                var builder = new Builder();
                builder.Use(request);
                builder.Use(response);

                builder.Run(adapter.Object);

                Assert.Throws<NotSupportedException>(() => builder.Use(a => new Mock<IApplication>().Object));
                Assert.Throws<NotSupportedException>(() => builder.Run(new Mock<IApplication>().Object));
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
                var adapter = new Mock<IApplication>();
                adapter.Setup(x => x.Call(env))
                    .Returns(Task.FromResult(adapter.Object))
                    .Callback<StubEnvironment>(e => called.Add(adapter.Object));
                var builder = new Builder();
                builder.Use(request);
                builder.Use(response);

                var app = builder.Run(adapter.Object);

                app.Should().Be(requestHandler);
                app.Call(env);
                called.Count.Should().Be(3);
                called[0].Should().Be(requestHandler);
                called[1].Should().Be(responseHandler);
                called[2].Should().Be(adapter.Object);
            }

            [Fact]
            public void ThrowsIfRunIsCalledTwice()
            {
                var builder = new Builder();
                builder.Run();

                Assert.Throws<NotSupportedException>(() => builder.Run());
            }

            public void UsesHttpClientAdapterIfNoneIsSpecified()
            {
                var builder = new Builder();

                builder.Run().GetType().Should().Be(typeof(HttpClientAdapter));
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

                public Task<IApplication> Call<T>(Environment<T> environment)
                {
                    called.Add(this);
                    return app.Call(environment);
                }
            }
        }

        public class TheUseMethod
        {
            [Fact]
            public void AddsToCollectionOfHandlers()
            {
                var mock = new Mock<IApplication>();
                var handler = new Func<IApplication, IApplication>(a => mock.Object);
                var builder = new Builder();

                builder.Use(handler);

                builder.Handlers.Contains(handler).Should().BeTrue();
                builder.Handlers[0](new Mock<IApplication>().Object).Should().Be(mock.Object);
            }

            [Fact]
            public void ThrowsWithBadParams()
            {
                Assert.Throws<ArgumentNullException>(() => new Builder().Use(null));
            }
        }
    }
}
