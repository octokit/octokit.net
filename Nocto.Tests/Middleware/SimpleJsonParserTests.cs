using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Nocto.Http;
using Xunit;

namespace Nocto.Tests
{
    public class SimpleJsonResponseHandlerTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new SimpleJsonParser(null, new SimpleJsonSerializer()));
                Assert.Throws<ArgumentNullException>(() => new SimpleJsonParser(Substitute.For<IApplication>(), null));
            }
        }

        public class TheBeforeMethod
        {
            [Fact]
            public async Task SetsRequestHeader()
            {
                const string data = "works";
                var env = new StubEnvironment { Response = { Body = SimpleJson.SerializeObject(data) } };
                var app = Substitute.For<IApplication>();
                app.Invoke(Args.Environment<string>()).Returns(Task.FromResult(app));
                var parser = new SimpleJsonParser(app, new SimpleJsonSerializer());

                await parser.Invoke(env);

                env.Request.Headers.Should().ContainKey("Accept");
                env.Request.Headers["Accept"].Should().Be("application/vnd.github.v3+json; charset=utf-8");
            }

            [Fact]
            public async Task LeavesStringBodyAlone()
            {
                const string json = "just some string data";
                var env = new StubEnvironment
                {
                    Request = { Body = json },
                    Response = { Body = SimpleJson.SerializeObject("hi") }
                };
                var app = Substitute.For<IApplication>();
                app.Invoke(Args.Environment<string>()).Returns(Task.FromResult(app));
                var parser = new SimpleJsonParser(app, new SimpleJsonSerializer());

                await parser.Invoke(env);

                env.Request.Body.Should().Be(json);
            }

            [Fact]
            public async Task EncodesObjectBody()
            {
                var env = new StubEnvironment
                {
                    Request = { Body = new { test = "value" } },
                    Response = { Body = SimpleJson.SerializeObject("hi") }
                };
                var app = Substitute.For<IApplication>();
                app.Invoke(Args.Environment<string>()).Returns(Task.FromResult(app));
                var parser = new SimpleJsonParser(app, new SimpleJsonSerializer());

                await parser.Invoke(env);

                env.Request.Body.Should().Be("{\"test\":\"value\"}");
            }
        }

        public class TheAfterMethod
        {
            [Fact]
            public async Task DeserializesResponse()
            {
                const string data = "works";
                var env = new StubEnvironment { Response = { Body = SimpleJson.SerializeObject(data) } };
                var app = Substitute.For<IApplication>();
                app.Invoke(env).Returns(Task.FromResult(app));
                var parser = new SimpleJsonParser(app, new SimpleJsonSerializer());

                await parser.Invoke(env);

                env.Response.BodyAsObject.Should().NotBeNull();
                env.Response.BodyAsObject.Should().Be(data);
            }
        }
    }
}
