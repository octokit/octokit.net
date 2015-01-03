using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    /// <summary>
    /// Represents the response to a 2FA challenge from the API
    /// </summary>
    public class TwoFactorChallengeResult
    {
        /// <summary>
        /// Helper action for resending the 2FA code
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "It really is immutable yo!")]
        public static readonly TwoFactorChallengeResult RequestResendCode = new TwoFactorChallengeResult(null, true);

        /// <summary>
        /// Construct an instance of TwoFactorChallengeResult
        /// </summary>
        /// <param name="authenticationCode"></param>
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

        /// <summary>
        /// True if this request should resent an authentication code
        /// </summary>
        public bool ResendCodeRequested { get; private set; }

        /// <summary>
        /// The user-specified authentication code
        /// </summary>
        public string AuthenticationCode { get; private set; }
    }
}
