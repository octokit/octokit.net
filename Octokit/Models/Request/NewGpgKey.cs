using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gpg")]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewGpgKey
    {
        public NewGpgKey()
        {
        }

        public NewGpgKey(string publicKey)
        {
            Ensure.ArgumentNotNullOrEmptyString(publicKey, nameof(publicKey));

            ArmoredPublicKey = publicKey;
        }

        public string ArmoredPublicKey { get; set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "ArmoredPublicKey: {0}", ArmoredPublicKey); }
        }
    }
}
