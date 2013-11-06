using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    public class NewTeam
    {
        /// <summary>
        /// team name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// permission associated to this team
        /// </summary>
        public Permission Permission { get; set; }

        /// <summary>
        /// array of repo_names this team has permissions to
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public string[] RepoNames { get; set; }
    }
}