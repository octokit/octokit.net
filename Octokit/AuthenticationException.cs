using System;
using System.Net;
using System.Runtime.Serialization;

namespace Octokit
{
#if !NETFX_CORE
    [Serializable]
#endif
    public class AuthenticationException : Exception
    {
        public AuthenticationException()
        {
        }

        public AuthenticationException(string message)
            : base(message)
        {
        }

        public AuthenticationException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public AuthenticationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public AuthenticationException(string message, Exception innerException, HttpStatusCode statusCode)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; private set; }

#if !NETFX_CORE
        protected AuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info == null) return;
            StatusCode = (HttpStatusCode)(info.GetInt32("StatusCode"));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("StatusCode", StatusCode);
        }
#endif
    }
}
