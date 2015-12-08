using System.Net.Http;
using Octokit.Internal;

namespace Octokit
{
    public static class HttpClientFactory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static HttpClient Create()
        {
            var handler = HttpMessageHandlerFactory.CreateDefault();

            var http = new HttpClient(new RedirectHandler { InnerHandler = handler });

            return http;
        }

    }
}
