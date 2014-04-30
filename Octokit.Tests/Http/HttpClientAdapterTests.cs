using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Octokit.Internal;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Http
{
    public class HttpClientAdapterTests
    {
        public class TheBuildRequestMessageMethod
        {
            [Fact]
            public void AddsHeadersToRequestMessage()
            {
                var request = new Request
                {
                    Method = HttpMethod.Post,
                    Headers =
                    {
                        { "foo", "bar" },
                        { "blah", "blase" }
                    }
                };
                var tester = new HttpClientAdapterTester();
                
                var requestMessage = tester.BuildRequestMessageTester(request);

                Assert.Equal(2, requestMessage.Headers.Count());
                var firstHeader = requestMessage.Headers.First();
                Assert.Equal("foo", firstHeader.Key);
                Assert.Equal("bar", firstHeader.Value.First());
                var lastHeader = requestMessage.Headers.Last();
                Assert.Equal("blah", lastHeader.Key);
                Assert.Equal("blase", lastHeader.Value.First());
                Assert.Null(requestMessage.Content);
            }

            [Fact]
            public void SetsBodyAndContentType()
            {
                var request = new Request
                {
                    Method = HttpMethod.Post,
                    Body = "{}",
                    ContentType = "text/plain"
                };
                var tester = new HttpClientAdapterTester();

                var requestMessage = tester.BuildRequestMessageTester(request);

                Assert.NotNull(requestMessage.Content);
                Assert.Equal("text/plain", requestMessage.Content.Headers.ContentType.MediaType);
            }

            [Fact]
            public void SetsStreamBodyAndContentType()
            {
                var request = new Request
                {
                    Method = HttpMethod.Post,
                    Body = new MemoryStream(),
                    ContentType = "text/plain"
                };
                var tester = new HttpClientAdapterTester();

                var requestMessage = tester.BuildRequestMessageTester(request);

                Assert.NotNull(requestMessage.Content);
                Assert.IsType<StreamContent>(requestMessage.Content);
                Assert.Equal("text/plain", requestMessage.Content.Headers.ContentType.MediaType);
            }

            [Fact]
            public void SetsHttpContentBody()
            {
                var request = new Request
                {
                    Method = HttpMethod.Post,
                    Body = new FormUrlEncodedContent(new Dictionary<string, string> {{"foo", "bar"}})
                };
                var tester = new HttpClientAdapterTester();

                var requestMessage = tester.BuildRequestMessageTester(request);

                Assert.NotNull(requestMessage.Content);
                Assert.IsType<FormUrlEncodedContent>(requestMessage.Content);
                Assert.Equal("application/x-www-form-urlencoded", requestMessage.Content.Headers.ContentType.MediaType);
            }

            [Fact]
            public void EnsuresArguments()
            {
                var tester = new HttpClientAdapterTester();
                Assert.Throws<ArgumentNullException>(() => tester.BuildRequestMessageTester(null));
            }
        }

        public class TheBuildResponseMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.OK)]
            [InlineData(HttpStatusCode.NotFound)]
            public async Task BuildsResponseFromResponseMessage(HttpStatusCode httpStatusCode)
            {
                var responseMessage = new HttpResponseMessage {
                    StatusCode = httpStatusCode,
                    Content = new ByteArrayContent(Encoding.UTF8.GetBytes("{}")),
                    Headers =
                    {
                        {"peanut", "butter"},
                        {"ele", "phant"}
                    }
                };
                var tester = new HttpClientAdapterTester();

                var response = await tester.BuildResponseTester<object>(responseMessage);
                
                var firstHeader = response.Headers.First();
                Assert.Equal("peanut", firstHeader.Key);
                Assert.Equal("butter", firstHeader.Value);
                var lastHeader = response.Headers.Last();
                Assert.Equal("ele", lastHeader.Key);
                Assert.Equal("phant", lastHeader.Value);
                Assert.Equal("{}", response.Body);
                Assert.Equal(httpStatusCode, response.StatusCode);
                Assert.Null(response.ContentType);
            }

            [Fact]
            public async Task BuildsByteArrayResponseFromResponseMessage()
            {
                var responseMessage = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ByteArrayContent(new byte[] { 0, 1, 1, 0, 1}),
                };
                var tester = new HttpClientAdapterTester();

                var response = await tester.BuildResponseTester<byte[]>(responseMessage);

                Assert.Equal(new byte[] { 0, 1, 1, 0, 1 }, response.BodyAsObject);
                Assert.Null(response.Body);
                Assert.Null(response.ContentType);
            }

            public async Task SetsContentType(HttpStatusCode httpStatusCode)
            {
                var responseMessage = new HttpResponseMessage
                {
                    StatusCode = httpStatusCode,
                    Content = new StringContent("{}", Encoding.UTF8, "application/json"),
                };
                var tester = new HttpClientAdapterTester();

                var response = await tester.BuildResponseTester<object>(responseMessage);

                Assert.Equal("application/json", response.ContentType);
            }
        }

        sealed class HttpClientAdapterTester : HttpClientAdapter
        {
            public HttpRequestMessage BuildRequestMessageTester(IRequest request)
            {
                return BuildRequestMessage(request);
            }

            public async Task<IResponse<T>> BuildResponseTester<T>(HttpResponseMessage responseMessage)
            {
                return await BuildResponse<T>(responseMessage);
            }
        }
    }

    public class TheSendMethod
    {
        [Fact]
        public void EnsuresRequestNotNull()
        {
        }
    }
}
