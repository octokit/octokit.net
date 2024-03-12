using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
            public async Task EnsuresNonNullArguments()
            {
                // arrange
                var underlyingClient = Substitute.For<IHttpClient>();
                var responseCache = Substitute.For<IResponseCache>();

                var cachingHttpClient = new CachingHttpClient(underlyingClient, responseCache);

                // act + assert
                await Assert.ThrowsAsync<ArgumentNullException>(() => cachingHttpClient.Send(null, CancellationToken.None));
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
            [MemberData(nameof(SuccessHttpStatusCodesWithSetCacheFailure))]
            public async Task UsesGithubResponseIfEtagIsPresentAndGithubReturnsSuccessCode(HttpStatusCode httpStatusCode, bool setCacheThrows)
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

            public static IEnumerable<object[]> SuccessHttpStatusCodesWithSetCacheFailure()
            {
                var setCacheFails = new[] { true, false };

                foreach (var cacheFail in setCacheFails)
                {
                    foreach (var statusCode in _successStatusCodes.Cast<object>())
                    {
                        yield return new[] { statusCode, cacheFail };
                        yield return new[] { statusCode, cacheFail };
                    }
                }
            }

            private static readonly IImmutableList<HttpStatusCode> _successStatusCodes = Enumerable
                .Range(200, 100)
                .Where(code => Enum.IsDefined(typeof(HttpStatusCode), code))
                .Cast<HttpStatusCode>()
                .ToImmutableList();

            private static readonly IImmutableList<string> _invalidETags = new[]
            {
                null, string.Empty
            }.ToImmutableList();
            
            public static IEnumerable<object[]> SuccessHttpStatusCodesWithSetCacheFailureAndInvalidETags()
            {
                var setCacheFails = new[] { true, false };
                foreach (var etag in _invalidETags)
                {
                    foreach (var cacheFail in setCacheFails)
                    {
                        foreach (var statusCode in _successStatusCodes.Cast<object>())
                        {
                            yield return new[] { statusCode, cacheFail, etag };
                            yield return new[] { statusCode, cacheFail, etag };
                        }
                    }
                }
            }

            [Theory]
            [MemberData(nameof(SuccessHttpStatusCodesWithSetCacheFailure))]
            public async Task UsesGithubResponseIfCachedEntryIsNull(HttpStatusCode httpStatusCode, bool setCacheThrows)
            {
                // arrange
                var underlyingClient = Substitute.For<IHttpClient>();
                var responseCache = Substitute.For<IResponseCache>();
                var request = Substitute.For<IRequest>();
                request.Method.Returns(HttpMethod.Get);
                request.Headers.Returns(new Dictionary<string, string>());

                var cancellationToken = CancellationToken.None;

                var githubResponse = Substitute.For<IResponse>();
                githubResponse.StatusCode.Returns(httpStatusCode);

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
            [MemberData(nameof(SuccessHttpStatusCodesWithSetCacheFailure))]
            public async Task UsesGithubResponseIfGetCachedEntryThrows(HttpStatusCode httpStatusCode, bool setCacheThrows)
            {
                // arrange
                var underlyingClient = Substitute.For<IHttpClient>();
                var responseCache = Substitute.For<IResponseCache>();
                var request = Substitute.For<IRequest>();
                request.Method.Returns(HttpMethod.Get);
                request.Headers.Returns(new Dictionary<string, string>());

                var cancellationToken = CancellationToken.None;

                var githubResponse = Substitute.For<IResponse>();
                githubResponse.StatusCode.Returns(httpStatusCode);

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
            [MemberData(nameof(SuccessHttpStatusCodesWithSetCacheFailureAndInvalidETags))]
            public async Task UsesGithubResponseIfCachedEntryEtagIsNullOrEmpty(HttpStatusCode httpStatusCode, bool setCacheThrows, string etag)
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
                githubResponse.StatusCode.Returns(httpStatusCode);

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

            public static IEnumerable<object[]> DoesNotUpdateCacheData()
            {
                var codesToExclude = _successStatusCodes
                    .Add(HttpStatusCode.NotModified);
                var failureCodes = Enum
                    .GetValues(typeof(HttpStatusCode))
                    .Cast<HttpStatusCode>()
                    .Except(codesToExclude)
                    .ToList();
                var hasCachedResponses = new[] { false, true };

                foreach (var etag in _invalidETags)
                {
                    foreach (var hasCachedResponse in hasCachedResponses)
                    {
                        foreach (var statusCode in failureCodes)
                        {
                            yield return new object[]
                            {
                                statusCode, hasCachedResponse, etag
                            };
                        }
                    }
                }
            }
            
            [Theory]
            [MemberData(nameof(DoesNotUpdateCacheData))]
            public async Task DoesNotUpdateCacheIfGitHubResponseIsNotSuccessCode(HttpStatusCode httpStatusCode, bool hasCachedResponse, string etag)
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

                var githubResponse = Substitute.For<IResponse>();
                githubResponse.StatusCode.Returns(httpStatusCode);

                underlyingClient.Send(request).Returns(githubResponse);
                responseCache.GetAsync(request).Returns(hasCachedResponse ? cachedV1Response : null);

                var cachingHttpClient = new CachingHttpClient(underlyingClient, responseCache);

                // act
                _ = await cachingHttpClient.Send(request, CancellationToken.None);

                // assert
                responseCache.DidNotReceiveWithAnyArgs().SetAsync(Arg.Any<IRequest>(), Arg.Any<CachedResponse.V1>());
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
