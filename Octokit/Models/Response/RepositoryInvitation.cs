using Octokit.Internal;
using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    public enum InvitationPermissionType
    {
        Read,
        Write,
        Admin,
        /// <summary>
        /// Used as a placeholder for unknown fields
        /// </summary>
        Unknown
    }

    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryInvitation
    {
        public RepositoryInvitation() { }

        public RepositoryInvitation(int id, Repository repository, User invitee, User inviter, DateTimeOffset createdAt, string url, string htmlUrl)
        {
            Id = id;
            Repository = repository;
            Invitee = invitee;
            Inviter = inviter;
            CreatedAt = createdAt;
            Url = url;
            HtmlUrl = htmlUrl;
        }

        public int Id { get; protected set; }

        public Repository Repository { get; protected set; }

        public User Invitee { get; protected set; }

        public User Inviter { get; protected set; }

        [Parameter(Key = "IgnoreThisField")]
        public InvitationPermissionType? Permissions { get { return PermissionsText.ParseEnumWithDefault(InvitationPermissionType.Unknown); } }

        [Parameter(Key = "permissions")]
        public string PermissionsText { get; protected set; }

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
