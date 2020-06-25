using Octokit.Internal;
using System.Diagnostics;

namespace Octokit
{
    /// <summary>
    /// Used to create or update a repository secret
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpsertRepositorySecret
    {
        /// <summary>
        /// The encrypted value of the secret
        /// </summary>
        [Parameter(Value = "encrypted_value")]
        public string EncryptedValue { get; set; }

        /// <summary>
        /// The key_id used to encrypt the secret
        /// </summary>
        [Parameter(Value = "key_id")]
        public string EncryptionKeyId { get; set; }
    }
}
