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

        public Team(string url, string htmlUrl, int id, string nodeId, string slug, string name, string description, TeamPrivacy privacy, PermissionLevel permission, int membersCount, int reposCount, Organization organization, Team parent, string ldapDistinguishedName)
        {
            Url = url;
            HtmlUrl = htmlUrl;
            Id = id;
            NodeId = nodeId;
            Slug = slug;
            Name = name;
            Description = description;
            Privacy = privacy;
            Permission = permission;
            MembersCount = membersCount;
            ReposCount = reposCount;
            Organization = organization;
            Parent = parent;
            LdapDistinguishedName = ldapDistinguishedName;
        }

        /// <summary>
        /// url for this team
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The HTML URL for this team.
        /// </summary>
        public string HtmlUrl { get; private set; }

        /// <summary>
        /// team id
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// team slug
        /// </summary>
        public string Slug { get; private set; }

        /// <summary>
        /// team name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// team description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// team privacy
        /// </summary>
        public StringEnum<TeamPrivacy> Privacy { get; private set; }

        /// <summary>
        /// permission attached to this team
        /// </summary>
        public StringEnum<PermissionLevel> Permission { get; private set; }

        /// <summary>
        /// how many members in this team
        /// </summary>
        public int MembersCount { get; private set; }

        /// <summary>
        /// how many repo this team has access to
        /// </summary>
        public int ReposCount { get; private set; }

        /// <summary>
        /// who this team belongs to
        /// </summary>
        public Organization Organization { get; private set; }

        /// <summary>
        /// The parent team
        /// </summary>
        public Team Parent { get; private set; }

        /// <summary>
        /// LDAP Binding (GitHub Enterprise only)
        /// </summary>
        [Parameter(Key = "ldap_dn")]
        public string LdapDistinguishedName { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Name: {0} ", Name); }
        }
    }

    /// <summary>
    /// Used to describe a team's privacy level.
    /// </summary>
    public enum TeamPrivacy
    {
        /// <summary>
        /// Only visible to organization owners and members of the team.
        /// </summary>
        [Parameter(Value = "secret")]
        Secret,

        /// <summary>
        /// Visible to all members of the organization.
        /// </summary>
        [Parameter(Value = "closed")]
        Closed
    }
}
