using Octokit.Internal;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    public enum PermissionType
    {
        [Parameter(Value = "pull")]
        Pull,
        [Parameter(Value = "push")]
        Push,
        [Parameter(Value = "admin")]
        Admin
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TeamRepositoryUpdate
    {
        /// <summary>
        /// Used to add or update a team repository.
        /// </summary>
        public TeamRepositoryUpdate(PermissionType permission)
        {
            Permission = permission;
        }

        /// <summary>
        /// The permission to grant the team on this repository.
        /// </summary>
        [Parameter(Key = "permission")]
        public PermissionType Permission { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Permission: {0}", Permission);
            }
        }
    }
}
