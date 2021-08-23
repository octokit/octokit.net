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
        public string Url { get; protected set; }

        /// <summary>
        /// The HTML URL for this team.
        /// </summary>
        public string HtmlUrl { get; protected set; }

        /// <summary>
        /// team id
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; protected set; }

        /// <summary>
        /// team slug
        /// </summary>
        public string Slug { get; protected set; }

        /// <summary>
        /// team name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// team description
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// team privacy
        /// </summary>
        public StringEnum<TeamPrivacy> Privacy { get; protected set; }

        /// <summary>
        /// permission attached to this team
        /// </summary>
        public StringEnum<PermissionLevel> Permission { get; protected set; }

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
        /// The parent team
        /// </summary>
        public Team Parent { get; protected set; }

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
