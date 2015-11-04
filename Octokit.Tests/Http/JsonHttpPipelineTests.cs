using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Octokit.Internal;
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
            public void SetsRequestAcceptHeader()
            {
                var request = new Request();
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.SerializeRequest(request);

                Assert.Contains("Accept", request.Headers.Keys);
                Assert.Equal("application/vnd.github.quicksilver-preview+json; charset=utf-8, application/vnd.github.v3+json; charset=utf-8", request.Headers["Accept"]);
            }

            [Fact]
            public void DoesNotChangeExistingAcceptsHeader()
            {
                var request = new Request();
                request.Headers.Add("Accept", "application/vnd.github.v3; charset=utf-8");
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.SerializeRequest(request);

                Assert.Contains("Accept", request.Headers.Keys);
                Assert.Equal("application/vnd.github.v3; charset=utf-8", request.Headers["Accept"]);
            }

            [Fact]
            public void LeavesStringBodyAlone()
            {
                const string json = "just some string data";
                var request = new Request { Body = json };
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.SerializeRequest(request);

                Assert.Equal(json, request.Body);
            }

            [Fact]
            public void LeavesStreamBodyAlone()
            {
                var stream = new MemoryStream();
                var request = new Request { Body = stream };
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.SerializeRequest(request);

                Assert.Same(stream, request.Body);
            }

            [Fact]
            public void LeavesNullBodyAlone()
            {
                var request = new Request { Body = null };
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.SerializeRequest(request);

                Assert.Null(request.Body);
            }

            [Fact]
            public void EncodesObjectBody()
            {
                var request = new Request { Body = new { test = "value" } };
                var jsonPipeline = new JsonHttpPipeline();

                jsonPipeline.SerializeRequest(request);

                Assert.Equal("{\"test\":\"value\"}", request.Body);
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
                var httpResponse = new Response(
                    HttpStatusCode.OK,
                    SimpleJson.SerializeObject(data),
                    new Dictionary<string, string>(),
                    "application/json");
                var jsonPipeline = new JsonHttpPipeline();

                var response = jsonPipeline.DeserializeResponse<string>(httpResponse);

                Assert.NotNull(response.Body);
                Assert.Equal(data, response.Body);
            }

            [Fact]
            public void IgnoresResponsesNotIdentifiedAsJsonWhenNotDeserializingToString()
            {
                const string data = "works";
                var httpResponse = new Response(
                    HttpStatusCode.OK,
                    SimpleJson.SerializeObject(data),
                    new Dictionary<string, string>(),
                    "text/html");
                var jsonPipeline = new JsonHttpPipeline();

                var response = jsonPipeline.DeserializeResponse<Commit>(httpResponse);

                Assert.Null(response.Body);
            }

            [Fact]
            public void DeserializesSingleObjectResponseIntoCollectionWithOneItem()
            {
                const string data = "{\"name\":\"Haack\"}";
                var jsonPipeline = new JsonHttpPipeline();
                var httpResponse = new Response(
                    HttpStatusCode.OK,
                    data,
                    new Dictionary<string, string>(),
                    "application/json");

                var response = jsonPipeline.DeserializeResponse<List<SomeObject>>(httpResponse);

                Assert.Equal(1, response.Body.Count);
                Assert.Equal("Haack", response.Body.First().Name);
            }

            public class SomeObject
            {
                public string Name { get; set; }
            }

            [Fact]
            public void PerformsGitTagMapping()
            {
                const string data = @"{ ""tag"":""tag-name"",
                                        ""sha"": ""tag-sha"",
                                        ""url"": ""tag-url"",
                                        ""message"": ""tag-message"",
                                        ""tagger"": {
                                            ""name"": ""tagger-name"",
                                            ""email"": ""tagger-email"",
                                            ""date"": ""2011-06-17T14:53:35-07:00""
                                        },
                                        ""object"": {
                                            ""type"": ""commit"",
                                            ""sha"": ""object-sha"",
                                            ""url"": ""object-url""
                                        }}";
                var httpResponse = new Response(
                    HttpStatusCode.OK,
                    data,
                    new Dictionary<string, string>(),
                    "application/json");
                var jsonPipeline = new JsonHttpPipeline();

                var response = jsonPipeline.DeserializeResponse<GitTag>(httpResponse);

                Assert.NotNull(response.Body);
                Assert.Equal("tag-name", response.Body.Tag);
                Assert.Equal("tag-sha", response.Body.Sha);
                Assert.Equal("tag-url", response.Body.Url);
                Assert.Equal("tag-message", response.Body.Message);
                Assert.Equal("tagger-name", response.Body.Tagger.Name);
                Assert.Equal("tagger-email", response.Body.Tagger.Email);
                //Adjust expected date for time zone adjustment
                Assert.Equal(new DateTimeOffset(2011, 06, 17, 21, 53, 35, TimeSpan.Zero), response.Body.Tagger.Date);
                Assert.Equal(TaggedType.Commit, response.Body.Object.Type);
                Assert.Equal("object-sha", response.Body.Object.Sha);
                Assert.Equal("object-url", response.Body.Object.Url);
            }
        }
    }
}
