using System.Diagnostics;

namespace Octokit
{
    /// <summary>
    /// Represents the public key used to sign the secrets to post to GitHub
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositorySecretsPublicKey
    {
        public int KeyId { get; set; }
        public string Key { get; set; }
    }
}
