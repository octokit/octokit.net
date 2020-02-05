using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class SecretRequest : RequestParameters
    {
        [Parameter(Key = "encrypted_value")]
        public string EncryptedValue { get; set; }

        [Parameter(Key = "key_id")]
        public string KeyId { get; set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "KeyId: {0}", KeyId);
    }
}
