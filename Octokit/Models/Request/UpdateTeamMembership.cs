using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to filter requests for team members
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpdateTeamMembership
    {
        public UpdateTeamMembership(TeamRole role)
        {
            Role = role;
        }

        /// <summary>
        /// Which membership roles to get
        /// </summary>
        public TeamRole Role { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Role {0}", Role);
            }
        }
    }
}
