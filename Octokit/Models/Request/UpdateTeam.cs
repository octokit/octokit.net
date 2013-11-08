using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
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
    }
}