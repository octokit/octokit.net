using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading;
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
        /// <param name="connection">A connection for making HTTP requests</param>
        public ApiConnection(IConnection connection) : this(connection, new ApiPagination())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiConnection"/> class.
        /// </summary>
        /// <param name="connection">A connection for making HTTP requests</param>
        /// <param name="pagination">A pagination for paging API responses</param>
        protected ApiConnection(IConnection connection, IApiPagination pagination)
        {
            Ensure.ArgumentNotNull(connection, nameof(connection));
            Ensure.ArgumentNotNull(pagination, nameof(pagination));

            Connection = connection;
            _pagination = pagination;
        }

        /// <summary>
        /// The underlying connection.
        /// </summary>
        public IConnection Connection { get; private set; }

        /// <summary>
        /// Gets the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource to get.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <returns>The API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public Task<T> Get<T>(Uri uri)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));

            return Get<T>(uri, null);
        }

        /// <summary>
        /// Gets the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource to get.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="parameters">Parameters to add to the API request</param>
        /// <returns>The API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Get<T>(Uri uri, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));

            var response = await Connection.Get<T>(uri, parameters, null).ConfigureAwait(false);
            return response.Body;
        }

        /// <summary>
        /// Gets the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource to get.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="parameters">Parameters to add to the API request</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <returns>The API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Get<T>(Uri uri, IDictionary<string, string> parameters, string accepts)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(accepts, nameof(accepts));

            var response = await Connection.Get<T>(uri, parameters, accepts).ConfigureAwait(false);
            return response.Body;
        }

        /// <summary>
        /// Gets the HTML content of the API resource at the specified URI.
        /// </summary>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="parameters">Parameters to add to the API request</param>
        /// <returns>The API resource's HTML content.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<string> GetHtml(Uri uri, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));

            var response = await Connection.GetHtml(uri, parameters).ConfigureAwait(false);
            return response.Body;
        }

        /// <summary>
        /// Gets the raw content of the API resource at the specified URI.
        /// </summary>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="parameters">Parameters to add to the API request</param>
        /// <returns>The API resource's raw content or <c>null</c> if the <paramref name="uri"/> points to a directory.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<byte[]> GetRaw(Uri uri, IDictionary<string, string> parameters)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));

            var response = await Connection.GetRaw(uri, parameters).ConfigureAwait(false);
            return response.Body;
        }

        /// <summary>
        /// Gets all API resources in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource in the list.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of the API resources in the list.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public Task<IReadOnlyList<T>> GetAll<T>(Uri uri)
        {
            return GetAll<T>(uri, ApiOptions.None);
        }

        /// <summary>
        /// Gets all API resources in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource in the list.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of the API resources in the list.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public Task<IReadOnlyList<T>> GetAll<T>(Uri uri, ApiOptions options)
        {
            return GetAll<T>(uri, null, null, options);
        }

        /// <summary>
        /// Gets all API resources in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource in the list.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="parameters">Parameters to add to the API request</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of the API resources in the list.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public Task<IReadOnlyList<T>> GetAll<T>(Uri uri, IDictionary<string, string> parameters)
        {
            return GetAll<T>(uri, parameters, null, ApiOptions.None);
        }

        /// <summary>
        /// Gets all API resources in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource in the list.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of the API resources in the list.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public Task<IReadOnlyList<T>> GetAll<T>(Uri uri, string accepts)
        {
            return GetAll<T>(uri, null, accepts, ApiOptions.None);
        }

        /// <summary>
        /// Gets all API resources in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource in the list.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="parameters">Parameters to add to the API request</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of the API resources in the list.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public Task<IReadOnlyList<T>> GetAll<T>(Uri uri, IDictionary<string, string> parameters, ApiOptions options)
        {
            return GetAll<T>(uri, parameters, null, options);
        }

        /// <summary>
        /// Gets all API resources in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource in the list.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="parameters">Parameters to add to the API request</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of the API resources in the list.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public Task<IReadOnlyList<T>> GetAll<T>(Uri uri, IDictionary<string, string> parameters, string accepts)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));

            return _pagination.GetAllPages(async () => await GetPage<T>(uri, parameters, accepts).ConfigureAwait(false), uri);
        }

        public Task<IReadOnlyList<T>> GetAll<T>(Uri uri, IDictionary<string, string> parameters, string accepts, ApiOptions options)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(options, nameof(options));

            parameters = Pagination.Setup(parameters, options);

            return _pagination.GetAllPages(async () => await GetPage<T>(uri, parameters, accepts, options).ConfigureAwait(false), uri);
        }

        /// <summary>
        /// Creates a new API resource in the list at the specified URI.
        /// </summary>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns><seealso cref="HttpStatusCode"/>Representing the received HTTP response</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public Task Post(Uri uri, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));

            return Connection.Post(uri, cancellationToken);
        }

        /// <summary>
        /// Creates a new API resource in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Post<T>(Uri uri, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));

            var response = await Connection.Post<T>(uri, cancellationToken).ConfigureAwait(false);
            return response.Body;
        }

        /// <summary>
        /// Creates a new API resource in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="data">Object that describes the new API resource; this will be serialized and used as the request's body</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public Task<T> Post<T>(Uri uri, object data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(data, nameof(data));

            return Post<T>(uri, data, null, null, cancellationToken);
        }

        /// <summary>
        /// Creates a new API resource in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="data">Object that describes the new API resource; this will be serialized and used as the request's body</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public Task<T> Post<T>(Uri uri, object data, string accepts, CancellationToken cancellationToken = default)
        {
            return Post<T>(uri, data, accepts, null, cancellationToken);
        }

        /// <summary>
        /// Creates a new API resource in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="data">Object that describes the new API resource; this will be serialized and used as the request's body</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <param name="contentType">Content type of the API request</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Post<T>(Uri uri, object data, string accepts, string contentType, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(data, nameof(data));

            var response = await Connection.Post<T>(uri, data, accepts, contentType, cancellationToken: cancellationToken).ConfigureAwait(false);
            return response.Body;
        }

        /// <summary>
        /// Creates a new API resource in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="data">Object that describes the new API resource; this will be serialized and used as the request's body</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <param name="contentType">Content type of the API request</param>
        /// <param name="twoFactorAuthenticationCode">Two Factor Authentication Code</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Post<T>(Uri uri, object data, string accepts, string contentType, string twoFactorAuthenticationCode, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(data, nameof(data));
            Ensure.ArgumentNotNull(twoFactorAuthenticationCode, nameof(twoFactorAuthenticationCode));

            var response = await Connection.Post<T>(uri, data, accepts, contentType, twoFactorAuthenticationCode, cancellationToken).ConfigureAwait(false);
            return response.Body;
        }


        public async Task<T> Post<T>(Uri uri, object data, string accepts, string contentType, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(data, nameof(data));

            var response = await Connection.Post<T>(uri, data, accepts, contentType, timeout, cancellationToken).ConfigureAwait(false);
            return response.Body;
        }

        /// <summary>
        /// Creates or replaces the API resource at the specified URI
        /// </summary>
        /// <param name="uri">URI of the API resource to put</param>
        /// <returns>A <see cref="Task"/> for the request's execution.</returns>
        public Task Put(Uri uri)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));

            return Connection.Put(uri);
        }

        /// <summary>
        /// Creates or replaces the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to create or replace</param>
        /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Put<T>(Uri uri, object data)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(data, nameof(data));

            var response = await Connection.Put<T>(uri, data).ConfigureAwait(false);

            return response.Body;
        }

        /// <summary>
        /// Creates or replaces the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to create or replace</param>
        /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body</param>
        /// <param name="twoFactorAuthenticationCode">The two-factor authentication code in response to the current user's previous challenge</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Put<T>(Uri uri, object data, string twoFactorAuthenticationCode)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(data, nameof(data));
            Ensure.ArgumentNotNullOrEmptyString(twoFactorAuthenticationCode, nameof(twoFactorAuthenticationCode));

            var response = await Connection.Put<T>(uri, data, twoFactorAuthenticationCode).ConfigureAwait(false);

            return response.Body;
        }

        /// <summary>
        /// Creates or replaces the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to create or replace</param>
        /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body</param>
        /// <param name="twoFactorAuthenticationCode">The two-factor authentication code in response to the current user's previous challenge</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Put<T>(Uri uri, object data, string twoFactorAuthenticationCode, string accepts)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(data, nameof(data));

            var response = await Connection.Put<T>(uri, data, twoFactorAuthenticationCode, accepts).ConfigureAwait(false);

            return response.Body;
        }

        /// <summary>
        /// Updates the API resource at the specified URI.
        /// </summary>
        /// <param name="uri">URI of the API resource to patch</param>
        /// <returns>A <see cref="Task"/> for the request's execution.</returns>
        public Task Patch(Uri uri)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));

            return Connection.Patch(uri);
        }

        /// <summary>
        /// Updates the API resource at the specified URI.
        /// </summary>
        /// <param name="uri">URI of the API resource to patch</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <returns>A <see cref="Task"/> for the request's execution.</returns>
        public Task Patch(Uri uri, string accepts)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(accepts, nameof(accepts));

            return Connection.Patch(uri, accepts);
        }

        /// <summary>
        /// Updates the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to update</param>
        /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body</param>
        /// <returns>The updated API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Patch<T>(Uri uri, object data)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(data, nameof(data));

            var response = await Connection.Patch<T>(uri, data).ConfigureAwait(false);

            return response.Body;
        }

        /// <summary>
        /// Updates the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to update</param>
        /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <returns>The updated API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<T> Patch<T>(Uri uri, object data, string accepts)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(data, nameof(data));
            Ensure.ArgumentNotNull(accepts, nameof(accepts));

            var response = await Connection.Patch<T>(uri, data, accepts).ConfigureAwait(false);

            return response.Body;
        }

        /// <summary>
        /// Deletes the API object at the specified URI.
        /// </summary>
        /// <param name="uri">URI of the API resource to delete</param>
        /// <returns>A <see cref="Task"/> for the request's execution.</returns>
        public Task Delete(Uri uri)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));

            return Connection.Delete(uri);
        }

        /// <summary>
        /// Deletes the API object at the specified URI.
        /// </summary>
        /// <param name="uri">URI of the API resource to delete</param>
        /// <param name="twoFactorAuthenticationCode">Two Factor Code</param>
        /// <returns>A <see cref="Task"/> for the request's execution.</returns>
        public Task Delete(Uri uri, string twoFactorAuthenticationCode)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));

            return Connection.Delete(uri, twoFactorAuthenticationCode);
        }

        /// <summary>
        /// Deletes the API object at the specified URI.
        /// </summary>
        /// <param name="uri">URI of the API resource to delete</param>
        /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body</param>
        /// <returns>A <see cref="Task"/> for the request's execution.</returns>
        public Task Delete(Uri uri, object data)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(data, nameof(data));

            return Connection.Delete(uri, data);
        }

        /// <summary>
        /// Performs an asynchronous HTTP DELETE request that expects an empty response.
        /// </summary>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="data">The object to serialize as the body of the request</param>
        /// <param name="accepts">Specifies accept response media type</param>
        /// <returns>The returned <seealso cref="HttpStatusCode"/></returns>
        public Task Delete(Uri uri, object data, string accepts)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(data, nameof(data));
            Ensure.ArgumentNotNull(accepts, nameof(accepts));

            return Connection.Delete(uri, data, accepts);
        }

        /// <summary>
        /// Performs an asynchronous HTTP DELETE request.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="data">The object to serialize as the body of the request</param>
        public async Task<T> Delete<T>(Uri uri, object data)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(data, nameof(data));

            var response = await Connection.Delete<T>(uri, data).ConfigureAwait(false);

            return response.Body;
        }

        /// <summary>
        /// Performs an asynchronous HTTP DELETE request.
        /// Attempts to map the response body to an object of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="accepts">Specifies accept response media type</param>
        /// <returns>The returned <seealso cref="HttpStatusCode"/></returns>
        public async Task<T> Delete<T>(Uri uri, string accepts)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(accepts, nameof(accepts));

            var response = await Connection.Delete<T>(uri, null, accepts).ConfigureAwait(false);

            return response.Body;
        }

        /// <summary>
        /// Performs an asynchronous HTTP DELETE request.
        /// Attempts to map the response body to an object of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type to map the response to</typeparam>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="data">The object to serialize as the body of the request</param>
        /// <param name="accepts">Specifies accept response media type</param>
        public async Task<T> Delete<T>(Uri uri, object data, string accepts)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));
            Ensure.ArgumentNotNull(data, nameof(data));
            Ensure.ArgumentNotNull(accepts, nameof(accepts));

            var response = await Connection.Delete<T>(uri, data, accepts).ConfigureAwait(false);

            return response.Body;
        }

        /// <summary>
        /// Executes a GET to the API object at the specified URI. This operation is appropriate for API calls which 
        /// queue long running calculations and return a collection of a resource.
        /// It expects the API to respond with an initial 202 Accepted, and queries again until a 200 OK is received.
        /// It returns an empty collection if it receives a 204 No Content response.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to update</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        /// <returns>The updated API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public async Task<IReadOnlyList<T>> GetQueuedOperation<T>(Uri uri, CancellationToken cancellationToken)
        {
            while (true)
            {
                Ensure.ArgumentNotNull(uri, nameof(uri));

                var response = await Connection.GetResponse<IReadOnlyList<T>>(uri, cancellationToken).ConfigureAwait(false);

                switch (response.HttpResponse.StatusCode)
                {
                    case HttpStatusCode.Accepted:
                        continue;
                    case HttpStatusCode.NoContent:
                        return new ReadOnlyCollection<T>(new T[] { });
                    case HttpStatusCode.OK:
                        return response.Body;
                }

                throw new ApiException("Queued Operations expect status codes of Accepted, No Content, or OK.",
                    response.HttpResponse.StatusCode);
            }
        }

        async Task<IReadOnlyPagedCollection<T>> GetPage<T>(
            Uri uri,
            IDictionary<string, string> parameters,
            string accepts)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));

            var response = await Connection.Get<List<T>>(uri, parameters, accepts).ConfigureAwait(false);
            return new ReadOnlyPagedCollection<T>(
                response,
                nextPageUri => Connection.Get<List<T>>(nextPageUri, parameters, accepts));
        }

        async Task<IReadOnlyPagedCollection<TU>> GetPage<TU>(
            Uri uri,
            IDictionary<string, string> parameters,
            string accepts,
            ApiOptions options)
        {
            Ensure.ArgumentNotNull(uri, nameof(uri));

            var connection = Connection;

            var response = await connection.Get<List<TU>>(uri, parameters, accepts).ConfigureAwait(false);
            return new ReadOnlyPagedCollection<TU>(
                response,
                nextPageUri =>
                {
                    var shouldContinue = Pagination.ShouldContinue(
                        nextPageUri,
                        options);

                    return shouldContinue
                        ? connection.Get<List<TU>>(nextPageUri, parameters, accepts)
                        : null;
                });
        }
    }
}
