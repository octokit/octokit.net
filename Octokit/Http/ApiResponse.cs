using System;
using System.Collections.Generic;
using System.Net;

namespace Octokit.Internal
{
    public class ApiResponse<T> : IResponse<T>
    {
        public ApiResponse()
        {
            Headers = new Dictionary<string, string>();
        }

        object IResponse.BodyAsObject
        {
            get { return BodyAsObject; }
            set { BodyAsObject = (T)value; }
        }

        public string Body { get; set; }
        public T BodyAsObject { get; set; }
        public Dictionary<string, string> Headers { get; private set; }
        public Uri ResponseUri { get; set; }
        public ApiInfo ApiInfo { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; }
    }
}
