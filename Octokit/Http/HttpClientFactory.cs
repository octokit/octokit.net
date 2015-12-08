using System.Net.Http;
using Octokit.Internal;

namespace Octokit
{
    public static class HttpClientFactory
    {
        public static HttpClient Create()
        {
            var handler = HttpMessageHandlerFactory.CreateDefault();

            var http = new HttpClient(new RedirectHandler { InnerHandler = handler });

            return http;
        }

    }
}
