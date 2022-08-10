using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace Octokit
{
    /// <summary>
    /// Represents a HTTP 451 - Unavailable For Legal Reasons response returned from the API.
    /// This will returned if GitHub has been asked to takedown the requested resource due to
    /// a DMCA takedown.
    /// </summary>
    [Serializable]
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class LegalRestrictionException : ApiException
    {
        public override string Message
        {
            get { return ApiErrorMessageSafe ?? "Resource taken down due to a DMCA notice."; }
        }

        /// <summary>
        /// Constructs an instance of LegalRestrictionException
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        public LegalRestrictionException(IResponse response) : this(response, null)
        {
        }

        /// <summary>
        /// Constructs an instance of LegalRestrictionException
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="statusCode">The http status code returned by the response</param>
        public LegalRestrictionException(string message, HttpStatusCode statusCode) : base(message, statusCode)
        {
        }

        /// <summary>
        /// Constructs an instance of LegalRestrictionException
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        /// <param name="innerException">The inner exception</param>
        public LegalRestrictionException(IResponse response, Exception innerException)
            : base(response, innerException)
        {
            Debug.Assert(response != null && response.StatusCode == (HttpStatusCode)451,
                "LegalRestrictionException created with wrong status code");
        }

        /// <summary>
        /// Constructs an instance of LegalRestrictionException
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected LegalRestrictionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
