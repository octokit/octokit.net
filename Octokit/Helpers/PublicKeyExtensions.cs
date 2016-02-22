using System;
using System.Text.RegularExpressions;

namespace Octokit
{
    /// <summary>
    /// Extensions for working with PublicKeys
    /// </summary>
    public static class PublicKeyExtensions
    {
#if NETFX_CORE
        static readonly Regex keyRegex = new Regex(@"ssh-[rd]s[as] (?<data>\S+) ?(?<name>.*)$");
#else
        static readonly Regex keyRegex = new Regex(@"ssh-[rd]s[as] (?<data>\S+) ?(?<name>.*)$", RegexOptions.Compiled);
#endif

        /// <summary>
        /// Gets Name and Data components of a a <see cref="PublicKey"/>
        /// </summary>
        /// <param name="key"><see cref="PublicKey"/> received from API</param>
        /// <returns><see cref="PublicKeyInfo"/> object representing the data and name components of the <see cref="PublicKey"/></returns>
        public static PublicKeyInfo GetKeyDataAndName(this PublicKey key)
        {
            Ensure.ArgumentNotNull(key, "sshKey");

            var keyInfo = key.Key;
            if (keyInfo == null) return null;
            var match = keyRegex.Match(keyInfo);
            return (match.Success ? new PublicKeyInfo(match.Groups["data"].Value, match.Groups["name"].Value) : null);
        }

        /// <summary>
        /// Gets data component from a <see cref="PublicKey"/>
        /// </summary>
        /// <param name="key"><see cref="PublicKey"/> received from API</param>
        static string GetKeyData(this PublicKey key)
        {
            var keyInfo = key.GetKeyDataAndName();
            return keyInfo == null ? null : keyInfo.Data;
        }

        /// <summary>
        /// Compare two <see cref="PublicKey"/>s to see if they have equal data components
        /// </summary>
        /// <param name="key">Reference <see cref="PublicKey"/></param>
        /// <param name="otherKey"><see cref="PublicKey"/> to compare</param>
        public static bool HasSameDataAs(this PublicKey key, PublicKey otherKey)
        {
            Ensure.ArgumentNotNull(key, "key");

            if (otherKey == null) return false;
            var keyData = key.GetKeyData();
            return keyData != null && keyData == otherKey.GetKeyData();
        }
    }
}
