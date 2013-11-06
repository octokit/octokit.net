using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    public class UpdateTeam
    {
        /// <summary>
        /// team name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// permission for this team
        /// </summary>
        public Permission Permission { get; set; }
    }
}