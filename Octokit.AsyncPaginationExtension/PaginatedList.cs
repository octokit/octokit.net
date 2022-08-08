using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Octokit.AsyncPaginationExtension
{
    internal class PaginatedList<T> : IPaginatedList<T>
    {
        private readonly LazyList<Task<IReadOnlyList<T>>> _pages;
        private readonly int _pageSize;

        internal PaginatedList(Func<ApiOptions, Task<IReadOnlyList<T>>> getPage, int pageSize)
        {
            _pages = new(i => getPage(new()
            {
                StartPage = i,
                PageSize = pageSize,
            }));
            _pageSize = pageSize;
        }

        private async Task<T?> Get(int index)
        {
            var page = await _pages[index / _pageSize].ConfigureAwait(false);
            index %= _pageSize;
            return page.Count > index ? page[index] : default;
        }

        public Task<T?> this[int index] => index >= 0 ? Get(index) : throw new ArgumentOutOfRangeException(nameof(index), index, "The index must be positive.");

        public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new())
        {
            var pageNum = 0;
            while (await _pages[pageNum++].ConfigureAwait(false) is { Count: > 0 } page)
            {
                foreach (var t in page)
                {
                    yield return t;
                }
            }
        }
    }
}
