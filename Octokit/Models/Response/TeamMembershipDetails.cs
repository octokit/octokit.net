﻿using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TeamMembershipDetails
    {
        public StringEnum<TeamRole> Role { get; protected set; }

        public StringEnum<MembershipState> State { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Role: {0} State: {1}", Role, State);
            }
        }
    }

    /// <summary>
    /// Roles within a Team
    /// </summary>
    public enum TeamRole
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
        Maintainer
    }
}
