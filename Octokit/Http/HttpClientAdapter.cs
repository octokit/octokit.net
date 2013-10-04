﻿using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.Http
{
    public class HttpClientAdapter : IHttpClient
    {
        public async Task<IResponse<T>> Send<T>(IRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            var http = new HttpClient { BaseAddress = request.BaseAddress };
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
            using (var content = responseMessage.Content)
            {
                if (content != null)
                {
                    responseBody = await responseMessage.Content.ReadAsStringAsync();
                }
            }

            var response = new ApiResponse<T>
            {
                Body = responseBody,
                StatusCode = responseMessage.StatusCode,
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
                    requestMessage.Headers.Add(header.Key, header.Value);

                var body = request.Body as string;
                if (body != null)
                {
                    requestMessage.Content = new StringContent(body, Encoding.UTF8);
                }
                var bodyStream = request.Body as System.IO.Stream;
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
    }
}
