using System;
using System.Threading;
#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Extensions for working with the <see cref="IApiConnection"/>
    /// </summary>
    public static class ApiExtensions
    {
        /// <summary>
        /// Gets the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource to get.</typeparam>
        /// <param name="connection">The connection to use</param>
        /// <param name="uri">URI of the API resource to get</param>
        /// <returns>The API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public static Task<T> Get<T>(this IApiConnection connection, Uri uri)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(uri, "uri");

            return connection.Get<T>(uri, null);
        }

        /// <summary>
        /// Gets all API resources in the list at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource in the list.</typeparam>
        /// <param name="connection">The connection to use</param>
        /// <param name="uri">URI of the API resource to get</param>
        /// <returns><see cref="IReadOnlyList{T}"/> of the The API resources in the list.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public static Task<IReadOnlyList<T>> GetAll<T>(this IApiConnection connection, Uri uri)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(uri, "uri");

            return connection.GetAll<T>(uri, null);
        }

        /// <summary>
        /// Gets the HTML content of the API resource at the specified URI.
        /// </summary>
        /// <param name="connection">The connection to use</param>
        /// <param name="uri">URI of the API resource to get</param>
        /// <returns>The API resource's HTML content.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public static Task<string> GetHtml(this IApiConnection connection, Uri uri)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(uri, "uri");

            return connection.GetHtml(uri, null);
        }

        /// <summary>
        /// Performs an asynchronous HTTP GET request that expects a <seealso cref="IResponse"/> containing HTML.
        /// </summary>
        /// <param name="connection">The connection to use</param>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <returns><seealso cref="IResponse"/> representing the received HTTP response</returns>
        public static Task<IResponse<string>> GetHtml(this IConnection connection, Uri uri)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(uri, "uri");
            
            return connection.GetHtml(uri, null);
        }

        /// <summary>
        /// Gets the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource to get.</typeparam>
        /// <param name="connection">The connection to use</param>
        /// <param name="uri">URI of the API resource to get</param>
        /// <returns>The API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public static Task<IResponse<T>> GetResponse<T>(this IConnection connection, Uri uri)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(uri, "uri");

            return connection.Get<T>(uri, null, null);
        }

        /// <summary>
        /// Gets the API resource at the specified URI.
        /// </summary>
        /// <typeparam name="T">Type of the API resource to get.</typeparam>
        /// <param name="connection">The connection to use</param>
        /// <param name="uri">URI of the API resource to get</param>
        /// <param name="cancellationToken">A token used to cancel the GetResponse request</param>
        /// <returns>The API resource.</returns>
        /// <exception cref="ApiException">Thrown when an API error occurs.</exception>
        public static Task<IResponse<T>> GetResponse<T>(this IConnection connection, Uri uri, CancellationToken cancellationToken)
        {
            Ensure.ArgumentNotNull(connection, "connection");
            Ensure.ArgumentNotNull(uri, "uri");

            return connection.Get<T>(uri, null, null, cancellationToken);
        }
    }
}
