using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit.Caching
{
    public sealed class CachingHttpClient : IHttpClient
    {
        internal readonly IHttpClient _httpClient;
        internal readonly IResponseCache _responseCache;

        public CachingHttpClient(IHttpClient httpClient, IResponseCache responseCache)
        {
            Ensure.ArgumentNotNull(httpClient, nameof(httpClient));
            Ensure.ArgumentNotNull(responseCache, nameof(responseCache));

            _httpClient = httpClient is CachingHttpClient cachingHttpClient ? cachingHttpClient._httpClient : httpClient;
            _responseCache = responseCache;
        }

        public async Task<IResponse> Send(IRequest request, CancellationToken cancellationToken, Func<object, object> preprocessResponseBody = null)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            if (request.Method != HttpMethod.Get)
            {
                return await _httpClient.Send(request, cancellationToken, preprocessResponseBody);
            }

            var cachedResponse = await TryGetCachedResponse(request);
            if (cachedResponse != null && !string.IsNullOrEmpty(cachedResponse.ApiInfo.Etag))
            {
                request.Headers["If-None-Match"] = cachedResponse.ApiInfo.Etag;
                var conditionalResponse = await _httpClient.Send(request, cancellationToken);
                if (conditionalResponse.StatusCode == HttpStatusCode.NotModified)
                {
                    return cachedResponse;
                }

                _ = TrySetCachedResponse(request, conditionalResponse);
                return conditionalResponse;
            }

            var response = await _httpClient.Send(request, cancellationToken);
            _ = TrySetCachedResponse(request, response);
            return response;
        }

        private async Task<IResponse> TryGetCachedResponse(IRequest request)
        {
            try
            {
                return (await _responseCache.GetAsync(request))?.ToResponse();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task TrySetCachedResponse(IRequest request, IResponse response)
        {
            try
            {
                if(!response.IsSuccessStatusCode())
                {
                    return;
                }
                await _responseCache.SetAsync(request, CachedResponse.V1.Create(response));
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void SetRequestTimeout(TimeSpan timeout)
        {
            _httpClient.SetRequestTimeout(timeout);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
