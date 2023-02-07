using System;
using System.Collections.Generic;
using System.Net;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Octokit.Caching;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Caching
{
    public class CachedResponseTests
    {
        public class V1
        {
            public class TheToResponseMethod
            {
                [Fact]
                public void CreatesResponseWithSameProperties()
                {
                    var body = new object();
                    var headers = new Dictionary<string, string>();
                    var apiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit());
                    const HttpStatusCode httpStatusCode = HttpStatusCode.OK;
                    const string contentType = "content-type";
                    var v1 = new CachedResponse.V1(body, headers, apiInfo, httpStatusCode, contentType);

                    var response = v1.ToResponse();

                    Assert.Equal(new Response(httpStatusCode, body, headers, contentType), response, new ResponseComparer());
                }
            }

            public class TheCreateMethod
            {
                [Fact]
                public void EnsuresNonNullArguments()
                {
                    Assert.Throws<ArgumentNullException>(() => CachedResponse.V1.Create(null));
                }

                [Fact]
                public void EnsuresNonNullResponseHeader()
                {
                    var response = Substitute.For<IResponse>();
                    response.Headers.ReturnsNull();

                    Assert.Throws<ArgumentNullException>(() => CachedResponse.V1.Create(response));
                }

                [Fact]
                public void CreatesV1WithSameProperties()
                {
                    var response = Substitute.For<IResponse>();
                    response.Headers.Returns(new Dictionary<string, string>());

                    var v1 = CachedResponse.V1.Create(response);
                    Assert.Equal(response.Body, v1.Body);
                    Assert.Equal(response.Headers, v1.Headers);
                    Assert.Equal(response.ApiInfo, v1.ApiInfo);
                    Assert.Equal(response.StatusCode, v1.StatusCode);
                    Assert.Equal(response.ContentType, v1.ContentType);
                }
            }

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
