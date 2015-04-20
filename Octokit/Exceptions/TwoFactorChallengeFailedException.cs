using System;
using System.Diagnostics.CodeAnalysis;
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
    public class TwoFactorChallengeFailedException : TwoFactorAuthorizationException
    {
        /// <summary>
        /// Constructs an instance of TwoFactorChallengeFailedException
        /// </summary>
        public TwoFactorChallengeFailedException() : base(TwoFactorType.None, null)
        {
        }

        /// <summary>
        /// Constructs an instance of TwoFactorChallengeFailedException
        /// </summary>
        /// <param name="authorizationCode">The authorization code that was incorrect</param>
        /// <param name="innerException">The inner exception</param>
        public TwoFactorChallengeFailedException(
            string authorizationCode,
            ApiException innerException)
            : base(ParseTwoFactorType(innerException), innerException)
        {
            AuthorizationCode = authorizationCode;
        }

        public override string Message
        {
            get { return "The two-factor authentication code supplied is not correct"; }
        }

        public string AuthorizationCode { get; private set; }

        static TwoFactorType ParseTwoFactorType(ApiException exception)
        {
            return exception == null ? TwoFactorType.None : Connection.ParseTwoFactorType(exception.HttpResponse);
        }

#if !NETFX_CORE
        /// <summary>
        /// Constructs an instance of TwoFactorChallengeFailedException.
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
            if (info == null) return;
            AuthorizationCode = info.GetString("AuthorizationCode");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("AuthorizationCode", AuthorizationCode);
        }
#endif
    }
}
