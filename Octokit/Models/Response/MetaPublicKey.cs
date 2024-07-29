using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class MetaPublicKey
    {
        public MetaPublicKey() { }

        public MetaPublicKey(string keyIdentifier, string key, bool isCurrent)
        {
            KeyIdentifier = keyIdentifier;
            Key = key;
            IsCurrent = isCurrent;
        }

        public string KeyIdentifier { get; protected set; }

        public string Key { get; protected set; }

        public bool IsCurrent { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "KeyIdentifier: {0} IsCurrent: {1}", KeyIdentifier, IsCurrent); }
        }
    }
}
