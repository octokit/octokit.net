using System.Text.RegularExpressions;

namespace Octokit
{
    // TODO: this is only related to SSH keys, we should rename this

    /// <summary>
    /// Extensions for working with SSH keys
    /// </summary>
    public static class ModelExtensions
    {
#if NETFX_CORE
        static readonly Regex sshKeyRegex = new Regex(@"ssh-[rd]s[as] (?<data>\S+) ?(?<name>.*)$");
#else
        static readonly Regex sshKeyRegex = new Regex(@"ssh-[rd]s[as] (?<data>\S+) ?(?<name>.*)$", RegexOptions.Compiled);
#endif

        /// <summary>
        /// Extract SSH key information from the API response
        /// </summary>
        /// <param name="sshKey">Key details received from API</param>
        public static SshKeyInfo GetKeyDataAndName(this SshKey sshKey)
        {
            Ensure.ArgumentNotNull(sshKey, "sshKey");

            var key = sshKey.Key;
            if (key == null) return null;
            var match = sshKeyRegex.Match(key);
            return (match.Success ? new SshKeyInfo(match.Groups["data"].Value, match.Groups["name"].Value) : null);
        }

        /// <summary>
        /// Compare two SSH keys to see if they are equal
        /// </summary>
        /// <param name="key">Reference SSH key</param>
        /// <param name="otherKey">Key to compare</param>
        public static bool HasSameDataAs(this SshKey key, SshKey otherKey)
        {
            Ensure.ArgumentNotNull(key, "key");

            if (otherKey == null) return false;
            var keyData = key.GetKeyData();
            return keyData != null && keyData == otherKey.GetKeyData();
        }

        static string GetKeyData(this SshKey key)
        {
            var keyInfo = key.GetKeyDataAndName();
            return keyInfo == null ? null : keyInfo.Data;
        }
    }
}
