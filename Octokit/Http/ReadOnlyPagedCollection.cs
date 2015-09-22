using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Octokit.Internal
{
    public class ReadOnlyPagedCollection<T> : ReadOnlyCollection<T>, IReadOnlyPagedCollection<T>
    {
        readonly ApiInfo _info;
        readonly Func<Uri, Task<IApiResponse<List<T>>>> _nextPageFunc;

        public ReadOnlyPagedCollection(IApiResponse<List<T>> response, Func<Uri, Task<IApiResponse<List<T>>>> nextPageFunc)
            : base(response != null ? response.Body ?? new List<T>() : new List<T>())
        {
            Ensure.ArgumentNotNull(response, "response");
            Ensure.ArgumentNotNull(nextPageFunc, "nextPageFunc");

            _nextPageFunc = nextPageFunc;
            if (response != null)
            {
                _info = response.HttpResponse.ApiInfo;
            }
        }

        public async Task<IReadOnlyPagedCollection<T>> GetNextPage()
        {
            var nextPageUrl = _info.GetNextPageUrl();
            if (nextPageUrl == null) return null;

            var response = await _nextPageFunc(nextPageUrl).ConfigureAwait(false);
            return new ReadOnlyPagedCollection<T>(response, _nextPageFunc);
        }
    }
}
