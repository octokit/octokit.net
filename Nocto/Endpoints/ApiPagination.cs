using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Nocto.Endpoints
{
    /// <summary>
    /// Used to paginate through API response results.
    /// </summary>
    /// <remarks>
    /// This is meant to be internal, but I factored it out so we can change our mind more easily later.
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class ApiPagination<T> : IApiPagination<T>
    {
        public async Task<IReadOnlyCollection<T>> GetAllPages(Func<Task<IReadOnlyPagedCollection<T>>> getFirstPage)
        {
            var page = await getFirstPage();
            var allItems = new List<T>(page);
            while ((page = await page.GetNextPage()) != null)
            {
                allItems.AddRange(page);
            }
            return new ReadOnlyCollection<T>(allItems);
        }
    }
}
