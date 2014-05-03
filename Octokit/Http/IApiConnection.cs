using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A connection for making API requests against URI endpoints.
    /// Provides type-friendly convenience methods that wrap <see cref="IConnection"/> methods.
    /// </summary>
    public interface IApiConnection
    {
        /// <summary>
        /// The underlying connection.
        /// </summary>
        IConnection Connection { get; }

        /// <summary>
        /// Gets the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource to get.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="parameters">Parameters to add to the API request</param>
        /// <returns>The API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "It's fiiiine. It's fine. Trust us.")]
        Task<T> Get<T>(Uri uri, IDictionary<string, string> parameters);

        /// <summary>
        /// Gets the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource to get.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="parameters">Parameters to add to the API request</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <returns>The API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "It's fiiiine. It's fine. Trust us.")]
        Task<T> Get<T>(Uri uri, IDictionary<string, string> parameters, string accepts);

        /// <summary>
        /// Gets the HTML content of the API resource at the specified URI.
        /// </summary>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="parameters">Parameters to add to the API request</param>
        /// <returns>The API resource's HTML content.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        Task<string> GetHtml(Uri uri, IDictionary<string, string> parameters);

        /// <summary>
        /// Gets all API resources in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource in the list.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of the The API resources in the list.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        Task<IReadOnlyList<T>> GetAll<T>(Uri uri);
        
        /// <summary>
        /// Gets all API resources in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource in the list.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="parameters">Parameters to add to the API request</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of the The API resources in the list.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        Task<IReadOnlyList<T>> GetAll<T>(Uri uri, IDictionary<string, string> parameters);

        /// <summary>
        /// Gets all API resources in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource in the list.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="parameters">Parameters to add to the API request</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of the The API resources in the list.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        Task<IReadOnlyList<T>> GetAll<T>(Uri uri, IDictionary<string, string> parameters, string accepts);

        /// <summary>
        /// Creates a new API resource in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="data">Object that describes the new API resource; this will be serialized and used as the request's body</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        Task<T> Post<T>(Uri uri, object data);

        /// <summary>
        /// Creates a new API resource in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="data">Object that describes the new API resource; this will be serialized and used as the request's body</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        Task<T> Post<T>(Uri uri, object data, string accepts); 

        /// <summary>
        /// Creates a new API resource in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="data">Object that describes the new API resource; this will be serialized and used as the request's body</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <param name="contentType">Content type of the API request</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        Task<T> Post<T>(Uri uri, object data, string accepts, string contentType);

        /// <summary>
        /// Creates or replaces the API resource at the specified URI
        /// </summary>
        /// <param name="uri">URI of the API resource to put</param>
        /// <returns>A <see cref="Task"/> for the request's execution.</returns>
        Task Put(Uri uri);

        /// <summary>
        /// Creates or replaces the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to create or replace</param>
        /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        Task<T> Put<T>(Uri uri, object data);

        /// <summary>
        /// Creates or replaces the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to create or replace</param>
        /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body</param>
        /// <param name="twoFactorAuthenticationCode">The two-factor authentication code in response to the current user's previous challenge</param>
        /// <returns>The created API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        Task<T> Put<T>(Uri uri, object data, string twoFactorAuthenticationCode);

        /// <summary>
        /// Updates the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to update</param>
        /// /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body</param>
        /// <returns>The updated API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        Task<T> Patch<T>(Uri uri, object data);

        /// <summary>
        /// Updates the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to update</param>
        /// <param name="data">Object that describes the API resource; this will be serialized and used as the request's body</param>
        /// <param name="accepts">Accept header to use for the API request</param>
        /// <returns>The updated API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        Task<T> Patch<T>(Uri uri, object data, string accepts);

        /// <summary>
        /// Deletes the API object at the specified URI.
        /// </summary>
        /// <param name="uri">URI of the API resource to delete</param>
        /// <returns>A <see cref="Task"/> for the request's execution.</returns>
        Task Delete(Uri uri);

        /// <summary>
        /// Executes a GET to the API object at the specified URI. This operation is appropriate for
        /// API calls which queue long running calculations.
        /// It expects the API to respond with an initial 202 Accepted, and queries again until a 
        /// 200 OK is received.
        /// </summary>
        /// <typeparam name="T">The API resource's type.</typeparam>
        /// <param name="uri">URI of the API resource to update</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        /// <returns>The updated API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        Task<T> GetQueuedOperation<T>(Uri uri,CancellationToken cancellationToken);
    }
}