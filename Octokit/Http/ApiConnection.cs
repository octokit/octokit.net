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

        public async Task<T> Get(Uri endpoint, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            return await GetItem<T>(endpoint, parameters);
        }

        public async Task<TOther> GetItem<TOther>(Uri endpoint, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            var response = await Connection.GetAsync<TOther>(endpoint, parameters);
            return response.BodyAsObject;
        }

        public async Task<string> GetHtml(Uri endpoint, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            var response = await Connection.GetHtml(endpoint, parameters);
            return response.Body;
        }

        public async Task<IReadOnlyCollection<T>> GetAll(Uri endpoint, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            return await pagination.GetAllPages(async () => await GetPage(endpoint, parameters));
        }

        public async Task<IReadOnlyCollection<TOther>> GetAll<TOther>(Uri endpoint, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            return await pagination.GetAllPages<TOther>(async () => await GetPage<TOther>(endpoint, parameters));
        }

        public async Task<T> Create(Uri endpoint, object data)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");
            Ensure.ArgumentNotNull(data, "data");

            var response = await Connection.PostAsync<T>(endpoint, data);

            return response.BodyAsObject;
        }

        public async Task<T> Update(Uri endpoint, object data)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");
            Ensure.ArgumentNotNull(data, "data");

            var response = await Connection.PatchAsync<T>(endpoint, data);

            return response.BodyAsObject;
        }

        public async Task<T> Put(Uri endpoint, object data)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");
            Ensure.ArgumentNotNull(data, "data");

            var response = await Connection.PostAsync<T>(endpoint, data);

            return response.BodyAsObject;
        }

        public async Task Delete(Uri endpoint)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");
            
            await Connection.DeleteAsync<T>(endpoint);
        }

        async Task<IReadOnlyPagedCollection<T>> GetPage(Uri endpoint, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            var response = await Connection.GetAsync<List<T>>(endpoint, parameters);
            return new ReadOnlyPagedCollection<T>(response, Connection);
        }

        async Task<IReadOnlyPagedCollection<TOther>> GetPage<TOther>(Uri endpoint, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            var response = await Connection.GetAsync<List<TOther>>(endpoint, parameters);
            return new ReadOnlyPagedCollection<TOther>(response, Connection);
        }

    }
}
