using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// organization teams
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Team
    {
        public Team() { }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Dn")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Dn")]
        public Team(Uri url, int id, string name, Permission permission, int membersCount, int reposCount, Organization organization, string ldapDn)
        {
            Url = url;
            Id = id;
            Name = name;
            Permission = permission;
            MembersCount = membersCount;
            ReposCount = reposCount;
            Organization = organization;
            LdapDn = ldapDn;
        }

        /// <summary>
        /// url for this team
        /// </summary>
        public Uri Url { get; protected set; }

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
        public Permission Permission { get; protected set; }

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
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dn")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Dn")]
        public string LdapDn { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Name: {0} ", Name); }
        }
    }
}
