using System;
using System.Collections.Generic;
using System.Net;
using Octokit.Caching;
using Xunit;

namespace Octokit.Tests.Caching
{
    public class CachedResponseTests
    {
        public class V1
        {
            public class TheCtor
            {
                [Fact]
                public void EnsuresNonNullHeaders()
                {
                    Assert.Throws<ArgumentNullException>(() => new CachedResponse.V1("body", null, new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()), HttpStatusCode.OK, "content-type"));
                }

                [Fact]
                public void AllowsParametersOtherThanHeadersToBeNull()
                {
                    new CachedResponse.V1(null, new Dictionary<string, string>(), null, HttpStatusCode.OK, null);
                }

                [Fact]
                public void SetsProperties()
                {
                    var body = new object();
                    var headers = new Dictionary<string, string>();
                    var apiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit());
                    const HttpStatusCode httpStatusCode = HttpStatusCode.OK;
                    const string contentType = "content-type";

                    var v1 = new CachedResponse.V1(body, headers, apiInfo, httpStatusCode, contentType);

                    Assert.Equal(body, v1.Body);
                    Assert.Equal(headers, v1.Headers);
                    Assert.Equal(apiInfo, v1.ApiInfo);
                    Assert.Equal(httpStatusCode, v1.StatusCode);
                    Assert.Equal(contentType, v1.ContentType);
                }
            }
        }

    }
}
