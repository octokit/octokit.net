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

        public ApiResponse(IResponse response)
        {
            Ensure.ArgumentNotNull(response, "response");

            HttpResponse = response;
            Headers = response.Headers;
            Body = (T)response.BodyAsObject;
            ResponseUri = response.ResponseUri;
            ApiInfo = response.ApiInfo;
            StatusCode = response.StatusCode;
            ContentType = response.ContentType;
        }

        public ApiResponse(IResponse response, T bodyAsObject) : this(response)
        {
            Body = bodyAsObject;
        }

        public T Body { get; private set; }
        public Dictionary<string, string> Headers { get; private set; }
        public Uri ResponseUri { get; set; }
        public ApiInfo ApiInfo { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public IResponse HttpResponse { get; private set; }
        public string ContentType { get; set; }
    }
}
