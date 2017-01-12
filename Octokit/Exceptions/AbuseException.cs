using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
#if !NO_SERIALIZABLE
using System.Runtime.Serialization;
using System.Security;
#endif

namespace Octokit
{
    /// <summary>
    /// Represents a subset of the HTTP 403 - Forbidden response returned from the API when the forbidden response is related to an abuse detection mechanism.
    /// Containts the amount of seconds after which it's safe to retry the request.
    /// </summary>
#if !NO_SERIALIZABLE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class AbuseException : ForbiddenException
    {
        /// <summary>
        /// Constructs an instance of AbuseException
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        public AbuseException(IResponse response) : this(response, null)
        {
        }

        /// <summary>
        /// Constructs an instance of AbuseException
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        /// <param name="innerException">The inner exception</param>
        public AbuseException(IResponse response, Exception innerException)
            : base(response, innerException)
        {
            Debug.Assert(response != null && response.StatusCode == HttpStatusCode.Forbidden,
                "AbuseException created with wrong status code");

            RetryAfterSeconds = ParseRetryAfterSeconds(response);
        }

        private static int? ParseRetryAfterSeconds(IResponse response)
        {
            string secondsValue;
            if (!response.Headers.TryGetValue("Retry-After", out secondsValue)) { return null; }

            int retrySeconds;
            if (!int.TryParse(secondsValue, out retrySeconds)) { return null; }
            if (retrySeconds < 0) { return null; }

            return retrySeconds;
        }

        public int? RetryAfterSeconds { get; private set; }

        public override string Message
        {
            get { return ApiErrorMessageSafe ?? "Request Forbidden - Abuse Detection"; }
        }

#if !NO_SERIALIZABLE
        /// <summary>
        /// Constructs an instance of AbuseException
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected AbuseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("RetryAfterSeconds", RetryAfterSeconds);
        }
#endif
    }
}
