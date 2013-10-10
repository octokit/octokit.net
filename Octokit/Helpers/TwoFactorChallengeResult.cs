using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    public class TwoFactorChallengeResult
    {
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "It really is immutable yo!")]
        public static readonly TwoFactorChallengeResult RequestResendCode = new TwoFactorChallengeResult(null, true);

        public TwoFactorChallengeResult(string authenticationCode)
            : this(authenticationCode, false)
        {
            Ensure.ArgumentNotNull(authenticationCode, "authenticationCode");
        }

        TwoFactorChallengeResult(string authenticationCode, bool resendCodeRequested)
        {
            AuthenticationCode = authenticationCode;
            ResendCodeRequested = resendCodeRequested;
        }

        public bool ResendCodeRequested { get; private set; }

        public string AuthenticationCode { get; private set; }
    }
}
