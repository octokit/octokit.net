using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octopi.Http;

namespace Octopi.Endpoints
{
    public abstract class ApiEndpoint<T>
    {
        readonly IApiPagination<T> pagination;

        protected ApiEndpoint(IConnection connection) : this(connection, new ApiPagination<T>())
        {
        }

        protected ApiEndpoint(IConnection connection, IApiPagination<T> pagination)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(pagination, "pagination");

            Connection = connection;
            this.pagination = pagination;
        }

        protected IConnection Connection { get; private set; }

        protected async Task<T> Get(Uri endpoint)
        {
            return await GetItem<T>(endpoint);
        }

        protected async Task<TOther> GetItem<TOther>(Uri endpoint)
        {
            var response = await Connection.GetAsync<TOther>(endpoint);
            return response.BodyAsObject;
        }

        protected async Task<IReadOnlyCollection<T>> GetAll(Uri endpoint)
        {
            return await pagination.GetAllPages(async () => await GetPage(endpoint));
        }

        protected async Task<T> Create(Uri endpoint, object data)
        {
            var response = await Connection.PostAsync<T>(endpoint, data);

            return response.BodyAsObject;
        }

        protected async Task<T> Update(Uri endpoint, object data)
        {
            var response = await Connection.PatchAsync<T>(endpoint, data);

            return response.BodyAsObject;
        }

        protected async Task Delete(Uri endpoint)
        {
            await Connection.DeleteAsync<T>(endpoint);
        }

        async Task<IReadOnlyPagedCollection<T>> GetPage(Uri endpoint)
        {
            var response = await Connection.GetAsync<List<T>>(endpoint);
            return new ReadOnlyPagedCollection<T>(response, Connection);
        }

    }
}
