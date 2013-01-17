using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Nocto.Http;
using Xunit;

namespace Nocto.Tests.Http
{
    public class SurroundHandlerTests
    {
        public class MockSurroundHandler : Middleware
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
                var env = Substitute.For<Environment<string>>();
                var app = Substitute.For<IApplication>();
                var handler = new MockSurroundHandler(app);
                app.Invoke(env)
                    .Returns(_ =>
                    {
                        handler.BeforeWasCalled.Should().BeTrue();
                        handler.AfterWasCalled.Should().BeFalse();
                        return Task.FromResult(app);
                    });

                await handler.Invoke(env);

                app.Received().Invoke(env);
            }

            [Fact]
            public async Task InvokesAfter()
            {
                var env = Substitute.For<Environment<string>>();
                var app = Substitute.For<IApplication>();
                app.Invoke(env).Returns(Task.FromResult(app));
                var handler = new MockSurroundHandler(app);

                await handler.Invoke(env);

                app.Received().Invoke(env);
                handler.AfterWasCalled.Should().BeTrue();
            }
        }
    }
}
