using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Nocto.Http;

namespace Nocto.Endpoints
{
    public abstract class ApiEndpoint<T>
    {
        readonly IGitHubClient client;

        protected ApiEndpoint(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");
            this.client = client;
        }

        protected async Task<IReadOnlyPagedCollection<T>> GetPageOfItems(Uri endpoint)
        {
            var response = await client.Connection.GetAsync<List<T>>(endpoint);
            return new ReadOnlyPagedCollection<T>(response, client.Connection);
        }

        protected async Task<IReadOnlyCollection<T>> GetAllPages(Func<Task<IReadOnlyPagedCollection<T>>> getFirstPage)
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
