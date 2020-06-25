using System;
using System.Collections.Generic;
using System.Text;

namespace Octokit
{
    /// <summary>
    /// Represents the public key used to sign the secrets to post to GitHub
    /// </summary>
    public class RepositorySecretsPublicKey
    {
        public int KeyId { get; set; }
        public string Key { get; set; }
    }
}
