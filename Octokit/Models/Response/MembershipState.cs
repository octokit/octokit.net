using System;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// States of a Team/Organization Membership
    /// </summary>
    public enum MembershipState
    {
        /// <summary>
        /// The membership is pending
        /// </summary>
        [Parameter(Value = "pending")]
        Pending,

        /// <summary>
        /// The membership is active
        /// </summary>
        [Parameter(Value = "active")]
        Active
    }
}
