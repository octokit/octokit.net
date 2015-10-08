using System.Threading;
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
{
    public static class HttpClientExtensions
    {
        public static Task<IResponse> Send(this IHttpClient httpClient, IRequest request)
        {
            Ensure.ArgumentNotNull(httpClient, "httpClient");
            Ensure.ArgumentNotNull(request, "request");

            return httpClient.Send(request, CancellationToken.None);
        }
    }
}