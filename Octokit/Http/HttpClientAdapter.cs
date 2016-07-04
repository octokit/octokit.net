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
        readonly HttpClient _http;

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public HttpClientAdapter(Func<HttpMessageHandler> getHandler)
        {
            Ensure.ArgumentNotNull(getHandler, "getHandler");

            _http = new HttpClient(new RedirectHandler { InnerHandler = getHandler() });
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

            var response = await GetSuccessfulResponse(() => GetRequest(request), cancellationTokenForRequest);

            return await BuildResponse(response).ConfigureAwait(false);
        }

        private HttpRequestMessage GetRequest(IRequest request)
        {
            var requestMessage = BuildRequestMessage(request);
            return requestMessage;
        }

        private async Task<HttpResponseMessage> GetSuccessfulResponse(Func<HttpRequestMessage> request, CancellationToken cancellationToken)
        {
            var requestToSend = request();
            var response = await SendRequest(requestToSend, cancellationToken).ConfigureAwait(false);

            // Can't redirect without somewhere to redirect too.  Throw?
            if (response.Headers.Location == null) return response;

            if (response.StatusCode == HttpStatusCode.SeeOther)
            {
                requestToSend.Content = null;
                requestToSend.Method = HttpMethod.Get;
            }
            else
            {
                if (requestToSend.Content != null && response.RequestMessage.Content.Headers.ContentLength != 0)
                {
                    var stream = await response.RequestMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    if (stream.CanSeek)
                    {
                        stream.Position = 0;
                    }
                    else
                    {
                        throw new Exception("Cannot redirect a request with an unbuffered body");
                    }
                    newRequest.Content = new StreamContent(stream);
                }
                else
                {
                    newRequest.Content = null;
                }
            }
            newRequest.RequestUri = response.Headers.Location;

            if (string.Compare(newRequest.RequestUri.Host, newRequest.RequestUri.Host, StringComparison.OrdinalIgnoreCase) != 0)
            {
                newRequest.Headers.Authorization = null;
            }

            // Don't redirect if we exceed max number of redirects
            //var redirectCount = 0;
            //if (requestMessage.Properties.Keys.Contains(RedirectCountKey))
            //{
            //    redirectCount = (int)request.Properties[RedirectCountKey];
            //}
            //if (redirectCount > 3)
            //{
            //    throw new InvalidOperationException("The redirect count for this request has been exceeded. Aborting.");
            //}
            //request.Properties[RedirectCountKey] = ++redirectCount;

            if (response.StatusCode == HttpStatusCode.MovedPermanently
                        || response.StatusCode == HttpStatusCode.Redirect
                        || response.StatusCode == HttpStatusCode.Found
                        || response.StatusCode == HttpStatusCode.SeeOther
                        || response.StatusCode == HttpStatusCode.TemporaryRedirect
                        || (int)response.StatusCode == 308)
            {                                
                response = await GetSuccessfulResponse(() => CopyRequest(response.RequestMessage, response), cancellationToken).ConfigureAwait(false);
            }

            return response;
        }

        private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {            
            var response = await _http.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);
            return response;
        }        

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private HttpRequestMessage CopyRequest(HttpRequestMessage oldRequest, HttpResponseMessage response)
        {
            var newRequest = new HttpRequestMessage(oldRequest.Method, oldRequest.RequestUri);

            foreach (var header in oldRequest.Headers)
            {
                newRequest.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
            foreach (var property in oldRequest.Properties)
            {
                newRequest.Properties.Add(property);
            }

            return newRequest;
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

        protected virtual async Task<IResponse> BuildResponse(HttpResponseMessage responseMessage)
        {
            Ensure.ArgumentNotNull(responseMessage, "responseMessage");

            object responseBody = null;
            string contentType = null;

            // We added support for downloading images,zip-files and application/octet-stream. 
            // Let's constrain this appropriately.
            var binaryContentTypes = new[] {
                "application/zip" ,
                "application/x-gzip" ,
                "application/octet-stream"};

            using (var content = responseMessage.Content)
            {
                if (content != null)
                {
                    contentType = GetContentMediaType(responseMessage.Content);

                    if (contentType != null && (contentType.StartsWith("image/") || binaryContentTypes
                        .Any(item => item.Equals(contentType, StringComparison.OrdinalIgnoreCase))))
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

    internal class RedirectHandler : DelegatingHandler
    {
        public const string RedirectCountKey = "RedirectCount";
        public bool Enabled { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

           
        }

        
    }
}
