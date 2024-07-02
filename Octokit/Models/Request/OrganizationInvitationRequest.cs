using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used as part of the request to invite a user to an organization.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationInvitationRequest
    {
        public OrganizationInvitationRequest(long inviteeId)
        {
            InviteeId = inviteeId;
        }

        public OrganizationInvitationRequest(string email)
        {
            Email = email;
        }

        public OrganizationInvitationRequest(long inviteeId, OrganizationMembershipRole role)
        {
            InviteeId = inviteeId;
            Role = role;
        }

        public OrganizationInvitationRequest(string email, OrganizationMembershipRole role)
        {
            Email = email;
            Role = role;
        }

        public OrganizationInvitationRequest(long inviteeId, long[] teamIds)
        {
            InviteeId = inviteeId;
            TeamIds = teamIds;
        }

        public OrganizationInvitationRequest(string email, long[] teamIds)
        {
            Email = email;
            TeamIds = teamIds;
        }

        public OrganizationInvitationRequest(long inviteeId, OrganizationMembershipRole role, long[] teamIds)
        {
            InviteeId = inviteeId;
            Role = role;
            TeamIds = teamIds;
        }

        public OrganizationInvitationRequest(string email, OrganizationMembershipRole role, long[] teamIds)
        {
            Email = email;
            Role = role;
            TeamIds = teamIds;
        }

        /// <summary>
        /// The user ID of the person being invited. Required if Email is not specified.
        /// </summary>
        [Parameter(Key = "invitee_id")]
        public long? InviteeId { get; set; }

        /// <summary>
        /// The email address of the person being invited. Required if InviteeId is not specified.
        /// </summary>
        [Parameter(Key = "email")]
        public string Email { get; set; }

        /// <summary>
        /// The role to give the user in the organization. The default is <see cref="OrganizationMembershipRole.DirectMember"/>.
        /// </summary>
        [Parameter(Key = "role")]
        public OrganizationMembershipRole Role { get; set; } = OrganizationMembershipRole.DirectMember;

        /// <summary>
        /// The IDs for the team(s) to invite new members to
        /// </summary>
        [Parameter(Key = "team_ids")]
        public long[] TeamIds { get; set; }

        internal string DebuggerDisplay => $"InviteeId: {InviteeId}; Email: {Email}; Role: {Role}; Team IDs: {(TeamIds != null ? string.Join(", ", TeamIds) : "")}";
    }
}
