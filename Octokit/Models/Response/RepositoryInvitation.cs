using System;

namespace Octokit
{
    public enum RepositoryInvitationPermission
    {
        Read,
        Write,
        Admin
    }
    public class RepositoryInvitation
    {
        public RepositoryInvitation()
        {
        }

        public RepositoryInvitation(int id, Repository repository, User invitee, User inviter, RepositoryInvitationPermission permissions, DateTimeOffset createdAt, string url, string htmlUrl)
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

        public RepositoryInvitationPermission Permissions { get; protected set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string Url { get; protected set; }

        public string HtmlUrl { get; protected set; }
    }
}
