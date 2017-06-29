using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    public enum InvitationPermissionType
    {
        [Parameter(Value = "read")]
        Read,

        [Parameter(Value = "write")]
        Write,

        [Parameter(Value = "admin")]
        Admin
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryInvitation
    {
        public RepositoryInvitation() { }

        public RepositoryInvitation(int id, Repository repository, User invitee, User inviter, InvitationPermissionType permissions, DateTimeOffset createdAt, string url, string htmlUrl)
        {
            Id = id;
            Repository = repository;
            Invitee = invitee;
            Inviter = inviter;
            Permissions = permissions;
            CreatedAt = createdAt;
            Url = url;
            HtmlUrl = htmlUrl;
        }

        public int Id { get; protected set; }

        public Repository Repository { get; protected set; }

        public User Invitee { get; protected set; }

        public User Inviter { get; protected set; }

        public StringEnum<InvitationPermissionType> Permissions { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }

        public string Url { get; protected set; }

        public string HtmlUrl { get; protected set; }

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
