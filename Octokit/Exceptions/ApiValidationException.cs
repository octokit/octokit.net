using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace Octokit
{
    /// <summary>
    /// Represents a HTTP 422 - Unprocessable Entity response returned from the API.
    /// </summary>
#if !NETFX_CORE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class ApiValidationException : ApiException
    {
        /// <summary>
        /// Constructs an instance of ApiValidationException
        /// </summary>
        public ApiValidationException() : base((HttpStatusCode)422, null)
        {
        }

        /// <summary>
        /// Constructs an instance of ApiValidationException
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        public ApiValidationException(IResponse response)
            : this(response, null)
        {
        }

        /// <summary>
        /// Constructs an instance of ApiValidationException
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        /// <param name="innerException">The inner exception</param>
        public ApiValidationException(IResponse response, Exception innerException)
            : base(response, innerException)
        {
            Debug.Assert(response != null && response.StatusCode == (HttpStatusCode)422,
                "ApiValidationException created with wrong status code");
        }

        /// <summary>
        /// Constructs an instance of ApiValidationException
        /// </summary>
        /// <param name="innerException">The inner exception</param>
        protected ApiValidationException(ApiException innerException)
            : base(innerException)
        {
        }

        public override string Message
        {
            get { return ApiErrorMessageSafe ?? "Validation Failed"; }
        }

#if !NETFX_CORE
        /// <summary>
        /// Constructs an instance of ApiValidationException
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected ApiValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
