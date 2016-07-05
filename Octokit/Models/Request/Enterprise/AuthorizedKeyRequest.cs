using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AuthorizedKeyRequest : RequestParameters
    {
        public AuthorizedKeyRequest(string authorizedKey)
        {
            AuthorizedKey = authorizedKey;
        }

        [Parameter(Key = "authorized_key")]
        public string AuthorizedKey { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "AuthorizedKey: {0}", AuthorizedKey);
            }
        }
    }
}
