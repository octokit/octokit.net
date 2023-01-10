using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using Octokit.Caching;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Caching
{
    public class CachingHttpClientTests
    {
        public class TheSendMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                // arrange
                var underlyingClient = Substitute.For<IHttpClient>();
                var responseCache = Substitute.For<IResponseCache>();

                var cachingHttpClient = new CachingHttpClient(underlyingClient, responseCache);

                // act + assert
                Assert.ThrowsAsync<ArgumentNullException>(() => cachingHttpClient.Send(null, CancellationToken.None));
            }

            [Theory]
            [MemberData(nameof(NonGetHttpMethods))]
            public async Task CallsUnderlyingHttpClientMethodForNonGetRequests(HttpMethod method)
            {
                // arrange
                var underlyingClient = Substitute.For<IHttpClient>();
                var responseCache = Substitute.For<IResponseCache>();
                var request = Substitute.For<IRequest>();
                var cancellationToken = CancellationToken.None;
                var expectedResponse = Substitute.For<IResponse>();

                request.Method.Returns(method);
                underlyingClient.Send(request, cancellationToken).Returns(expectedResponse);

                var cachingHttpClient = new CachingHttpClient(underlyingClient, responseCache);

                // act
                var response = await cachingHttpClient.Send(request, CancellationToken.None);

                // assert
                Assert.Equal(expectedResponse, response);
                Assert.Empty(responseCache.ReceivedCalls());
                Assert.Single(underlyingClient.ReceivedCalls());
                underlyingClient.Received(1).Send(request, cancellationToken);
            }

            public static IEnumerable<object[]> NonGetHttpMethods()
            {
                yield return new object[] { HttpMethod.Delete };
                yield return new object[] { HttpMethod.Post };
                yield return new object[] { HttpMethod.Put };
                yield return new object[] { HttpMethod.Trace };
                yield return new object[] { HttpMethod.Options };
                yield return new object[] { HttpMethod.Head };
            }

            [Fact]
            public async Task UsesCachedResponseIfEtagIsPresentAndGithubReturns304()
            {
                // arrange
                var underlyingClient = Substitute.For<IHttpClient>();
                var responseCache = Substitute.For<IResponseCache>();
                const string etag = "my-etag";
                var request = Substitute.For<IRequest>();
                request.Method.Returns(HttpMethod.Get);
                request.Headers.Returns(new Dictionary<string, string>());

                var cachedResponse = Substitute.For<IResponse>();
                cachedResponse.Headers.Returns(new Dictionary<string, string> { { "ETag", etag } });

                var cachedV1Response = CachedResponse.V1.Create(cachedResponse);
                var cancellationToken = CancellationToken.None;

                var githubResponse = Substitute.For<IResponse>();
                githubResponse.StatusCode.Returns(HttpStatusCode.NotModified);

                underlyingClient.Send(Arg.Is<IRequest>(req => req == request && req.Headers["If-None-Matched"] == etag), cancellationToken).ReturnsForAnyArgs(githubResponse);
                responseCache.GetAsync(request).Returns(cachedV1Response);

                var cachingHttpClient = new CachingHttpClient(underlyingClient, responseCache);

                // act
                var response = await cachingHttpClient.Send(request, cancellationToken);

                // assert
                Assert.Equal(cachedV1Response.ToResponse(), response, new ResponseComparer());
                Assert.Single(underlyingClient.ReceivedCalls());
                underlyingClient.Received(1).Send(request, cancellationToken);
                Assert.Single(responseCache.ReceivedCalls());
                responseCache.Received(1).GetAsync(request);
            }

            [Theory]
            [MemberData(nameof(NonNotModifiedHttpStatusCodesWithSetCacheFailure))]
            public async Task UsesGithubResponseIfEtagIsPresentAndGithubReturnsNon304(HttpStatusCode httpStatusCode, bool setCacheThrows)
            {
                // arrange
                var underlyingClient = Substitute.For<IHttpClient>();
                var responseCache = Substitute.For<IResponseCache>();
                const string etag = "my-etag";
                var request = Substitute.For<IRequest>();
                request.Method.Returns(HttpMethod.Get);
                request.Headers.Returns(new Dictionary<string, string>());

                var cachedResponse = Substitute.For<IResponse>();
                cachedResponse.Headers.Returns(new Dictionary<string, string> { { "ETag", etag } });

                var cachedV1Response = CachedResponse.V1.Create(cachedResponse);
                var cancellationToken = CancellationToken.None;

                var githubResponse = Substitute.For<IResponse>();
                githubResponse.StatusCode.Returns(httpStatusCode);

                underlyingClient.Send(Arg.Is<IRequest>(req => req == request && req.Headers["If-None-Matched"] == etag), cancellationToken).ReturnsForAnyArgs(githubResponse);
                responseCache.GetAsync(request).Returns(cachedV1Response);
                if (setCacheThrows)
                {
                    responseCache.SetAsync(request, Arg.Any<CachedResponse.V1>()).ThrowsForAnyArgs<Exception>();
                }

                var cachingHttpClient = new CachingHttpClient(underlyingClient, responseCache);

                // act
                var response = await cachingHttpClient.Send(request, cancellationToken);

                // assert
                Assert.Equal(githubResponse, response, new ResponseComparer());
                Assert.Single(underlyingClient.ReceivedCalls());
                underlyingClient.Received(1).Send(request, cancellationToken);
                Assert.Equal(2, responseCache.ReceivedCalls().Count());
                responseCache.Received(1).GetAsync(request);
                responseCache.Received(1).SetAsync(request, Arg.Is<CachedResponse.V1>(v1 => new ResponseComparer().Equals(v1, CachedResponse.V1.Create(githubResponse))));
            }

            public static IEnumerable<object[]> NonNotModifiedHttpStatusCodesWithSetCacheFailure()
            {
                foreach (var statusCode in Enum.GetValues(typeof(HttpStatusCode)))
                {
                    if (statusCode.Equals(HttpStatusCode.NotModified)) continue;
                    yield return new[] { statusCode, true };
                    yield return new[] { statusCode, false };
                }
            }

            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public async Task UsesGithubResponseIfCachedEntryIsNull(bool setCacheThrows)
            {
                // arrange
                var underlyingClient = Substitute.For<IHttpClient>();
                var responseCache = Substitute.For<IResponseCache>();
                var request = Substitute.For<IRequest>();
                request.Method.Returns(HttpMethod.Get);
                request.Headers.Returns(new Dictionary<string, string>());

                var cancellationToken = CancellationToken.None;

                var githubResponse = Substitute.For<IResponse>();

                underlyingClient.Send(Arg.Is<IRequest>(req => req == request && !req.Headers.Any()), cancellationToken).ReturnsForAnyArgs(githubResponse);
                responseCache.GetAsync(request).ReturnsNull();
                if (setCacheThrows)
                {
                    responseCache.SetAsync(request, Arg.Any<CachedResponse.V1>()).ThrowsForAnyArgs<Exception>();
                }

                var cachingHttpClient = new CachingHttpClient(underlyingClient, responseCache);

                // act
                var response = await cachingHttpClient.Send(request, cancellationToken);

                // assert
                Assert.Equal(githubResponse, response, new ResponseComparer());
                Assert.Single(underlyingClient.ReceivedCalls());
                underlyingClient.Received(1).Send(request, cancellationToken);
                Assert.Equal(2, responseCache.ReceivedCalls().Count());
                responseCache.Received(1).GetAsync(request);
                responseCache.Received(1).SetAsync(request, Arg.Is<CachedResponse.V1>(v1 => new ResponseComparer().Equals(v1, CachedResponse.V1.Create(githubResponse))));
            }

            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public async Task UsesGithubResponseIfGetCachedEntryThrows(bool setCacheThrows)
            {
                // arrange
                var underlyingClient = Substitute.For<IHttpClient>();
                var responseCache = Substitute.For<IResponseCache>();
                var request = Substitute.For<IRequest>();
                request.Method.Returns(HttpMethod.Get);
                request.Headers.Returns(new Dictionary<string, string>());

                var cancellationToken = CancellationToken.None;

                var githubResponse = Substitute.For<IResponse>();

                underlyingClient.Send(Arg.Is<IRequest>(req => req == request && !req.Headers.Any()), cancellationToken).ReturnsForAnyArgs(githubResponse);
                responseCache.GetAsync(Args.Request).ThrowsForAnyArgs<Exception>();
                if (setCacheThrows)
                {
                    responseCache.SetAsync(request, Arg.Any<CachedResponse.V1>()).ThrowsForAnyArgs<Exception>();
                }

                var cachingHttpClient = new CachingHttpClient(underlyingClient, responseCache);

                // act
                var response = await cachingHttpClient.Send(request, cancellationToken);

                // assert
                Assert.Equal(githubResponse, response, new ResponseComparer());
                Assert.Single(underlyingClient.ReceivedCalls());
                underlyingClient.Received(1).Send(request, cancellationToken);
                Assert.Equal(2, responseCache.ReceivedCalls().Count());
                responseCache.Received(1).GetAsync(request);
                responseCache.Received(1).SetAsync(request, Arg.Is<CachedResponse.V1>(v1 => new ResponseComparer().Equals(v1, CachedResponse.V1.Create(githubResponse))));
            }

            [Theory]
            [InlineData(null, true)]
            [InlineData(null, false)]
            [InlineData("", true)]
            [InlineData("", false)]
            public async Task UsesGithubResponseIfCachedEntryEtagIsNullOrEmpty(string etag, bool setCacheThrows)
            {
                // arrange
                var underlyingClient = Substitute.For<IHttpClient>();
                var responseCache = Substitute.For<IResponseCache>();
                var request = Substitute.For<IRequest>();
                request.Method.Returns(HttpMethod.Get);
                request.Headers.Returns(new Dictionary<string, string>());

                var cachedResponse = Substitute.For<IResponse>();
                cachedResponse.Headers.Returns(etag == null ? new Dictionary<string, string>() : new Dictionary<string, string> { { "ETag", etag } });

                var cachedV1Response = CachedResponse.V1.Create(cachedResponse);
                var cancellationToken = CancellationToken.None;

                var githubResponse = Substitute.For<IResponse>();

                underlyingClient.Send(Arg.Is<IRequest>(req => req == request && !req.Headers.Any()), cancellationToken).ReturnsForAnyArgs(githubResponse);
                responseCache.GetAsync(request).Returns(cachedV1Response);
                if (setCacheThrows)
                {
                    responseCache.SetAsync(request, Arg.Any<CachedResponse.V1>()).ThrowsForAnyArgs<Exception>();
                }

                var cachingHttpClient = new CachingHttpClient(underlyingClient, responseCache);

                // act
                var response = await cachingHttpClient.Send(request, cancellationToken);

                // assert
                Assert.Equal(githubResponse, response, new ResponseComparer());
                Assert.Single(underlyingClient.ReceivedCalls());
                underlyingClient.Received(1).Send(request, cancellationToken);
                Assert.Equal(2, responseCache.ReceivedCalls().Count());
                responseCache.Received(1).GetAsync(request);
                responseCache.Received(1).SetAsync(request, Arg.Is<CachedResponse.V1>(v1 => new ResponseComparer().Equals(v1, CachedResponse.V1.Create(githubResponse))));
            }
        }

        public class TheSetRequestTimeoutMethod
        {
            [Fact]
            public void SetsRequestTimeoutForUnderlyingClient()
            {
                // arrange
                var underlyingClient = Substitute.For<IHttpClient>();
                var responseCache = Substitute.For<IResponseCache>();
                var timeout = TimeSpan.Zero;

                var cachingHttpClient = new CachingHttpClient(underlyingClient, responseCache);

                // act
                cachingHttpClient.SetRequestTimeout(timeout);

                // assert
                underlyingClient.Received(1).SetRequestTimeout(timeout);
            }
        }

        public class TheDisposeMethod
        {
            [Fact]
            public void CallsDisposeForUnderlyingClient()
            {
                // arrange
                var underlyingClient = Substitute.For<IHttpClient>();
                var responseCache = Substitute.For<IResponseCache>();

                var cachingHttpClient = new CachingHttpClient(underlyingClient, responseCache);

                // act
                cachingHttpClient.Dispose();

                // assert
                underlyingClient.Received(1).Dispose();
            }
        }

        public class TheCtor
        {
            [Theory]
            [InlineData(true, true)]
            [InlineData(true, false)]
            [InlineData(false, true)]
            public void EnsuresNonNullArguments(bool httpClientIsNull, bool responseCacheIsNull)
            {
                var httpClient = httpClientIsNull ? null : Substitute.For<IHttpClient>();
                var responseCache = responseCacheIsNull ? null : Substitute.For<IResponseCache>();

                Assert.Throws<ArgumentNullException>(() => new CachingHttpClient(httpClient, responseCache));
            }
        }
    }
}
