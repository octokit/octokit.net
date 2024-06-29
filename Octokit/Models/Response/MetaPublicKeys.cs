using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class MetaPublicKeys
    {
        public MetaPublicKeys() { }

        public MetaPublicKeys(IReadOnlyList<MetaPublicKey> publicKeys)
        {
            PublicKeys = publicKeys;
        }

        public IReadOnlyList<MetaPublicKey> PublicKeys { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "PublicKeys: {0}", PublicKeys.Count); }
        }
    }
}
