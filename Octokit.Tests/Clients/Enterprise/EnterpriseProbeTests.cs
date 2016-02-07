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
        [InlineData("")]
        [InlineData("\r\n")]
        [InlineData("\n")]
        public async Task ReturnsExistsFor200ResponseWithSha1(string lineEnding)
        {
            object responseBody = "ac2e56021f6d357663e5c112bdeb591b403dd4d2" + lineEnding;
            var response = Substitute.For<IResponse>();
            response.StatusCode.Returns(HttpStatusCode.OK);
            response.Body.Returns(responseBody);
            response.Headers["Server"].Returns("GitHub.com");
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

        [Theory]
        [InlineData("ac2e56021f6d357663e5c112bdeb591b403dd4d2A")] // 41 chars instead of 40
        [InlineData("ac2e56021f6d357663e5c112bdeb591b403dd4dz")] // 40 chars, but one out of range
        public async Task ReturnsDefinitelyNotEnterpriseExistsForNotSha1(object responseBody)
        {
            var response = Substitute.For<IResponse>();
            response.StatusCode.Returns(HttpStatusCode.OK);
            response.Body.Returns(responseBody);
            response.Headers["Server"].Returns("GitHub.com");
            var productHeader = new ProductHeaderValue("GHfW", "99");
            var httpClient = Substitute.For<IHttpClient>();
            httpClient.Send(Args.Request, CancellationToken.None).Returns(Task.FromResult(response));
            var enterpriseProbe = new EnterpriseProbe(productHeader, httpClient);

            var result = await enterpriseProbe.Probe(new Uri("https://example.com/"));

            Assert.Equal(EnterpriseProbeResult.NotFound, result);
        }

        [Fact]
        public async Task ReturnsDefinitelyNotEnterpriseExistsForNot200Response()
        {
            object responseBody = "ac2e56021f6d357663e5c112bdeb591b403dd4d2";
            var response = Substitute.For<IResponse>();
            response.StatusCode.Returns(HttpStatusCode.Redirect);
            response.Body.Returns(responseBody);
            response.Headers["Server"].Returns("GitHub.com");
            var productHeader = new ProductHeaderValue("GHfW", "99");
            var httpClient = Substitute.For<IHttpClient>();
            httpClient.Send(Args.Request, CancellationToken.None).Returns(Task.FromResult(response));
            var enterpriseProbe = new EnterpriseProbe(productHeader, httpClient);

            var result = await enterpriseProbe.Probe(new Uri("https://example.com/"));

            Assert.Equal(EnterpriseProbeResult.NotFound, result);
        }

        [Fact]
        public async Task ReturnsDefinitelyNotEnterpriseExistsForNotGitHubDotComServer()
        {
            object responseBody = "ac2e56021f6d357663e5c112bdeb591b403dd4d2";
            var response = Substitute.For<IResponse>();
            response.StatusCode.Returns(HttpStatusCode.OK);
            response.Body.Returns(responseBody);
            response.Headers["Server"].Returns("NotGitHub.com");
            var productHeader = new ProductHeaderValue("GHfW", "99");
            var httpClient = Substitute.For<IHttpClient>();
            httpClient.Send(Args.Request, CancellationToken.None).Returns(Task.FromResult(response));
            var enterpriseProbe = new EnterpriseProbe(productHeader, httpClient);

            var result = await enterpriseProbe.Probe(new Uri("https://example.com/"));

            Assert.Equal(EnterpriseProbeResult.NotFound, result);
        }

        [Fact]
        public async Task ReturnsDefinitelyNotEnterpriseExistsForException()
        {
            var httpClient = Substitute.For<IHttpClient>();
            httpClient.Send(Args.Request, CancellationToken.None).ThrowsAsync(new InvalidOperationException());
            var productHeader = new ProductHeaderValue("GHfW", "99");
            var enterpriseProbe = new EnterpriseProbe(productHeader, httpClient);

            var result = await enterpriseProbe.Probe(new Uri("https://example.com/"));

            Assert.Equal(EnterpriseProbeResult.Failed, result);
        }
    }
}
