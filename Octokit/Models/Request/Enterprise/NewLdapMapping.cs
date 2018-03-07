using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

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
        /// <param name="ldapDistinguishedName">The LDAP Distinguished Name</param>
        public NewLdapMapping(string ldapDistinguishedName)
        {
            Ensure.ArgumentNotNullOrEmptyString(ldapDistinguishedName, nameof(ldapDistinguishedName));

            LdapDistinguishedName = ldapDistinguishedName;
        }

        /// <summary>
        /// The LDAP Distinguished Name (required)
        /// </summary>
        [Parameter(Key = "ldap_dn")]
        public string LdapDistinguishedName { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "LdapDistinguishedName: {0}", LdapDistinguishedName);
            }
        }
    }
}
