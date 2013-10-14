using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
{
    public class ApiConnection : IApiConnection
    {
        readonly IApiPagination _pagination;

        public ApiConnection(IConnection connection) : this(connection, new ApiPagination())
        {
        }

        protected ApiConnection(IConnection connection, IApiPagination pagination)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(pagination, "pagination");

            Connection = connection;
            _pagination = pagination;
        }

        protected IConnection Connection { get; private set; }

        public async Task<TOther> Get<TOther>(Uri endpoint, IDictionary<string, string> parameters)
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

        public async Task<IReadOnlyList<T>> GetAll<T>(Uri endpoint, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            return await _pagination.GetAllPages(async () => await GetPage<T>(endpoint, parameters));
        }

        public async Task<T> Create<T>(Uri endpoint, object data)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");
            Ensure.ArgumentNotNull(data, "data");

            var response = await Connection.PostAsync<T>(endpoint, data);

            return response.BodyAsObject;
        }

        public async Task<T> GetOrCreate<T>(Uri endpoint, object data)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");
            Ensure.ArgumentNotNull(data, "data");

            var response = await Connection.PutAsync<T>(endpoint, data);

            return response.BodyAsObject;
        }

        public async Task<T> GetOrCreate<T>(Uri endpoint, object data, string twoFactorAuthenticationCode)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");
            Ensure.ArgumentNotNull(data, "data");
            Ensure.ArgumentNotNullOrEmptyString(twoFactorAuthenticationCode, "twoFactorAuthenticationCode");
            
            var response = await Connection.PutAsync<T>(endpoint, data, twoFactorAuthenticationCode);

            return response.BodyAsObject;
        }

        public async Task<T> Update<T>(Uri endpoint, object data)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");
            Ensure.ArgumentNotNull(data, "data");

            var response = await Connection.PatchAsync<T>(endpoint, data);

            return response.BodyAsObject;
        }

        public async Task<T> Put<T>(Uri endpoint, object data)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");
            Ensure.ArgumentNotNull(data, "data");

            var response = await Connection.PostAsync<T>(endpoint, data);

            return response.BodyAsObject;
        }

        public async Task Delete<T>(Uri endpoint)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            await Connection.DeleteAsync<T>(endpoint);
        }

        public async Task<TOther> Upload<TOther>(Uri uri, Stream rawData, string contentType)
        {
            Ensure.ArgumentNotNull(uri, "uri");
            Ensure.ArgumentNotNull(rawData, "rawData");
            Ensure.ArgumentNotNull(contentType, "contentType");

            var response = await Connection.PostAsync<TOther>(
                uri,
                rawData,
                contentType,
                "application/vnd.github.manifold-preview");
            return response.BodyAsObject;
        }

        async Task<IReadOnlyPagedCollection<T>> GetPage<T>(Uri endpoint, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(endpoint, "endpoint");

            var response = await Connection.GetAsync<List<T>>(endpoint, parameters);
            return new ReadOnlyPagedCollection<T>(response, Connection);
        }
    }
}
