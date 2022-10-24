using Octokit.Internal;
using System;
using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationCredential
    {
        public OrganizationCredential() { }

        public OrganizationCredential(
            string login,
            long credentialId,
            string credentialType,
            string tokenLastEight,
            DateTime credentialAuthorizedAt,
            string[] scopes,
            string fingerprint,
            DateTime? credentialAccessedAt,
            long? authorizedCredentialId,
            string authorizedCredentialTitle,
            string authorizedCredentialNote,
            DateTime? authorizedCredentialExpiresAt)
        {
            Login = login;
            CredentialId = credentialId;
            CredentialType = credentialType;
            TokenLastEight = tokenLastEight;
            CredentialAuthorizedAt = credentialAuthorizedAt;
            Scopes = scopes;
            Fingerprint = fingerprint;
            CredentialAccessedAt = credentialAccessedAt;
            AuthorizedCredentialId = authorizedCredentialId;
            AuthorizedCredentialTitle = authorizedCredentialTitle;
            AuthorizedCredentialNote = authorizedCredentialNote;
            AuthorizedCredentialExpiresAt = authorizedCredentialExpiresAt;
        }



        /// <summary>
        /// User login that owns the underlying credential.
        /// </summary>
        public string Login { get; private set; }

        /// <summary>
        /// Unique identifier for the credential.
        /// </summary>
        public long CredentialId { get; private set; }

        /// <summary>
        /// Human-readable description of the credential type.
        /// </summary>
        public string CredentialType { get; private set; }

        /// <summary>
        /// Last eight characters of the credential. Only included in responses with credential_type of personal access token.
        /// </summary>
        public string TokenLastEight { get; private set; }

        /// <summary>
        /// Date when the credential was authorized for use.
        /// </summary>
        public DateTime CredentialAuthorizedAt { get; private set; }

        /// <summary>
        /// List of oauth scopes the token has been granted.
        /// </summary>
        public string[] Scopes { get; private set; }

        /// <summary>
        /// Unique string to distinguish the credential. Only included in responses with credential_type of SSH Key.
        /// </summary>
        public string Fingerprint { get; private set; }

        /// <summary>
        /// Date when the credential was last accessed. May be null if it was never accessed
        /// </summary>
        public DateTime? CredentialAccessedAt { get; private set; }

        public long? AuthorizedCredentialId { get; private set; }

        /// <summary>
        /// The title given to the ssh key. This will only be present when the credential is an ssh key.
        /// </summary>
        public string AuthorizedCredentialTitle { get; private set; }

        /// <summary>
        /// The note given to the token. This will only be present when the credential is a token.
        /// </summary>
        public string AuthorizedCredentialNote { get; private set; }

        /// <summary>
        /// The expiry for the token. This will only be present when the credential is a token.
        /// </summary>
        public DateTime? AuthorizedCredentialExpiresAt { get; private set; }

        internal string DebuggerDisplay => new SimpleJsonSerializer().Serialize(this);
    }
}