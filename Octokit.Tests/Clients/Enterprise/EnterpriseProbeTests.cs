using System;
using System.Collections.Generic;
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

using static Octokit.Internal.TestSetup;

public class EnterpriseProbeTests
{
    public class TheProbeMethod
    {
        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ReturnsExistsForResponseWithCorrectHeadersRegardlessOfResponse(HttpStatusCode httpStatusCode)
        {
            var headers = new Dictionary<string, string>()
            {
                { "Server", "REVERSE-PROXY" },
                { "X-GitHub-Request-Id", Guid.NewGuid().ToString() }
            };
            var response = CreateResponse(httpStatusCode, headers);

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
            var headers = new Dictionary<string, string>()
            {
                { "Server", "GitHub.com" },
                { "X-GitHub-Request-Id", Guid.NewGuid().ToString() }
            };

            var apiException = new ApiException(CreateResponse(HttpStatusCode.OK, headers));

            var httpClient = Substitute.For<IHttpClient>();
            httpClient.Send(Args.Request, CancellationToken.None).ThrowsAsync(apiException);

            var productHeader = new ProductHeaderValue("GHfW", "99");
            var enterpriseProbe = new EnterpriseProbe(productHeader, httpClient);

            var result = await enterpriseProbe.Probe(new Uri("https://example.com/"));

            Assert.Equal(EnterpriseProbeResult.Ok, result);
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
