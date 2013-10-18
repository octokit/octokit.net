using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Octokit
{
#if !NETFX_CORE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class LoginAttemptsExceededException : ForbiddenException
    {
        public LoginAttemptsExceededException(IResponse response) : base(response)
        {
        }

        public LoginAttemptsExceededException(IResponse response, Exception innerException)
            : base(response, innerException)
        {
        }

        public override string Message
        {
            get { return ApiErrorMessageSafe ?? "Maximum number of login attempts exceeded"; }
        }

#if !NETFX_CORE
        protected LoginAttemptsExceededException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif

    }
}
