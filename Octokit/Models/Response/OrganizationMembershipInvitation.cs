using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationMembershipInvitation
    {
        public OrganizationMembershipInvitation()
        {
        }

        public OrganizationMembershipInvitation(int id, string nodeId, string login, string email, OrganizationMembershipRole role, DateTimeOffset createdAt, User inviter)
        {
            Id = id;
            NodeId = nodeId;
            Login = login;
            Email = email;
            Role = role;
            CreatedAt = createdAt;
            Inviter = inviter;
        }

        public int Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        public string Login { get; private set; }
        public string Email { get; private set; }
        public StringEnum<OrganizationMembershipRole> Role { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public User Inviter { get; private set; }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "Used by DebuggerDisplayAttribute")]
        internal string DebuggerDisplay => $"{nameof(OrganizationMembershipInvitation)}: Invitee: {Login ?? "Non-GitHub member"}; Email: {Email}; Inviter: {Inviter.Login}";
    }
}
