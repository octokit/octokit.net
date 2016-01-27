using System.Collections.Generic;
using System.Net;

namespace Octokit
{
    /// <summary>
    /// A response from an API call that includes the deserialized object instance.
    /// </summary>
    public interface IApiResponse<out T>
    {
        /// <summary>
        /// Object deserialized from the JSON response body.
        /// </summary>
        T Body { get; }

        /// <summary>
        /// The original non-deserialized http response.
        /// </summary>
        IResponse HttpResponse { get; }
    }

    /// <summary>
    /// Represents a generic HTTP response
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// Raw response body. Typically a string, but when requesting images, it will be a byte array.
        /// </summary>
        object Body { get; }

        /// <summary>
        /// Information about the API.
        /// </summary>
        IReadOnlyDictionary<string, string> Headers { get; }

        /// <summary>
        /// Information about the API response parsed from the response headers.
        /// </summary>
        ApiInfo ApiInfo { get; }

        /// <summary>
        /// The response status code.
        /// </summary>
        HttpStatusCode StatusCode { get; }

        /// <summary>
        /// The content type of the response.
        /// </summary>
        string ContentType { get; }
    }
}
