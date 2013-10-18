using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;
using Octokit.Internal;

namespace Octokit
{
#if !NETFX_CORE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class TwoFactorChallengeFailedException : AuthorizationException
    {
        public TwoFactorChallengeFailedException() :
            base(new ApiResponse<object> { StatusCode = HttpStatusCode.Unauthorized })
        {
        }

        public TwoFactorChallengeFailedException(Exception innerException)
            : base(new ApiResponse<object> { StatusCode = HttpStatusCode.Unauthorized }, innerException)
        {
        }

        public override string Message
        {
            get { return "The two-factor authentication code supplied is not correct"; }
        }

#if !NETFX_CORE
        protected TwoFactorChallengeFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
