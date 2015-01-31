using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace Octokit
{
#if !NETFX_CORE
    /// <summary>
    /// Represents a failed 2FA challenge from the API
    /// </summary>
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class TwoFactorChallengeFailedException : AuthorizationException
    {
        /// <summary>
        /// Constructs an instance of TwoFactorChallengeFailedException
        /// </summary>
        public TwoFactorChallengeFailedException() :
            base(HttpStatusCode.Unauthorized, null)
        {
        }

        /// <summary>
        /// Constructs an instance of TwoFactorChallengeFailedException
        /// </summary>
        /// <param name="innerException">The inner exception</param>
        public TwoFactorChallengeFailedException(Exception innerException)
            : base(HttpStatusCode.Unauthorized, innerException)
        {
        }

        public override string Message
        {
            get { return "The two-factor authentication code supplied is not correct"; }
        }

#if !NETFX_CORE
        /// <summary>
        /// Constructs an instance of TwoFactorChallengeFailedException
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected TwoFactorChallengeFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
