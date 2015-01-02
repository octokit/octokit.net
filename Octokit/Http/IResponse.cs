using System;
using System.Collections.Generic;
using System.Net;

namespace Octokit
{
    public interface IApiResponse<out T>
    {
        T Body { get; }

        ApiInfo ApiInfo { get; }

        HttpStatusCode StatusCode { get; }

        IResponse HttpResponse { get; }
    }

    public interface IResponse
    {
        object BodyAsObject { get; set; }
        string Body { get; set; }
        Dictionary<string, string> Headers { get; }
        Uri ResponseUri { get; set; }
        ApiInfo ApiInfo { get; set; }
        HttpStatusCode StatusCode { get; set; }
        string ContentType { get; set; }
    }

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
