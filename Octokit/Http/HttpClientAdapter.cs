using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Internal
{
    public class HttpClientAdapter : IHttpClient
    {
        public async Task<IResponse<T>> Send<T>(IRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            var httpOptions = new HttpClientHandler
            {
                AllowAutoRedirect = request.AllowAutoRedirect
            };
            if (httpOptions.SupportsAutomaticDecompression)
            {
                httpOptions.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }

            var http = new HttpClient(httpOptions)
            {
                BaseAddress = request.BaseAddress,
                Timeout = request.Timeout
            };
            using (var requestMessage = BuildRequestMessage(request))
            {
                // Make the request
                var responseMessage = await http.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead);
                return await BuildResponse<T>(responseMessage);
            }
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
                    responseBody = await responseMessage.Content.ReadAsStringAsync();
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
                requestMessage = new HttpRequestMessage(request.Method, request.Endpoint);
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
    }
}
