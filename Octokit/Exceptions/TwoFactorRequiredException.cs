using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Octokit
{
#if !NETFX_CORE
    [Serializable]
#endif
    public class TwoFactorRequiredException : AuthorizationException
    {
        public TwoFactorRequiredException()
        {
        }

        public TwoFactorRequiredException(string message) : base(message)
        {
        }

        public TwoFactorRequiredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public TwoFactorRequiredException(string message, TwoFactorType twoFactorType)
            : base(message)
        {
            TwoFactorType = twoFactorType;
        }

#if !NETFX_CORE
        protected TwoFactorRequiredException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null) return;
            TwoFactorType = (TwoFactorType) (info.GetInt32("TwoFactorType"));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("TwoFactorType", TwoFactorType);
        }
#endif

        public TwoFactorType TwoFactorType { get; private set; }
    }

    public enum TwoFactorType
    {
        None,
        Unknown,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sms")] Sms,
        AuthenticatorApp
    }
}
