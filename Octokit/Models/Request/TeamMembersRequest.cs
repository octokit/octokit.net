using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to filter requests for team members
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TeamMembersRequest : RequestParameters
    {
        public TeamMembersRequest(TeamRoleFilter role)
        {
            Role = role;
        }

        /// <summary>
        /// Which membership roles to get
        /// </summary>
        public TeamRoleFilter Role { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Role {0} ", Role);
            }
        }
    }

    /// <summary>
    /// Filtering by Roles within a Team
    /// </summary>
    public enum TeamRoleFilter
    {
        /// <summary>
        /// Regular Team Member
        /// </summary>
        [Parameter(Value = "member")]
        Member,

        /// <summary>
        ///  Team Maintainer
        /// </summary>
        [Parameter(Value = "maintainer")]
        Maintainer,

        /// <summary>
        /// All Roles
        /// </summary>
        [Parameter(Value = "all")]
        All
    }
}
