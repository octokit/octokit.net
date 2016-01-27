using System;
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
            RepoNames = new Collection<string>();
            Permission = Permission.Pull;
        }

        /// <summary>
        /// The name of the team (required).
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the description of the team
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// permission associated to this team
        /// </summary>
        public Permission Permission { get; set; }

        /// <summary>
        /// array of repo_names this team has permissions to
        /// </summary>
        public Collection<string> RepoNames { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0} Permission: {1}", Name, Permission);
            }
        }
    }
}