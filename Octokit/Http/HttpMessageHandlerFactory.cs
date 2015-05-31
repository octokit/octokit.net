using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;

namespace Octokit.Internal
{
    public static class HttpMessageHandlerFactory
    {
        public static HttpMessageHandler CreateDefault()
        {
            return CreateDefault(null);
        }

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "proxy")]
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static HttpMessageHandler CreateDefault(IWebProxy proxy)
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
