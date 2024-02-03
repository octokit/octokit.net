using System.Threading;
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
{
    public static class HttpExtensions
    {
        public static Task<IResponse> Send(this IHttpClient httpClient, IRequest request)
        {
            Ensure.ArgumentNotNull(httpClient, nameof(httpClient));
            Ensure.ArgumentNotNull(request, nameof(request));

            return httpClient.Send(request, CancellationToken.None);
        }

        /// <summary>
        /// Gets a value that indicates whether the HTTP response was successful.
        /// </summary>
        public static bool IsSuccessStatusCode(this IResponse response)
        {
            Ensure.ArgumentNotNull(response, nameof(response));
            return (int) response.StatusCode >= 200 && (int) response.StatusCode <= 299;
        }
    }
}