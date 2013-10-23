using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// A connection for making API requests against URI endpoints.
    /// Provides type-friendly convenience methods that wrap <see cref="IConnection"/> methods.
    /// </summary>
    public class ApiConnection : IApiConnection
    {
        readonly IApiPagination _pagination;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiConnection"/> class.
        /// </summary>
        /// <param name="connection">A connection for making HTTP requests.</param>
        public ApiConnection(IConnection connection) : this(connection, new ApiPagination())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiConnection"/> class.
        /// </summary>
        /// <param name="connection">A connection for making HTTP requests.</param>
        /// <param name="pagination">A paginator for paging API responses.</param>
        protected ApiConnection(IConnection connection, IApiPagination pagination)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(pagination, "pagination");

            Connection = connection;
            _pagination = pagination;
        }

        /// <summary>
        /// Gets the connection for making HTTP requests.
        /// </summary>
        public IConnection Connection { get; private set; }

        /// <summary>
        /// Gets the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource to get.</typeparam>
        /// <param name="uri">URI of the API resource to get.</param>
        /// <param name="parameters">Parameters to add to the API request.</param>
        /// <returns>The API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Get<T>(Uri uri, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            var response = await Connection.GetAsync<T>(uri, parameters, null);
            return response.BodyAsObject;
        }

        /// <summary>
        /// Gets the HTML content of the API resource at the specified URI.
        /// </summary>
        /// <param name="uri">URI of the API resource to get.</param>
        /// <param name="parameters">Parameters to add to the API request.</param>
        /// <returns>The API resource's HTML content.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<string> GetHtml(Uri uri, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            var response = await Connection.GetHtml(uri, parameters);
            return response.Body;
        }

        /// <summary>
        /// Gets all API resources in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource in the list.</typeparam>
        /// <param name="uri">URI of the API resource to get.</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of the The API resources in the list.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<IReadOnlyList<T>> GetAll<T>(Uri uri)
        {
            return await GetAll<T>(uri, null, null);
        }

        /// <summary>
        /// Gets all API resources in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource in the list.</typeparam>
        /// <param name="uri">URI of the API resource to get.</param>
        /// <param name="parameters">Parameters to add to the API request.</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of the The API resources in the list.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<IReadOnlyList<T>> GetAll<T>(Uri uri, IDictionary<string, string> parameters)
        {
            return await GetAll<T>(uri, parameters, null);
        }

        /// <summary>
        /// Gets all API resources in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource in the list.</typeparam>
        /// <param name="uri">URI of the API resource to get.</param>
        /// <param name="parameters">Parameters to add to the API request.</param>
        /// <param name="accepts">Accept header to use for the API request.</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of the The API resources in the list.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<IReadOnlyList<T>> GetAll<T>(Uri uri, IDictionary<string, string> parameters, string accepts)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            return await _pagination.GetAllPages(async () => await GetPage<T>(uri, parameters, accepts));
        }

        /// <summary>
        /// Creates a new API resource in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to get.</param>
        /// <param name="data">Object that describes the new API resource; this will be serialized and used as the request's body.</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Post<T>(Uri uri, object data)
        {
            Ensure.ArgumentNotNull(uri, "uri");
            Ensure.ArgumentNotNull(data, "data");

            return await Post<T>(uri, data, null, null);
        }

        /// <summary>
        /// Creates a new API resource in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to get.</param>
        /// <param name="data">Object that describes the new API resource; this will be serialized and used as the request's body.</param>
        /// <param name="accepts">Accept header to use for the API request.</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Post<T>(Uri uri, object data, string accepts)
        {
            return await Post<T>(uri, data, accepts, null);
        }

        /// <summary>
        /// Creates a new API resource in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to get.</param>
        /// <param name="data">Object that describes the new API resource; this will be serialized and used as the request's body.</param>
        /// <param name="accepts">Accept header to use for the API request.</param>
        /// <param name="contentType">Content type of the API request.</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Post<T>(Uri uri, object data, string accepts, string contentType)
        {
            Ensure.ArgumentNotNull(uri, "uri");
            Ensure.ArgumentNotNull(data, "data");

            var response = await Connection.PostAsync<T>(
                uri,
                data,
                accepts,
                contentType);
            return response.BodyAsObject;
        }

        /// <summary>
        /// Creates or replaces the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to create or replace.</param>
        /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body.</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Put<T>(Uri uri, object data)
        {
            Ensure.ArgumentNotNull(uri, "uri");
            Ensure.ArgumentNotNull(data, "data");

            var response = await Connection.PutAsync<T>(uri, data);

            return response.BodyAsObject;
        }

        /// <summary>
        /// Creates or replaces the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to create or replace.</param>
        /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body.</param>
        /// <param name="twoFactorAuthenticationCode">The two-factor authentication code in response to the current user's previous challenge.</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Put<T>(Uri uri, object data, string twoFactorAuthenticationCode)
        {
            Ensure.ArgumentNotNull(uri, "uri");
            Ensure.ArgumentNotNull(data, "data");
            Ensure.ArgumentNotNullOrEmptyString(twoFactorAuthenticationCode, "twoFactorAuthenticationCode");
            
            var response = await Connection.PutAsync<T>(uri, data, twoFactorAuthenticationCode);

            return response.BodyAsObject;
        }

        /// <summary>
        /// Updates the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to update.</param>
        /// /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body.</param>
        /// <returns>The updated API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Patch<T>(Uri uri, object data)
        {
            Ensure.ArgumentNotNull(uri, "uri");
            Ensure.ArgumentNotNull(data, "data");

            var response = await Connection.PatchAsync<T>(uri, data);

            return response.BodyAsObject;
        }

        /// <summary>
        /// Deletes the API object at the specified URI.
        /// </summary>
        /// <param name="uri">URI of the API resource to delete.</param>
        /// <returns>A <see cref="Task"/> for the request's execution.</returns>
        public async Task Delete(Uri uri)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            await Connection.DeleteAsync(uri);
        }

        async Task<IReadOnlyPagedCollection<T>> GetPage<T>(
            Uri uri,
            IDictionary<string, string> parameters,
            string accepts)
        {
            Ensure.ArgumentNotNull(uri, "uri");

            var response = await Connection.GetAsync<List<T>>(uri, parameters, accepts);
            return new ReadOnlyPagedCollection<T>(
                response,
                nextPageUri => Connection.GetAsync<List<T>>(nextPageUri, parameters, accepts));
        }
    }
}
