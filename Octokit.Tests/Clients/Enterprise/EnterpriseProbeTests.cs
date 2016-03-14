using System;
using System.Net;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Internal;
using Octokit.Tests;
using Xunit;

public class EnterpriseProbeTests
{
    public class TheProbeMethod
    {
        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ReturnsExistsForResponseWithCorrectHeadersRegardlessOfResponse(HttpStatusCode httpStatusCode)
        {
            var response = Substitute.For<IResponse>();
            response.StatusCode.Returns(httpStatusCode);
            response.Headers["Server"].Returns("GitHub.com");
            response.Headers.ContainsKey("X-GitHub-Request-Id").Returns(true);
            var productHeader = new ProductHeaderValue("GHfW", "99");
            var httpClient = Substitute.For<IHttpClient>();
            httpClient.Send(Args.Request, CancellationToken.None).Returns(Task.FromResult(response));
            var enterpriseProbe = new EnterpriseProbe(productHeader, httpClient);

            var result = await enterpriseProbe.Probe(new Uri("https://example.com/"));

            Assert.Equal(EnterpriseProbeResult.Ok, result);
            httpClient.Received(1).Send(Arg.Is<Request>(r =>
                r.BaseAddress == new Uri("https://example.com") &&
                r.Endpoint == new Uri("/site/sha", UriKind.Relative) &&
                r.Method == HttpMethod.Get &&
                r.Timeout == TimeSpan.FromSeconds(3) &&
                r.Headers["User-Agent"] == "GHfW/99"
                ), CancellationToken.None);
        }

        [Fact]
        public async Task ReturnsExistsForApiExceptionWithCorrectHeaders()
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = Substitute.For<IResponse>();
            response.Headers["Server"].Returns("GitHub.com");
            response.Headers.ContainsKey("X-GitHub-Request-Id").Returns(true);
            var apiException = new ApiException(response);
            httpClient.Send(Args.Request, CancellationToken.None).ThrowsAsync(apiException);
            var productHeader = new ProductHeaderValue("GHfW", "99");
            var enterpriseProbe = new EnterpriseProbe(productHeader, httpClient);

            var result = await enterpriseProbe.Probe(new Uri("https://example.com/"));

            Assert.Equal(EnterpriseProbeResult.Ok, result);
        }

        [Fact]
        public async Task ReturnsDefinitelyNotEnterpriseExistsForNotGitHubDotComServer()
        {
            object responseBody = "ac2e56021f6d357663e5c112bdeb591b403dd4d2";
            var response = Substitute.For<IResponse>();
            response.StatusCode.Returns(HttpStatusCode.OK);
            response.Body.Returns(responseBody);
            response.Headers["Server"].Returns("NotGitHub.com");
            response.Headers.ContainsKey("X-GitHub-Request-Id").Returns(true);
            var productHeader = new ProductHeaderValue("GHfW", "99");
            var httpClient = Substitute.For<IHttpClient>();
            httpClient.Send(Args.Request, CancellationToken.None).Returns(Task.FromResult(response));
            var enterpriseProbe = new EnterpriseProbe(productHeader, httpClient);

            var result = await enterpriseProbe.Probe(new Uri("https://example.com/"));

            Assert.Equal(EnterpriseProbeResult.NotFound, result);
        }

        [Fact]
        public async Task ReturnsDefinitelyNotEnterpriseExistsForUnknownException()
        {
            var httpClient = Substitute.For<IHttpClient>();
            httpClient.Send(Args.Request, CancellationToken.None).ThrowsAsync(new InvalidOperationException());
            var productHeader = new ProductHeaderValue("GHfW", "99");
            var enterpriseProbe = new EnterpriseProbe(productHeader, httpClient);

            var result = await enterpriseProbe.Probe(new Uri("https://example.com/"));

            Assert.Equal(EnterpriseProbeResult.Failed, result);
        }

        [Fact]
        public async Task ThrowsArgumentExceptionForGitHubDotComDomain()
        {
            var httpClient = Substitute.For<IHttpClient>();
            httpClient.Send(Args.Request, CancellationToken.None).ThrowsAsync(new InvalidOperationException());
            var productHeader = new ProductHeaderValue("GHfW", "99");
            var enterpriseProbe = new EnterpriseProbe(productHeader, httpClient);

            var result = await Assert.ThrowsAsync<ArgumentException>(async () => await enterpriseProbe.Probe(new Uri("https://github.com/")));

            Assert.Equal("enterpriseBaseUrl", result.ParamName);
        }
    }
}
