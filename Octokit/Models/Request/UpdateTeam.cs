using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpdateTeam
    {
        public UpdateTeam(string team)
        {
            Name = team;
        }

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
                return String.Format(CultureInfo.InvariantCulture, "Team: {0} Permission: {1}", Name, Permission.GetValueOrDefault());
            }
        }
    }
}