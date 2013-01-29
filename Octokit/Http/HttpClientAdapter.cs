using System;
using System.Linq;
using System.Net;
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
            
            var response = new ApiResponse<T>
            {
                Body = await responseMessage
                    .EnsureSuccess()
                    .Content
                    .ReadAsStringAsync()
            };

            foreach (var h in responseMessage.Headers)
                response.Headers.Add(h.Key, h.Value.First());

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

    internal static class HttpClientAdapterExtensions
    {
        public static HttpResponseMessage EnsureSuccess(this HttpResponseMessage response)
        {
            Ensure.ArgumentNotNull(response, "response");

            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            var content = response.Content;
            if (content != null)
            {
                content.Dispose();
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
                throw new AuthenticationException("You must be authenticated to call this method. Either supply a " +
                    "login/password or an oauth token.", response.StatusCode);

            // TODO: Flesh this out.
            throw new HttpRequestException("Unknown exception occurred.");
        }
    }
}
