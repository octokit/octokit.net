using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to update a teamm.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpdateTeam
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTeam"/> class.
        /// </summary>
        /// <param name="team">The team.</param>
        public UpdateTeam(string team)
        {
            Name = team;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTeam"/> class.
        /// </summary>
        /// <param name="team">The team.</param>
        /// <param name="permission">The permission.</param>
        public UpdateTeam(string team, Permission permission)
        {
            Name = team;
            Permission = permission;
        }

        /// <summary>
        /// team name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// permission for this team
        /// </summary>
        public Permission? Permission { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Team: {0} Permission: {1}", Name, Permission.GetValueOrDefault());
            }
        }
    }
}