using System;
using System.Net;
using System.Runtime.Serialization;

namespace Octokit
{
    /// <summary>
    /// Exception thrown when we receive an HttpStatusCode.Unauthorized (HTTP 401) response.
    /// </summary>
#if !NETFX_CORE
    [Serializable]
#endif
    public class AuthorizationException : ApiException
    {
        public AuthorizationException()
        {
        }

        public AuthorizationException(string message)
            : base(message, null, HttpStatusCode.Unauthorized)
        {
        }

        public AuthorizationException(string message, Exception innerException)
            : base(message, innerException, HttpStatusCode.Unauthorized)
        {
        }

#if !NETFX_CORE
        protected AuthorizationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
