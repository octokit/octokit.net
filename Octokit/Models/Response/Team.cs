using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// organization teams
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Team
    {
        public Team() { }

        public Team(string url, int id, string name, Permission permission, int membersCount, int reposCount, Organization organization, string ldapDistinguishedName)
        {
            Url = url;
            Id = id;
            Name = name;
            Permission = permission;
            MembersCount = membersCount;
            ReposCount = reposCount;
            Organization = organization;
            LdapDistinguishedName = ldapDistinguishedName;
        }

        /// <summary>
        /// url for this team
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// team id
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// team name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// permission attached to this team
        /// </summary>
        public StringEnum<Permission> Permission { get; protected set; }

        /// <summary>
        /// how many members in this team
        /// </summary>
        public int MembersCount { get; protected set; }

        /// <summary>
        /// how many repo this team has access to
        /// </summary>
        public int ReposCount { get; protected set; }

        /// <summary>
        /// who this team belongs to
        /// </summary>
        public Organization Organization { get; protected set; }

        /// <summary>
        /// LDAP Binding (GitHub Enterprise only)
        /// </summary>
        [Parameter(Key = "ldap_dn")]
        public string LdapDistinguishedName { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Name: {0} ", Name); }
        }
    }
}
