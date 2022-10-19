using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Represents a Signature Verification Object in Git Data Commit Payload.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Verification
    {
        public Verification() { }

        public Verification(bool verified, VerificationReason reason, string signature, string payload)
        {
            Verified = verified;
            Reason = reason;
            Signature = signature;
            Payload = payload;
        }

        /// <summary>
        /// Does GitHub consider the signature in this commit to be verified?
        /// </summary>
        public bool Verified { get; private set; }

        /// <summary>
        /// The reason for verified value.
        /// </summary>
        [Parameter(Key = "reason")]
        public StringEnum<VerificationReason> Reason { get; private set; }

        /// <summary>
        /// The signature that was extracted from the commit.
        /// </summary>
        public string Signature { get; private set; }

        /// <summary>
        /// The value that was signed.
        /// </summary>
        public string Payload { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                     CultureInfo.InvariantCulture,
                     "Verification: {0} Verified: {1} Reason: {2} Signature: {3} Payload",
                     Verified,
                     Reason.ToString(),
                     Signature,
                     Payload);
            }
        }
    }

    public enum VerificationReason
    {
        [Parameter(Value = "expired_key")]
        ExpiredKey,

        [Parameter(Value = "not_signing_key")]
        NotSigningKey,

        [Parameter(Value = "gpgverify_error")]
        GpgVerifyError,

        [Parameter(Value = "gpgverify_unavailable")]
        GpgVerifyUnavailable,

        [Parameter(Value = "unsigned")]
        Unsigned,

        [Parameter(Value = "unknown_signature_type")]
        UnknownSignatureType,

        [Parameter(Value = "no_user")]
        NoUser,

        [Parameter(Value = "unverified_email")]
        UnverifiedEmail,

        [Parameter(Value = "bad_email")]
        BadEmail,

        [Parameter(Value = "unknown_key")]
        UnknownKey,

        [Parameter(Value = "malformed_signature")]
        MalformedSignature,

        [Parameter(Value = "invalid")]
        Invalid,

        [Parameter(Value = "valid")]
        Valid
    }
}
