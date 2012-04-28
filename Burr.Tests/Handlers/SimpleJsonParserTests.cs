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
using Burr.SimpleJSON;

namespace Burr.Tests
{
    public class SimpleJsonResponseHandlerTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new SimpleJsonParser(null, Mock.Of<IApiObjectMap>()));
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
                env.Response.Body = JSONEncoder.Encode(data);
                var app = new Mock<IApplication>();
                app.Setup(x => x.Call(env))
                    .Returns(Task.FromResult(app.Object));
                var map = new Mock<IApiObjectMap>();
                map.Setup(x => x.For<string>(It.IsAny<JObject>())).Returns(data);
                var h = new SimpleJsonParser(app.Object, map.Object);

                await h.Call(env);

                env.Request.Headers.Should().ContainKey("Accept");
                env.Request.Headers["Accept"].Should().Be("application/json");

            }
        }

        public class TheAfterMethod
        {
            [Fact]
            public async Task DeserializesResponse()
            {
                var data = "works";
                var env = new StubEnv();
                env.Response.Body = JSONEncoder.Encode(data);
                var app = new Mock<IApplication>();
                app.Setup(x => x.Call(env))
                    .Returns(Task.FromResult(app.Object));
                var map = new Mock<IApiObjectMap>();
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
