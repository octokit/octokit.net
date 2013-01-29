using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit.Clients;

namespace Octokit.Http
{
    public class ApiConnection<T> : IApiConnection<T>
    {
        readonly IApiPagination<T> pagination;

        public ApiConnection(IConnection connection) : this(connection, new ApiPagination<T>())
        {
        }

        protected ApiConnection(IConnection connection, IApiPagination<T> pagination)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(pagination, "pagination");

            Connection = connection;
            this.pagination = pagination;
        }

        protected IConnection Connection { get; private set; }

        public async Task<T> Get(Uri endpoint)
        {
            return await GetItem<T>(endpoint);
        }

        public async Task<TOther> GetItem<TOther>(Uri endpoint)
        {
            var response = await Connection.GetAsync<TOther>(endpoint);
            return response.BodyAsObject;
        }

        public async Task<string> GetHtml(Uri endpoint)
        {
            var response = await Connection.GetHtml(endpoint);
            return response.Body;
        }

        public async Task<IReadOnlyCollection<T>> GetAll(Uri endpoint)
        {
            return await pagination.GetAllPages(async () => await GetPage(endpoint));
        }

        public async Task<T> Create(Uri endpoint, object data)
        {
            var response = await Connection.PostAsync<T>(endpoint, data);

            return response.BodyAsObject;
        }

        public async Task<T> Update(Uri endpoint, object data)
        {
            var response = await Connection.PatchAsync<T>(endpoint, data);

            return response.BodyAsObject;
        }

        public async Task Delete(Uri endpoint)
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
