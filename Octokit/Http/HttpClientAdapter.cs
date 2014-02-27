using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit.Internal
{
    /// <summary>
    /// Generic Http client. Useful for those who want to swap out System.Net.HttpClient with something else.
    /// </summary>
    /// <remarks>
    /// Most folks won't ever need to swap this out. But if you're trying to run this on Windows Phone, you might.
    /// </remarks>
    public class HttpClientAdapter : IHttpClient
    {
        readonly IWebProxy webProxy;
        readonly HttpClient client;

        public HttpClientAdapter() : this(null) { }

        public HttpClientAdapter(IWebProxy webProxy)
        {
            this.webProxy = webProxy;
            this.client = new HttpClient(BuildMessageHandler(), true);
        }

        public async Task<IResponse<T>> Send<T>(IRequest request, CancellationToken cancellationToken)
        {
            Ensure.ArgumentNotNull(request, "request");

            using (var requestMessage = BuildRequestMessage(request))
            {
                var timeoutCancellationTokenSource = new CancellationTokenSource(request.Timeout);
                var timeoutCancellationToken = timeoutCancellationTokenSource.Token;

                var ct = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCancellationToken);

                // Make the request
                var responseMessage = await this.client.SendAsync(
                    requestMessage, 
                    HttpCompletionOption.ResponseContentRead, 
                    ct.Token).ConfigureAwait(false);

                return await BuildResponse<T>(responseMessage).ConfigureAwait(false);
            }
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", 
            Justification="It gets disposed of by the caller, this method won't ever crash :trollface:")]
        HttpMessageHandler BuildMessageHandler()
        {
#if NETFX_CORE || MONO
            var handler = new HttpClientHandler();

            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip;
            }
#else
            var handler = new System.Net.Http.WebRequestHandler();

            // Erring on the side of caution here with a revalidation cache level. While the API correctly
            // sends Vary headers for the Authorization header initial testing suggests that the backin cache 
            // of HttpClient does not. Meaning that two requests, one with a proper token and one with a fake would
            // still pick up the same object from cache. Some sadness but this is what we want, to ping the API 
            // but avoid the payload. I suspect this might be due to the API returning two Vary headers and the
            // cache layer picking the first but I have no evidence of that yet.
            handler.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.Revalidate);

            // Go read http://connect.microsoft.com/VisualStudio/feedback/details/492544 and then have a good cry
            handler.AutomaticDecompression = DecompressionMethods.None;
#endif

            handler.AllowAutoRedirect = true;

            if (handler.SupportsProxy && webProxy != null)
            {
                handler.UseProxy = true;
                handler.Proxy = webProxy;
            }

            return handler;
        }

        protected async virtual Task<IResponse<T>> BuildResponse<T>(HttpResponseMessage responseMessage)
        {
            Ensure.ArgumentNotNull(responseMessage, "responseMessage");

            string responseBody = null;
            string contentType = null;
            using (var content = responseMessage.Content)
            {
                if (content != null)
                {
#if !NETFX_CORE && !MONO
                    if (IsGZipEncoded(content.Headers))
                    {
                        using (var responseStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        using (var gz = new GZipStream(responseStream, CompressionMode.Decompress))
                        using (var sr = new StreamReader(gz, GetContentEncoding(content)))
                        {
                            responseBody = await sr.ReadToEndAsync().ConfigureAwait(false);
                        }
                    }
                    else
                    {
                        responseBody = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
#else
                    responseBody = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif

                    contentType = GetContentType(content);
                }
            }

            var response = new ApiResponse<T>
            {
                Body = responseBody,
                StatusCode = responseMessage.StatusCode,
                ContentType = contentType
            };

            foreach (var h in responseMessage.Headers)
            {
                response.Headers.Add(h.Key, h.Value.First());
            }

            return response;
        }

        protected virtual HttpRequestMessage BuildRequestMessage(IRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");
            HttpRequestMessage requestMessage = null;
            try
            {
                requestMessage = new HttpRequestMessage(request.Method, new Uri(request.BaseAddress, request.Endpoint));
                foreach (var header in request.Headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }

#if !NETFX_CORE && !MONO
                // The nice thing about a client library for a specific API is that we can ignore deflate.
                requestMessage.Headers.AcceptEncoding.Clear();
                requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
#endif

                var httpContent = request.Body as HttpContent;
                if (httpContent != null)
                {
                    requestMessage.Content = httpContent;
                }

                var body = request.Body as string;
                if (body != null)
                {
                    requestMessage.Content = new StringContent(body, Encoding.UTF8, request.ContentType);
                }

                var bodyStream = request.Body as Stream;
                if (bodyStream != null)
                {
                    requestMessage.Content = new StreamContent(bodyStream);
                    requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(request.ContentType);
                }
            }
            catch (Exception)
            {
                if (requestMessage != null)
                {
                    requestMessage.Dispose();
                }
                throw;
            }

            return requestMessage;
        }

        static string GetContentType(HttpContent httpContent)
        {
            if (httpContent.Headers != null && httpContent.Headers.ContentType != null)
            {
                return httpContent.Headers.ContentType.MediaType;
            }
            return null;
        }

#if !NETFX_CORE && !MONO
        static Encoding GetContentEncoding(HttpContent content)
        {
            return Encoding.GetEncoding(content.Headers.ContentType.CharSet);
        }

        static bool IsGZipEncoded(HttpContentHeaders headers)
        {
            // NB: This is overkill as there will only ever be one Content-Encoding but it's easy to read.
            return headers.ContentEncoding.Any(x => String.Equals(x, "gzip", StringComparison.OrdinalIgnoreCase));
        }
#endif

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.client.Dispose();
            }
        }
    }
}
