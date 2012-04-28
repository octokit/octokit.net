using System;
using System.Threading.Tasks;
using Burr.Http;
using FluentAssertions;
using Moq;
using Xunit;

namespace Burr.Tests.Http
{
    public class RequestHandlerTests
    {
        public class MockRequestHandler : RequestHandler
        {
            public MockRequestHandler(IApplication app)
                : base(app)
            {
            }

            protected override void Before<T>(Env<T> env)
            {
                BeforeWasCalled = true;
            }

            public bool BeforeWasCalled { get; private set; }
        }

        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new MockRequestHandler(null));
            }
        }

        public class TheCallMethod
        {
            [Fact]
            public async Task InvokesBefore()
            {
                var env = new Mock<Env<string>>();
                var app = new Mock<IApplication>();
                var handler = new MockRequestHandler(app.Object);
                app.Setup(x => x.Call(env.Object))
                    .Returns(Task.FromResult(app.Object))
                    .Callback(() =>
                    {
                        handler.BeforeWasCalled.Should().BeTrue();
                    });

                await handler.Call(env.Object);

                app.Verify(x => x.Call(env.Object));
            }
        }
    }
}
