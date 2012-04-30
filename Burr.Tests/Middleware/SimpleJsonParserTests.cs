using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;
using FluentAssertions;
using Moq;
using Burr.Http;
using Burr.SimpleJson;
using Burr.Tests.TestHelpers;

namespace Burr.Tests
{
    public class SimpleJsonResponseHandlerTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new SimpleJsonParser(null, Mock.Of<IGitHubModelMap>()));
                Assert.Throws<ArgumentNullException>(() => new SimpleJsonParser(Mock.Of<IApplication>(), null));
            }
        }

        public class TheBeforeMethod
        {
            [Fact]
            public async Task SetsRequestHeader()
            {
                var data = "works";
                var env = new StubEnv();
                env.Response.Body = JsonEncoder.Encode(data);
                var app = MoqExtensions.ApplicationMock();
                var map = new Mock<IGitHubModelMap>();
                map.Setup(x => x.For<string>(It.IsAny<JObject>())).Returns(data);
                var h = new SimpleJsonParser(app.Object, map.Object);

                await h.Call(env);

                env.Request.Headers.Should().ContainKey("Accept");
                env.Request.Headers["Accept"].Should().Be("application/json; charset=utf-8");
            }

            [Fact]
            public async Task LeavesStringBodyAlone()
            {
                var json = "just some string data";
                var env = new StubEnv()
                {
                    Request = { Body = json },
                    Response = { Body = JsonEncoder.Encode("hi") }
                };
                var app = MoqExtensions.ApplicationMock();
                var map = new Mock<IGitHubModelMap>();
                map.Setup(x => x.For(It.IsAny<object>()))
                    .Returns(JObject.CreateObject(
                    new Dictionary<string, JObject>{
                        {"test", JObject.CreateString("value")}
                    }));
                var h = new SimpleJsonParser(app.Object, map.Object);

                await h.Call(env);

                env.Request.Body.Should().Be(json);
            }

            [Fact]
            public async Task EncodesObjectBody()
            {
                var env = new StubEnv()
                {
                    Request = { Body = new object() },
                    Response = { Body = JsonEncoder.Encode("hi") }
                };
                var app = MoqExtensions.ApplicationMock();
                var map = new Mock<IGitHubModelMap>();
                map.Setup(x => x.For(It.IsAny<object>()))
                    .Returns(JObject.CreateObject(
                    new Dictionary<string, JObject>{
                        {"test", JObject.CreateString("value")}
                    }));
                var h = new SimpleJsonParser(app.Object, map.Object);

                await h.Call(env);

                env.Request.Body.Should().Be("{\"test\":\"value\"}");
            }
        }

        public class TheAfterMethod
        {
            [Fact]
            public async Task DeserializesResponse()
            {
                var data = "works";
                var env = new StubEnv();
                env.Response.Body = JsonEncoder.Encode(data);
                var app = new Mock<IApplication>();
                app.Setup(x => x.Call(env))
                    .Returns(Task.FromResult(app.Object));
                var map = new Mock<IGitHubModelMap>();
                map.Setup(x => x.For<string>(It.IsAny<JObject>())).Returns(data);
                var h = new SimpleJsonParser(app.Object, map.Object);

                await h.Call(env);

                env.Response.BodyAsObject.Should().NotBeNull();
                env.Response.BodyAsObject.Should().Be(data);
                map.Verify(x => x.For<string>(It.IsAny<JObject>()));
            }
        }
    }
}
