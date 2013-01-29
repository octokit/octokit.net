using System;
using FluentAssertions;
using Octokit.Http;
using Xunit;

namespace Octokit.Tests.Http
{
    public class JsonHttpPipelineTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new JsonHttpPipeline(null));
            }
        }

        public class TheSerializeRequestMethod
        {
            [Fact]
            public void SetsRequestHeader()
            {
                var request = new Request();
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.SerializeRequest(request);

                request.Headers.Should().ContainKey("Accept");
                request.Headers["Accept"].Should().Be("application/vnd.github.v3+json; charset=utf-8");
            }

            [Fact]
            public void LeavesStringBodyAlone()
            {
                const string json = "just some string data";
                var request = new Request { Body = json };
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.SerializeRequest(request);

                request.Body.Should().Be(json);
            }

            [Fact]
            public void EncodesObjectBody()
            {
                var request = new Request { Body = new { test = "value" } };
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.SerializeRequest(request);

                request.Body.Should().Be("{\"test\":\"value\"}");
            }

            [Fact]
            public void EnsuresArguments()
            {
                var jsonPipeline = new JsonHttpPipeline();

                Assert.Throws<ArgumentNullException>(() => jsonPipeline.SerializeRequest(null));
            }
        }

        public class TheDeserializeResponseMethod
        {
            [Fact]
            public void DeserializesResponse()
            {
                const string data = "works";
                var response = new ApiResponse<string> { Body = SimpleJson.SerializeObject(data) };
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.DeserializeResponse(response);

                response.BodyAsObject.Should().NotBeNull();
                response.BodyAsObject.Should().Be(data);
            }
        }
    }
}
