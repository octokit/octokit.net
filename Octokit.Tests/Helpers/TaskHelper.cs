using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit.Internal
{
    public static class TestSetup
    {
        public static Task<IApiResponse<object>> GetApiResponse(HttpStatusCode statusCode)
        {
            var response = new Response(statusCode, null, new Dictionary<string, string>(), "application/json");
            return Task.FromResult<IApiResponse<object>>(new ApiResponse<object>(response));

        }
    }
}
