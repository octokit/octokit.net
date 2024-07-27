using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Repository invitations let you manage who you collaborate with.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryInvitation
    {
        public RepositoryInvitation() { }

        public RepositoryInvitation(long id, string nodeId, Repository repository, User invitee, User inviter, InvitationPermissionType permissions, DateTimeOffset createdAt, bool expired, string url, string htmlUrl)
        {
            Id = id;
            NodeId = nodeId;
            Repository = repository;
            Invitee = invitee;
            Inviter = inviter;
            Permissions = permissions;
            CreatedAt = createdAt;
            Expired = expired;
            Url = url;
            HtmlUrl = htmlUrl;
        }

        /// <summary>
        /// Unique identifier of the repository invitation.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        public Repository Repository { get; private set; }

        public User Invitee { get; private set; }

        public User Inviter { get; private set; }

        /// <summary>
        /// The permission associated with the invitation.
        /// </summary>
        public StringEnum<InvitationPermissionType> Permissions { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// Whether or not the invitation has expired
        /// </summary>
        public bool Expired { get; private set; }

        /// <summary>
        /// URL for the repository invitation
        /// </summary>
        public string Url { get; private set; }

        public string HtmlUrl { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture,
                    "Repository Invitation: Id: {0} Permissions: {1}", Id, Permissions);
    }
}
