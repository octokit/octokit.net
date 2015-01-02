using System;
using System.Collections.Generic;
using System.Net;

namespace Octokit.Internal
{
    public class ApiResponse<T> : IApiResponse<T>
    {
        public ApiResponse()
        {
            Headers = new Dictionary<string, string>();
        }

        public ApiResponse(IResponse response) : this(response, GetBodyAsObject(response))
        {
        }

        public ApiResponse(IResponse response, T bodyAsObject)
        {
            Ensure.ArgumentNotNull(response, "response");

            var body = response.Body is T ? (T)response.Body : default(T);
            HttpResponse = response;
            Headers = response.Headers;
            Body = body;
            ResponseUri = response.ResponseUri;
            ApiInfo = response.ApiInfo;
            StatusCode = response.StatusCode;
            ContentType = response.ContentType;
            Body = bodyAsObject;
        }

        public T Body { get; private set; }
        public Dictionary<string, string> Headers { get; private set; }
        public Uri ResponseUri { get; set; }
        public ApiInfo ApiInfo { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public IResponse HttpResponse { get; private set; }
        public string ContentType { get; set; }

        static T GetBodyAsObject(IResponse response)
        {
            if (response == null) return default(T);
            var body = response.Body;
            return body is T ? (T)body : default(T);
        }
    }
}
