using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

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
                return String.Format(CultureInfo.InvariantCulture, "Status: {0}", Status);
            }
        }
    }
}
