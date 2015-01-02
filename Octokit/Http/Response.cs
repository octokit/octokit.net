using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Octokit.Internal;

namespace Octokit.Internal
{
    public class Response : IResponse
    {
        public Response() : this(new Dictionary<string, string>())
        {
        }

        public Response(IDictionary<string, string> headers)
        {
            Ensure.ArgumentNotNull(headers, "headers");

            Headers = headers;
            ApiInfo = ApiInfoParser.ParseResponseHeaders(headers);
        }

        public Response(HttpStatusCode statusCode, object body, IDictionary<string, string> headers, string contentType)
        {
            Ensure.ArgumentNotNull(headers, "headers");

            StatusCode = statusCode;
            Body = body;
            Headers = headers;
            ApiInfo = ApiInfoParser.ParseResponseHeaders(headers);
            ContentType = contentType;
        }

        public object Body { get; set; }
        public IDictionary<string, string> Headers { get; private set; }
        public ApiInfo ApiInfo { get; private set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; private set; }
    }
}