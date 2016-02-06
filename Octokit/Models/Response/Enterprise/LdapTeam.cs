using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class LdapTeam : Team
    {
        public LdapTeam() { }

        public LdapTeam(Uri url, int id, string name, Permission permission, int membersCount, int reposCount, Organization organization, string ldapDN)
            : base(url, id, name, permission, membersCount, reposCount, organization)
        {
            LdapDN = ldapDN;
        }

        public string LdapDN { get; protected set; }

        internal new string DebuggerDisplay
        {
            get { return base.DebuggerDisplay; }
        }
    }
}
