using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Octokit
{
    /// <summary>
    /// Exception thrown when Secondary GitHub API Rate limits are exceeded.
    /// </summary>
    /// <summary>
    /// <para>
    /// This occurs when GitHub perceives misuse of the API. You may get this if
    /// you are polling heavily, creating content rapidly or making concurrent requests.
    /// </para>
    /// <para>See https://docs.github.com/rest/overview/resources-in-the-rest-api#secondary-rate-limits for more details.</para>
    /// </summary>
    [Serializable]
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class SecondaryRateLimitExceededException : ForbiddenException
    {
        /// <summary>
        /// Constructs an instance of the <see cref="Octokit.SecondaryRateLimitExceededException"/> class.
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        public SecondaryRateLimitExceededException(IResponse response) : this(response, null)
        {
        }

        /// <summary>
        /// Constructs an instance of the <see cref="Octokit.SecondaryRateLimitExceededException"/> class.
        /// </summary>
        /// <param name="response">The HTTP payload from the server</param>
        /// <param name="innerException">The inner exception</param>
        public SecondaryRateLimitExceededException(IResponse response, Exception innerException) : base(response, innerException)
        {
            Ensure.ArgumentNotNull(response, nameof(response));
        }

        public override string Message
        {
            get { return ApiErrorMessageSafe ?? "Secondary API Rate Limit exceeded"; }
        }

        /// <summary>
        /// Constructs an instance of <see  cref="Octokit.SecondaryRateLimitExceededException"/>.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected SecondaryRateLimitExceededException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
