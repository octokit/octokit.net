using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http.Headers;

namespace Octokit.Internal
{
    /// <summary>
    /// Represents a generic HTTP response
    /// </summary>
    internal class Response : IResponse
    {
        public Response()
        {
        }

        public Response(HttpResponseHeaders headers)
        {
            Ensure.ArgumentNotNull(headers, nameof(headers));

            // Headers = new ReadOnlyDictionary<string, string>(headers);
            ApiInfo = ApiInfoParser.ParseResponseHeaders(headers);
        }

        public Response(HttpStatusCode statusCode, object body, HttpResponseHeaders headers, string contentType)
        {
            Ensure.ArgumentNotNull(headers, nameof(headers));

            StatusCode = statusCode;
            Body = body;
            //Headers = new ReadOnlyDictionary<string, string>(headers);
            ApiInfo = ApiInfoParser.ParseResponseHeaders(headers);
            ContentType = contentType;
        }

        /// <summary>
        /// Raw response body. Typically a string, but when requesting images, it will be a byte array.
        /// </summary>
        public object Body { get; private set; }
        /// <summary>
        /// Information about the API.
        /// </summary>
        [Obsolete("Can we live without this?")]
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
    }
}
