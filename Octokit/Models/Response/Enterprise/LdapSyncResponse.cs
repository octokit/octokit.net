using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class LdapSyncResponse
    {
        public LdapSyncResponse() { }

        public LdapSyncResponse(string status)
        {
            Status = status;
        }

        public string Status
        {
            get;
            private set;
        }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Status: {0}", Status);
            }
        }
    }
}
