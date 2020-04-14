using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit.Internal
{
    public static class TestSetup
    {
        public static Task<IApiResponse<object>> CreateApiResponse(HttpStatusCode statusCode)
        {
            var response = CreateResponse(statusCode);
            return Task.FromResult<IApiResponse<object>>(new ApiResponse<object>(response));
        }

        public static IResponse CreateResponse(HttpStatusCode statusCode)
        {
            object body = null;
            return CreateResponse(statusCode, body);
        }

        public static IResponse CreateResponse(HttpStatusCode statusCode, IDictionary<string, string> headers)
        {
            var response = new Response(statusCode, null, headers, "application/json");
            return response;
        }

        public static IResponse CreateResponse(HttpStatusCode statusCode, object body)
        {
            var response = new Response(statusCode, body, new Dictionary<string, string>(), "application/json");
            return response;
        }

        public static IResponse CreateResponse(HttpStatusCode statusCode, object body, IDictionary<string, string> headers)
        {
            return CreateResponse(statusCode, body, headers, "application/json");
        }

        public static IResponse CreateResponse(HttpStatusCode statusCode, object body, IDictionary<string, string> headers, string contentType)
        {
            var response = new Response(statusCode, body, headers, contentType);
            return response;
        }
    }
}
