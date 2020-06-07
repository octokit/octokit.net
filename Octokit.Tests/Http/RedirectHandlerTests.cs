using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Http
{
    public class RedirectHandlerTests
    {
        [Fact]
        public async Task OkStatusShouldPassThrough()
        {
            var handler = CreateMockHttpHandler(new HttpResponseMessage(HttpStatusCode.OK));
            var adapter = new HttpClientAdapter(handler);

            var httpRequestMessage = CreateRequest(HttpMethod.Get);

            var response = await adapter.SendAsync(httpRequestMessage, new CancellationToken());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Same(httpRequestMessage, response.RequestMessage);
        }

        [Theory]
        [InlineData(HttpStatusCode.MovedPermanently)]  // 301
        [InlineData(HttpStatusCode.Found)]  // 302
        [InlineData(HttpStatusCode.TemporaryRedirect)]  // 307
        public async Task ShouldRedirectSameMethod(HttpStatusCode statusCode)
        {
            var redirectResponse = new HttpResponseMessage(statusCode);
            redirectResponse.Headers.Location = new Uri("http://example.org/bar");

            var handler = CreateMockHttpHandler(redirectResponse, new HttpResponseMessage(HttpStatusCode.OK));
            var adapter = new HttpClientAdapter(handler);

            var httpRequestMessage = CreateRequest(HttpMethod.Post);

            var response = await adapter.SendAsync(httpRequestMessage, new CancellationToken());

            Assert.Equal(response.RequestMessage.Method, httpRequestMessage.Method);
            Assert.NotSame(response.RequestMessage, httpRequestMessage);
        }

        [Fact]
        public async Task Status303ShouldRedirectChangeMethod()
        {
            var redirectResponse = new HttpResponseMessage(HttpStatusCode.SeeOther);
            redirectResponse.Headers.Location = new Uri("http://example.org/bar");

            var handler = CreateMockHttpHandler(redirectResponse, new HttpResponseMessage(HttpStatusCode.OK));
            var adapter = new HttpClientAdapter(handler);

            var httpRequestMessage = CreateRequest(HttpMethod.Post);

            var response = await adapter.SendAsync(httpRequestMessage, new CancellationToken());

            Assert.Equal(HttpMethod.Get, response.RequestMessage.Method);
            Assert.NotSame(response.RequestMessage, httpRequestMessage);
        }

        [Fact]
        public async Task RedirectWithSameHostShouldKeepAuthHeader()
        {
            var redirectResponse = new HttpResponseMessage(HttpStatusCode.Redirect);
            redirectResponse.Headers.Location = new Uri("http://example.org/bar");

            var handler = CreateMockHttpHandler(redirectResponse, new HttpResponseMessage(HttpStatusCode.OK));
            var adapter = new HttpClientAdapter(handler);

            var httpRequestMessage = CreateRequest(HttpMethod.Get);
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("fooAuth", "aparam");

            var response = await adapter.SendAsync(httpRequestMessage, new CancellationToken());

            Assert.NotSame(response.RequestMessage, httpRequestMessage);
            Assert.Equal("fooAuth", response.RequestMessage.Headers.Authorization.Scheme);
        }


        [Theory]
        [InlineData(HttpStatusCode.MovedPermanently)]  // 301
        [InlineData(HttpStatusCode.Found)]  // 302
        [InlineData(HttpStatusCode.SeeOther)]  // 303
        [InlineData(HttpStatusCode.TemporaryRedirect)]  // 307
        public async Task RedirectWithDifferentHostShouldLoseAuthHeader(HttpStatusCode statusCode)
        {
            var redirectResponse = new HttpResponseMessage(statusCode);
            redirectResponse.Headers.Location = new Uri("http://example.net/bar");

            var handler = CreateMockHttpHandler(redirectResponse, new HttpResponseMessage(HttpStatusCode.OK));
            var adapter = new HttpClientAdapter(handler);

            var httpRequestMessage = CreateRequest(HttpMethod.Get);
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("fooAuth", "aparam");

            var response = await adapter.SendAsync(httpRequestMessage, new CancellationToken());

            Assert.NotSame(response.RequestMessage, httpRequestMessage);
            Assert.Null(response.RequestMessage.Headers.Authorization);
        }

        [Theory]
        [InlineData(HttpStatusCode.MovedPermanently)]  // 301
        [InlineData(HttpStatusCode.Found)]  // 302
        [InlineData(HttpStatusCode.TemporaryRedirect)]  // 307
        public async Task Status301ShouldRedirectPOSTWithBody(HttpStatusCode statusCode)
        {
            var redirectResponse = new HttpResponseMessage(statusCode);
            redirectResponse.Headers.Location = new Uri("http://example.org/bar");

            var handler = CreateMockHttpHandler(redirectResponse, new HttpResponseMessage(HttpStatusCode.OK));
            var adapter = new HttpClientAdapter(handler);

            var httpRequestMessage = CreateRequest(HttpMethod.Post);
            httpRequestMessage.Content = new StringContent("Hello World");

            var response = await adapter.SendAsync(httpRequestMessage, new CancellationToken());

            Assert.Equal(response.RequestMessage.Method, httpRequestMessage.Method);
            Assert.NotSame(response.RequestMessage, httpRequestMessage);
        }

        // POST see other with content
        [Fact]
        public async Task Status303ShouldRedirectToGETWithoutBody()
        {
            var redirectResponse = new HttpResponseMessage(HttpStatusCode.SeeOther);
            redirectResponse.Headers.Location = new Uri("http://example.org/bar");

            var handler = CreateMockHttpHandler(redirectResponse, new HttpResponseMessage(HttpStatusCode.OK));
            var adapter = new HttpClientAdapter(handler);

            var httpRequestMessage = CreateRequest(HttpMethod.Post);
            httpRequestMessage.Content = new StringContent("Hello World");

            var response = await adapter.SendAsync(httpRequestMessage, new CancellationToken());

            Assert.Equal(HttpMethod.Get, response.RequestMessage.Method);
            Assert.NotSame(response.RequestMessage, httpRequestMessage);
            Assert.Null(response.RequestMessage.Content);
        }

        [Fact]
        public async Task Exceed3RedirectsShouldReturn()
        {
            var redirectResponse = new HttpResponseMessage(HttpStatusCode.Found);
            redirectResponse.Headers.Location = new Uri("http://example.org/bar");

            var redirectResponse2 = new HttpResponseMessage(HttpStatusCode.Found);
            redirectResponse2.Headers.Location = new Uri("http://example.org/foo");

            var handler = CreateMockHttpHandler(redirectResponse, redirectResponse2);
            var adapter = new HttpClientAdapter(handler);

            var httpRequestMessage = CreateRequest(HttpMethod.Get);

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => adapter.SendAsync(httpRequestMessage, new CancellationToken()));
        }

        static HttpRequestMessage CreateRequest(HttpMethod method)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.RequestUri = new Uri("http://example.org/foo");
            httpRequestMessage.Method = method;
            return httpRequestMessage;
        }

        static Func<HttpMessageHandler> CreateMockHttpHandler(HttpResponseMessage httpResponseMessage1, HttpResponseMessage httpResponseMessage2 = null)
        {
            return () => new MockRedirectHandler(httpResponseMessage1, httpResponseMessage2);
        }
    }

    public class MockRedirectHandler : HttpMessageHandler
    {
        readonly HttpResponseMessage _response1;
        readonly HttpResponseMessage _response2;
        private bool _Response1Sent;

        public MockRedirectHandler(HttpResponseMessage response1, HttpResponseMessage response2 = null)
        {
            _response1 = response1;
            _response2 = response2;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!_Response1Sent)
            {
                _Response1Sent = true;
                _response1.RequestMessage = request;
                return _response1;
            }
            else
            {
                _response2.RequestMessage = request;
                return _response2;
            }
        }
    }
}
