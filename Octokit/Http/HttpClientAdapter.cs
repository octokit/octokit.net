using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        readonly IWebProxy _webProxy;
        readonly HttpClient _http;
        
        public HttpClientAdapter() { }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public HttpClientAdapter(IWebProxy webProxy)
        {
            _webProxy = webProxy;
            var handler = GetHandler();
            _http = new HttpClient(new RedirectHandler { InnerHandler = handler });
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public HttpClientAdapter(IWebProxy webProxy, HttpMessageHandler handler)
        {
            Ensure.ArgumentNotNull(handler, "handler");

            _webProxy = webProxy;
            _http = new HttpClient(new RedirectHandler { InnerHandler = handler});
        }

        /// <summary>
        /// Sends the specified request and returns a response.
        /// </summary>
        /// <param name="request">A <see cref="IRequest"/> that represents the HTTP request</param>
        /// <param name="cancellationToken">Used to cancel the request</param>
        /// <returns>A <see cref="Task" /> of <see cref="IResponse"/></returns>
        public async Task<IResponse> Send(IRequest request, CancellationToken cancellationToken)
        {
            Ensure.ArgumentNotNull(request, "request");

            
            var cancellationTokenForRequest = GetCancellationTokenForRequest(request, cancellationToken);

            using (var requestMessage = BuildRequestMessage(request))
            {
                // Make the request
                var responseMessage = await _http.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancellationTokenForRequest)
                                                .ConfigureAwait(false);
                return await BuildResponse(responseMessage).ConfigureAwait(false);
            }
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        static CancellationToken GetCancellationTokenForRequest(IRequest request, CancellationToken cancellationToken)
        {
            var cancellationTokenForRequest = cancellationToken;

            if (request.Timeout != TimeSpan.Zero)
            {
                var timeoutCancellation = new CancellationTokenSource(request.Timeout);
                var unifiedCancellationToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCancellation.Token);

                cancellationTokenForRequest = unifiedCancellationToken.Token;
            }
            return cancellationTokenForRequest;
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        HttpClientHandler GetHandler()
        {
            var httpOptions = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };
            if (httpOptions.SupportsAutomaticDecompression)
            {
                httpOptions.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }
            if (httpOptions.SupportsProxy && _webProxy != null)
            {
                httpOptions.UseProxy = true;
                httpOptions.Proxy = _webProxy;
            }
            return httpOptions;
        }

        protected async virtual Task<IResponse> BuildResponse(HttpResponseMessage responseMessage)
        {
            Ensure.ArgumentNotNull(responseMessage, "responseMessage");

            object responseBody = null;
            string contentType = null;
            using (var content = responseMessage.Content)
            {
                if (content != null)
                {
                    contentType = GetContentMediaType(responseMessage.Content);

                    // We added support for downloading images and zip-files. Let's constrain this appropriately.
                    if (contentType != null && (contentType.StartsWith("image/") || contentType.Equals("application/zip", StringComparison.OrdinalIgnoreCase)))
                    {
                        responseBody = await responseMessage.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                    }
                    else
                    {
                        responseBody = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
                }
            }

            return new Response(
                responseMessage.StatusCode,
                responseBody,
                responseMessage.Headers.ToDictionary(h => h.Key, h => h.Value.First()),
                contentType);
        }

        protected virtual HttpRequestMessage BuildRequestMessage(IRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");
            HttpRequestMessage requestMessage = null;
            try
            {
                var fullUri = new Uri(request.BaseAddress, request.Endpoint);
                requestMessage = new HttpRequestMessage(request.Method, fullUri);

                requestMessage.Properties["AllowAutoRedirect"] = request.AllowAutoRedirect;
                foreach (var header in request.Headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }
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
                    requestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(request.ContentType);
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

        static string GetContentMediaType(HttpContent httpContent)
        {
            if (httpContent.Headers != null && httpContent.Headers.ContentType != null)
            {
                return httpContent.Headers.ContentType.MediaType;
            }
            return null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_http != null) _http.Dispose();
            }
        }
    }

    public class RedirectHandler : DelegatingHandler
    {
        public bool Enabled { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            var allowAutoRedirect = (bool)request.Properties["AllowAutoRedirect"];
            if (!allowAutoRedirect) return response;

            if (response.StatusCode == HttpStatusCode.MovedPermanently
                        || response.StatusCode == HttpStatusCode.Moved
                        || response.StatusCode == HttpStatusCode.Redirect
                        || response.StatusCode == HttpStatusCode.Found
                        || response.StatusCode == HttpStatusCode.SeeOther
                        || response.StatusCode == HttpStatusCode.RedirectKeepVerb
                        || response.StatusCode == HttpStatusCode.TemporaryRedirect
                        || (int)response.StatusCode == 308)
                    {

                        var newRequest = CopyRequest(response.RequestMessage);

                        if (response.StatusCode == HttpStatusCode.Redirect
                            || response.StatusCode == HttpStatusCode.Found
                            || response.StatusCode == HttpStatusCode.SeeOther)
                        {
                            newRequest.Content = null;
                            newRequest.Method = HttpMethod.Get;
                        }
                        newRequest.RequestUri = response.Headers.Location;

                        response = await base.SendAsync(newRequest, cancellationToken);
                    }

            return response;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        static HttpRequestMessage CopyRequest(HttpRequestMessage oldRequest)
        {
            var newrequest = new HttpRequestMessage(oldRequest.Method, oldRequest.RequestUri);

            foreach (var header in oldRequest.Headers)
            {
                newrequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
            foreach (var property in oldRequest.Properties)
            {
                newrequest.Properties.Add(property);
            }
            if (oldRequest.Content != null) newrequest.Content = new StreamContent(oldRequest.Content.ReadAsStreamAsync().Result);
            return newrequest;
        }
    }
}
