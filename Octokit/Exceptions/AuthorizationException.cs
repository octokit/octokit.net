using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.Serialization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Exception thrown when we receive an HttpStatusCode.Unauthorized (HTTP 401) response.
    /// </summary>
#if !NETFX_CORE
    [Serializable]
#endif
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors",
        Justification = "These exceptions are specific to the GitHub API and not general purpose exceptions")]
    public class AuthorizationException : ApiException
    {
        public AuthorizationException() : base(new ApiResponse<object> { StatusCode = HttpStatusCode.Unauthorized })
        {
        }

        public AuthorizationException(IResponse response)
            : this(response, null)
        {
        }

        public AuthorizationException(IResponse response, Exception innerException)
            : base(response, innerException)
        {
            Debug.Assert(response != null && response.StatusCode == HttpStatusCode.Unauthorized,
                "AuthorizationException created with wrong status code");
        }

#if !NETFX_CORE
        protected AuthorizationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
