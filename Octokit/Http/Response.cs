using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;

namespace Octokit.Internal
{
    /// <summary>
    /// Represents a generic HTTP response
    /// </summary>
    internal class Response : IResponse
    {
        [Obsolete("Use the constructor with maximum parameters to avoid shortcuts")]
        public Response() : this(new Dictionary<string, string>())
        {
        }

        [Obsolete("Use the constructor with maximum parameters to avoid shortcuts")]
        public Response(IDictionary<string, string> headers)
        {
            Ensure.ArgumentNotNull(headers, nameof(headers));

            Headers = new ReadOnlyDictionary<string, string>(headers);
            ApiInfo = ApiInfoParser.ParseResponseHeaders(headers);
        }

        public Response(HttpStatusCode statusCode, object body, IDictionary<string, string> headers, string contentType)
        {
            Ensure.ArgumentNotNull(headers, nameof(headers));

            StatusCode = statusCode;
            Body = body;
            Headers = new ReadOnlyDictionary<string, string>(headers);
            ApiInfo = ApiInfoParser.ParseResponseHeaders(headers);
            ContentType = contentType;
        }

        /// <inheritdoc />
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
    }
}
