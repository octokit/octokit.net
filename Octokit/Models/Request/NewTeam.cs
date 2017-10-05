using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to create a team.
    /// </summary>
    /// <remarks>
    /// <para>
    /// In order to create a team, the authenticated user must be a member of :org.
    /// </para>
    /// <para>API: https://developer.github.com/v3/orgs/teams/#create-team</para>
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewTeam
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewTeam"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NewTeam(string name)
        {
            Name = name;
            Maintainers = new Collection<string>();
            RepoNames = new Collection<string>();
        }

        /// <summary>
        /// The name of the team (required).
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The description of the team.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The logins of organization members to add as maintainers of the team
        /// </summary>
        public Collection<string> Maintainers { get; protected set; }

        /// <summary>
        /// The full name (e.g., "organization-name/repository-name") of repositories to add the team to
        /// </summary>
        public Collection<string> RepoNames { get; protected set; }

        /// <summary>
        /// The level of privacy this team should have (default: Secret)
        /// </summary>
        public TeamPrivacy? Privacy { get; set; }

        /// <summary>
        /// The permission that new repositories will be added to the team with when none is specified (default: Pull)
        /// </summary>
        public Permission? Permission { get; set; }

        /// <summary>
        /// Id of a team to set as the parent team
        /// </summary>
        public int? ParentTeamId { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0} Privacy: {1} Permission: {2}", Name, Privacy?.ToString() ?? "Default", Permission?.ToString() ?? "Default");
            }
        }
    }
}