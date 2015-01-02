using System.Collections.Generic;
using System.Net;

namespace Octokit.Internal
{
    public class ApiResponse<T> : IApiResponse<T>
    {
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
            StatusCode = response.StatusCode;
            ContentType = response.ContentType;
            Body = bodyAsObject;
        }

        public T Body { get; private set; }
        public IReadOnlyDictionary<string, string> Headers { get; private set; }
        public HttpStatusCode StatusCode { get; set; }
        public IResponse HttpResponse { get; private set; }
        public string ContentType { get; set; }

        static T GetBodyAsObject(IResponse response)
        {
            var body = response.Body;
            if (body is T) return (T)body;
            return default(T);
        }
    }
}
