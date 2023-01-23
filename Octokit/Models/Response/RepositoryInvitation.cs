using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryInvitation
    {
        public RepositoryInvitation() { }

        public RepositoryInvitation(int id, string nodeId, Repository repository, User invitee, User inviter, InvitationPermissionType permissions, DateTimeOffset createdAt, string url, string htmlUrl)
        {
            Id = id;
            NodeId = nodeId;
            Repository = repository;
            Invitee = invitee;
            Inviter = inviter;
            Permissions = permissions;
            CreatedAt = createdAt;
            Url = url;
            HtmlUrl = htmlUrl;
        }

        public int Id { get; private set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; private set; }

        public Repository Repository { get; private set; }

        public User Invitee { get; private set; }

        public User Inviter { get; private set; }

        public StringEnum<InvitationPermissionType> Permissions { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public string Url { get; private set; }

        public string HtmlUrl { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Repository Invitation: Id: {0} Permissions: {1}", Id, Permissions);
            }
        }
    }
}
