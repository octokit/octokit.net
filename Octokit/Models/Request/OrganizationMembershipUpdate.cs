using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationMembershipUpdate
    {
        public OrganizationMembershipUpdate()
        {
            Role = MembershipRole.Member;
        }
        
        /// <summary>
        /// The role to give the user in the organization. The default is <see cref="MembershipRole.Member"/>.
        /// </summary>
        public MembershipRole Role { get; set; }

        internal string DebuggerDisplay => $"{Role}";
    }
}
