using System;
using NSubstitute;
using Octokit.Caching;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Caching
{
    public class CachingHttpClientTests
    {
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
