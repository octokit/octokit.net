using System;
using System.Threading.Tasks;
using Burr.Http;
using Burr.Tests.TestHelpers;
using FluentAssertions;
using Moq;
using Xunit;

namespace Burr.Tests
{
    public class SimpleJsonResponseHandlerTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new SimpleJsonParser(null));
            }
        }

        public class TheBeforeMethod
        {
            [Fact]
            public async Task SetsRequestHeader()
            {
                const string data = "works";
                var env = new StubEnv { Response = { Body = SimpleJson.SerializeObject(data) } };
                var app = MoqExtensions.ApplicationMock();
                var h = new SimpleJsonParser(app.Object);

                await h.Call(env);

                env.Request.Headers.Should().ContainKey("Accept");
                env.Request.Headers["Accept"].Should().Be("application/vnd.github.v3+json; charset=utf-8");
            }

            [Fact]
            public async Task LeavesStringBodyAlone()
            {
                const string json = "just some string data";
                var env = new StubEnv
                {
                    Request = { Body = json },
                    Response = { Body = SimpleJson.SerializeObject("hi") }
                };
                var app = MoqExtensions.ApplicationMock();
                var h = new SimpleJsonParser(app.Object);

                await h.Call(env);

                env.Request.Body.Should().Be(json);
            }

            [Fact]
            public async Task EncodesObjectBody()
            {
                var env = new StubEnv
                {
                    Request = { Body = new { test = "value" } },
                    Response = { Body = SimpleJson.SerializeObject("hi") }
                };
                var app = MoqExtensions.ApplicationMock();
                var h = new SimpleJsonParser(app.Object);

                await h.Call(env);

                env.Request.Body.Should().Be("{\"test\":\"value\"}");
            }
        }

        public class TheAfterMethod
        {
            [Fact]
            public async Task DeserializesResponse()
            {
                const string data = "works";
                var env = new StubEnv { Response = { Body = SimpleJson.SerializeObject(data) } };
                var app = new Mock<IApplication>();
                app.Setup(x => x.Call(env))
                    .Returns(Task.FromResult(app.Object));
                var h = new SimpleJsonParser(app.Object);

                await h.Call(env);

                env.Response.BodyAsObject.Should().NotBeNull();
                env.Response.BodyAsObject.Should().Be(data);
            }
        }
    }
}
