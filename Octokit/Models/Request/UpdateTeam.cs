using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to update a team.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpdateTeam
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTeam"/> class.
        /// </summary>
        /// <param name="name">The updated team name.</param>
        public UpdateTeam(string name)
        {
            Name = name;
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
        public long? ParentTeamId { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Team: {0} Privacy: {1} Permission: {2}", Name, Privacy?.ToString() ?? "Default", Permission?.ToString() ?? "Default");
            }
        }
    }
}