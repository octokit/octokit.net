using System;
using System.Runtime.Serialization;

namespace Nocto
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

        public AuthenticationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

#if !NETFX_CORE
        protected AuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
#endif
    }
}
