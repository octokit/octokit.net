using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;

namespace Octokit.Internal
{
    public static class HttpMessageHandlerFactory
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static HttpClientHandler GetHandler()
        {
            return GetHandler(null);
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static HttpClientHandler GetHandler(IWebProxy proxy)
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };
#if !PORTABLE
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }
            if (handler.SupportsProxy && proxy != null)
            {
                handler.UseProxy = true;
                handler.Proxy = proxy;
            }
#endif
            return handler;
        }
    }
}
