using System.Collections.Generic;
using System.Linq;
using System.Net;
using Octokit.Internal;

namespace Octokit.Caching
{
    /// <remarks>
    /// When implementation details of <see cref="Response"/> changes:
    /// <list type="number">
    /// <item>mark <see cref="V1"/> as Obsolete</item>
    /// <item>create a V2</item>
    /// <item>update usages of <see cref="V1"/> to V2</item>
    /// </list>
    /// </remarks>
    public static class CachedResponse
    {
        public sealed class V1 : IResponse
        {
            public V1(object body, IReadOnlyDictionary<string, string> headers, ApiInfo apiInfo, HttpStatusCode statusCode, string contentType)
            {
                Ensure.ArgumentNotNull(headers, nameof(headers));

                StatusCode = statusCode;
                Body = body;
                Headers = headers;
                ApiInfo = apiInfo;
                ContentType = contentType;
            }

            /// <inheritdoc/>
            public object Body { get; private set; }
            /// <summary>
            /// Information about the API.
            /// </summary>
            public IReadOnlyDictionary<string, string> Headers { get; private set; }
            /// <summary>
            /// Information about the API response parsed from the response headers.
            /// </summary>
            public ApiInfo ApiInfo { get; internal set; } // This setter is internal for use in tests.
            /// <summary>
            /// The response status code.
            /// </summary>
            public HttpStatusCode StatusCode { get; private set; }
            /// <summary>
            /// The content type of the response.
            /// </summary>
            public string ContentType { get; private set; }

            internal Response ToResponse() =>
                new Response(StatusCode, Body, Headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value), ContentType);

            public static V1 Create(IResponse response)
            {
                Ensure.ArgumentNotNull(response, nameof(response));

                return new V1(response.Body, response.Headers, response.ApiInfo, response.StatusCode, response.ContentType);
            }
        }
    }
}
