using System;
using System.Diagnostics;
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
    public class TwoFactorRequiredException : AuthorizationException
    {
        public TwoFactorRequiredException() : this(TwoFactorType.None)
        {
        }

        public TwoFactorRequiredException(TwoFactorType twoFactorType)
            : this(new ApiResponse<object> { StatusCode = HttpStatusCode.Unauthorized}, twoFactorType)
        {
        }

        public TwoFactorRequiredException(IResponse response, TwoFactorType twoFactorType) : base(response)
        {
            Debug.Assert(response != null && response.StatusCode == HttpStatusCode.Unauthorized,
                "TwoFactorRequiredException status code should be 401");

            TwoFactorType = twoFactorType;
        }

        public override string Message
        {
            get { return ApiErrorMessageSafe ?? "Two-factor authentication code is required"; }
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
