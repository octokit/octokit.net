using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dn")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Dn")]
        public NewLdapMapping(string ldapDn)
        {
            LdapDn = ldapDn;
        }

        /// <summary>
        /// The LDAP Distinguished Name (required)
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dn")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Dn")]
        public string LdapDn { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "LdapDn: {0}", LdapDn);
            }
        }
    }
}
