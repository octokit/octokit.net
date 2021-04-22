using Octokit.Internal;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to create or update a repository secret
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpsertRepositorySecret
    {
        /// <summary>
        /// The encrypted value of the secret.
        /// </summary>
        /// <remarks>See the <a href="https://developer.github.com/v3/actions/secrets/#create-or-update-a-repository-secret">API documentation</a> for more information on how to encrypt the secret</remarks>
        [Parameter(Value = "encrypted_value")]
        public string EncryptedValue { get; set; }

        /// <summary>
        /// The id of the encryption key used to encrypt the secret.
        /// </summary>
        /// <remarks>Get key and id from <see cref="RepositorySecretsClient.GetPublicKey(string, string)"/> and use the <a href="https://developer.github.com/v3/actions/secrets/#create-or-update-a-repository-secret">API documentation</a> for more information on how to encrypt the secret</remarks>
        [Parameter(Value = "key_id")]
        public string KeyId { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "KeyId: {0}", KeyId);
            }
        }
    }
}
