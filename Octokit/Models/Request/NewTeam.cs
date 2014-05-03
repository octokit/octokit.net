using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewTeam
    {
        public NewTeam(string name)
        {
            Name = name;
            RepoNames = new Collection<string>();
            Permission = Permission.Pull;
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
        public Collection<string> RepoNames { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Name: {0} Permission: {1}", Name, Permission);
            }
        }
    }
}