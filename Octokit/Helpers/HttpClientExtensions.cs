using System.Threading;
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
{
    public static class HttpClientExtensions
    {
        public static Task<IResponse<T>> Send<T>(this IHttpClient httpClient, IRequest request)
        {
            Ensure.ArgumentNotNull(httpClient, "httpClient");
            Ensure.ArgumentNotNull(request, "request");

            return httpClient.Send<T>(request, CancellationToken.None);
        }
    }
}