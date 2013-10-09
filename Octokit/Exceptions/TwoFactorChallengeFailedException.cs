using System;
using System.Runtime.Serialization;

namespace Octokit
{
#if !NETFX_CORE
    [Serializable]
#endif
    public class TwoFactorChallengeFailedException : AuthorizationException
    {
        public TwoFactorChallengeFailedException()
        {
        }

        public TwoFactorChallengeFailedException(string message) : base(message)
        {
        }

        public TwoFactorChallengeFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

#if !NETFX_CORE
        protected TwoFactorChallengeFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
