using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;

namespace Octokit
{
#if !NETFX_CORE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class ForbiddenException : ApiException
    {
        public ForbiddenException(IResponse response) : this(response, null)
        {
        }

        public ForbiddenException(IResponse response, Exception innerException)
            : base(response, innerException)
        {
            Debug.Assert(response != null && response.StatusCode == HttpStatusCode.Forbidden,
                "ForbiddenException created with wrong status code");
        }

        public override string Message
        {
            get { return ApiErrorMessageSafe ?? "Request Forbidden"; }
        }
    
#if !NETFX_CORE
        protected ForbiddenException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
