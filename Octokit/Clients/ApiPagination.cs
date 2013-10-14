﻿using System;
using System.Collections.Generic;
#if NET_45
using System.Collections.ObjectModel;
#endif
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Used to paginate through API response results.
    /// </summary>
    /// <remarks>
    /// This is meant to be internal, but I factored it out so we can change our mind more easily later.
    /// </remarks>
    public class ApiPagination : IApiPagination
    {
        public async Task<IReadOnlyList<T>> GetAllPages<T>(Func<Task<IReadOnlyPagedCollection<T>>> getFirstPage)
        {
            Ensure.ArgumentNotNull(getFirstPage, "getFirstPage");

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
