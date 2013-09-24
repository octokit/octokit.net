using System.Text.RegularExpressions;

namespace Octokit
{
    public static class ModelExtensions
    {
#if NETFX_CORE
        static readonly Regex sshKeyRegex = new Regex(@"ssh-[rd]s[as] (?<data>\S+) ?(?<name>.*)$");
#else
        static readonly Regex sshKeyRegex = new Regex(@"ssh-[rd]s[as] (?<data>\S+) ?(?<name>.*)$", RegexOptions.Compiled);
#endif

        public static SshKeyInfo GetKeyDataAndName(this SshKey sshKey)
        {
            Ensure.ArgumentNotNull(sshKey, "sshKey");

            var key = sshKey.Key;
            if (key == null) return null;
            var match = sshKeyRegex.Match(key);
            return (match.Success ? new SshKeyInfo(match.Groups["data"].Value, match.Groups["name"].Value) : null);
        }

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
