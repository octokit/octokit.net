using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// A connection for making HTTP requests against URI endpoints.
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// Performs an asynchronous HTTP GET request that expects a <seealso cref="IResponse"/> containing HTML.
        /// </summary>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="parameters">Querystring parameters for the request</param>
        /// <returns><seealso cref="IResponse"/> representing the received HTTP response</returns>
        Task<IResponse<string>> GetHtml(Uri uri, IDictionary<string, string> parameters);

        /// <summary>
        /// Performs an asynchronous HTTP GET request.
        /// Attempts to map the response to an object of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type to map the response to</typeparam>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="parameters">Querystring parameters for the request</param>
        /// <param name="accepts">Specifies accepted response media types.</param>
        /// <returns><seealso cref="IResponse"/> representing the received HTTP response</returns>
        Task<IResponse<T>> GetAsync<T>(Uri uri, IDictionary<string, string> parameters, string accepts);

        /// <summary>
        /// Performs an asynchronous HTTP GET request.
        /// Attempts to map the response to an object of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type to map the response to</typeparam>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="parameters">Querystring parameters for the request</param>
        /// <param name="accepts">Specifies accepted response media types.</param>
        /// <param name="cancellationToken">A token used to cancel the Get request</param>
        /// <returns><seealso cref="IResponse"/> representing the received HTTP response</returns>
        Task<IResponse<T>> GetAsync<T>(Uri uri, IDictionary<string, string> parameters, string accepts, CancellationToken cancellationToken);

        /// <summary>
        /// Performs an asynchronous HTTP PATCH request.
        /// Attempts to map the response body to an object of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type to map the response to</typeparam>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="body">The object to serialize as the body of the request</param>
        /// <returns><seealso cref="IResponse"/> representing the received HTTP response</returns>
        Task<IResponse<T>> PatchAsync<T>(Uri uri, object body);

        /// <summary>
        /// Performs an asynchronous HTTP PATCH request.
        /// Attempts to map the response body to an object of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type to map the response to</typeparam>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="body">The object to serialize as the body of the request</param>
        /// <param name="accepts">Specifies accepted response media types.</param>
        /// <returns><seealso cref="IResponse"/> representing the received HTTP response</returns>
        Task<IResponse<T>> PatchAsync<T>(Uri uri, object body, string accepts);

        /// <summary>
        /// Performs an asynchronous HTTP POST request.
        /// Attempts to map the response body to an object of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type to map the response to</typeparam>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="body">The object to serialize as the body of the request</param>
        /// <param name="accepts">Specifies accepted response media types.</param>
        /// <param name="contentType">Specifies the media type of the request body</param>
        /// <returns><seealso cref="IResponse"/> representing the received HTTP response</returns>
        Task<IResponse<T>> PostAsync<T>(Uri uri, object body, string accepts, string contentType);

        /// <summary>
        /// Performs an asynchronous HTTP PUT request.
        /// Attempts to map the response body to an object of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type to map the response to</typeparam>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="body">The body of the request</param>
        /// <returns><seealso cref="IResponse"/> representing the received HTTP response</returns>
        Task<IResponse<T>> PutAsync<T>(Uri uri, object body);

        /// <summary>
        /// Performs an asynchronous HTTP PUT request using the provided two factor authentication code.
        /// Attempts to map the response body to an object of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type to map the response to</typeparam>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <param name="body">The object to serialize as the body of the request</param>
        /// <param name="twoFactorAuthenticationCode">Two factory authentication code to use</param>
        /// <returns><seealso cref="IResponse"/> representing the received HTTP response</returns>
        Task<IResponse<T>> PutAsync<T>(Uri uri, object body, string twoFactorAuthenticationCode);

        /// <summary>
        /// Performs an asynchronous HTTP PUT request that expects an empty response.
        /// </summary>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <returns>The returned <seealso cref="HttpStatusCode"/></returns>
        Task<HttpStatusCode> PutAsync(Uri uri);

        /// <summary>
        /// Performs an asynchronous HTTP DELETE request that expects an empty response.
        /// </summary>
        /// <param name="uri">URI endpoint to send request to</param>
        /// <returns>The returned <seealso cref="HttpStatusCode"/></returns>
        Task<HttpStatusCode> DeleteAsync(Uri uri);

        /// <summary>
        /// Base address for the connection.
        /// </summary>
        Uri BaseAddress { get; }

        /// <summary>
        /// Gets the <seealso cref="ICredentialStore"/> used to provide credentials for the connection.
        /// </summary>
        ICredentialStore CredentialStore { get; }

        /// <summary>
        /// Gets or sets the credentials used by the connection.
        /// </summary>
        /// <remarks>
        /// You can use this property if you only have a single hard-coded credential. Otherwise, pass in an 
        /// <see cref="ICredentialStore"/> to the constructor. 
        /// Setting this property will change the <see cref="ICredentialStore"/> to use 
        /// the default <see cref="InMemoryCredentialStore"/> with just these credentials.
        /// </remarks>
        Credentials Credentials { get; set; }
    }
}
