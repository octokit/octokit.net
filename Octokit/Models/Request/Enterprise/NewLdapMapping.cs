using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new organization to create via the <see cref="IEnterpriseOrganizationClient.Create(NewOrganization)" /> method.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewLdapMapping
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewLdapMapping"/> class.
        /// </summary>
        /// <param name="ldapDn">The LDAP Distinguished Name</param>
        public NewLdapMapping(string ldapDN)
        {
            LdapDN = ldapDN;
        }

        /// <summary>
        /// The LDAP Distinguished Name (required)
        /// </summary>
        public string LdapDN { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "LdapDn: {0}", LdapDN);
            }
        }
    }
}
