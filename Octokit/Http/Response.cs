using System;
using System.Collections.Generic;
using System.Net;

namespace Octokit.Internal
{
    public class Response : IResponse
    {
        public Response()
        {
            Headers = new Dictionary<string, string>();
        }

        public object BodyAsObject { get; set; }
        public string Body { get; set; }
        public Dictionary<string, string> Headers { get; private set; }
        public Uri ResponseUri { get; set; }
        public ApiInfo ApiInfo { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; }
    }
}