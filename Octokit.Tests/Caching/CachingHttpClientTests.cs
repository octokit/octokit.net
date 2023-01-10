using System;
using NSubstitute;
using Octokit.Caching;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Caching
{
    public class CachingHttpClientTests
    {
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
