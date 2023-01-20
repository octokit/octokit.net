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
        /// Although permission can be one of : pull, push, or admin based on documentation, passing admin results in an error response.
        /// That's why TeamPermission does not contain an admin value.
        /// See the issue here https://github.com/github/rest-api-description/issues/1952
        /// </summary>
        public TeamPermission? Permission { get; set; }

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