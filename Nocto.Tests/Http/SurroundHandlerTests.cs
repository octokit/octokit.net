using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Nocto.Http;
using Xunit;

namespace Nocto.Tests.Http
{
    public class SurroundHandlerTests
    {
        public class MockSurroundHandler : Nocto.Http.Middleware
        {
            public MockSurroundHandler(IApplication app)
                : base(app)
            {
            }

            protected override void After<T>(Environment<T> environment)
            {
                AfterWasCalled = true;
            }

            protected override void Before<T>(Environment<T> environment)
            {
                BeforeWasCalled = true;
            }

            public bool AfterWasCalled { get; private set; }
            public bool BeforeWasCalled { get; private set; }
        }

        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new MockSurroundHandler(null));
            }
        }

        public class TheCallMethod
        {
            [Fact]
            public async Task InvokesBefore()
            {
                var env = new Mock<Environment<string>>();
                var app = new Mock<IApplication>();
                var handler = new MockSurroundHandler(app.Object);
                app.Setup(x => x.Call(env.Object))
                    .Returns(Task.FromResult(app.Object))
                    .Callback(() =>
                    {
                        handler.BeforeWasCalled.Should().BeTrue();
                        handler.AfterWasCalled.Should().BeFalse();
                    });

                await handler.Call(env.Object);

                app.Verify(x => x.Call(env.Object));
            }

            [Fact]
            public async Task InvokesAfter()
            {
                var env = new Mock<Environment<string>>();
                var app = new Mock<IApplication>();
                app.Setup(x => x.Call(env.Object)).Returns(Task.FromResult(app.Object));
                var handler = new MockSurroundHandler(app.Object);

                await handler.Call(env.Object);

                app.Verify(x => x.Call(env.Object));
                handler.AfterWasCalled.Should().BeTrue();
            }
        }
    }
}
