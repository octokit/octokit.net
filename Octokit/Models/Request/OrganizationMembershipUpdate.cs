using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationMembershipUpdate
    {
        /// <summary>
        /// The role to give the user in the organization. The default is <see cref="OrganizationMembershipsRole.Member"/>.
        /// </summary>
        public OrganizationMembershipsRole Role { get; set; }

        internal string DebuggerDisplay => $"{Role}";
    }
}
