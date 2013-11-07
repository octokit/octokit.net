using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    public class NewTeam
    {
        public NewTeam(string name)
        {
            Name = name;
            RepoNames = new Collection<string>();
            Permission = Octokit.Permission.Pull;
        }

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
        [Parameter(Key="repo_names")]
        public Collection<string> RepoNames { get; private set; }
    }
}