using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Collaborator
    {
        public Collaborator() { }

        public Collaborator(string login, long id, string email, string name, string nodeId, string avatarUrl, string gravatarUrl, string url, string htmlUrl, string followersUrl, string followingUrl, string gistsUrl, string type, string starredUrl, string subscriptionsUrl, string organizationsUrl, string reposUrl, string eventsUrl, string receivedEventsUrl, bool siteAdmin, CollaboratorPermissions permissions, string roleName)
        {
            Login = login;
            Id = id;
            Email = email;
            Name = name;
            NodeId = nodeId;
            AvatarUrl = avatarUrl;
            GravatarUrl = gravatarUrl;
            Url = url;
            HtmlUrl = htmlUrl;
            FollowersUrl = followersUrl;
            FollowingUrl = followingUrl;
            GistsUrl = gistsUrl;
            StarredUrl = starredUrl;
            SubscriptionsUrl = subscriptionsUrl;
            OrganizationsUrl = organizationsUrl;
            ReposUrl = reposUrl;
            EventsUrl = eventsUrl;
            ReceivedEventsUrl = receivedEventsUrl;
            Type = type;
            SiteAdmin = siteAdmin;
            Permissions = permissions;
            RoleName = roleName;
        }

        public string Login { get; protected set; }

        public long Id { get; protected set; }

        public string Email { get; protected set; }

        public string Name { get; protected set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; protected set; }

        public string AvatarUrl { get; protected set; }

        public string GravatarUrl { get; protected set; }

        public string Url { get; protected set; }

        public string HtmlUrl { get; protected set; }

        public string FollowersUrl { get; protected set; }

        public string FollowingUrl { get; protected set; }

        public string GistsUrl { get; protected set; }

        public string StarredUrl { get; protected set; }

        public string SubscriptionsUrl { get; protected set; }

        public string OrganizationsUrl { get; protected set; }

        public string ReposUrl { get; protected set; }

        public string EventsUrl { get; protected set; }

        public string ReceivedEventsUrl { get; protected set; }

        public string Type { get; protected set; }

        public bool SiteAdmin { get; protected set; }

        public CollaboratorPermissions Permissions { get; protected set; }

        public string RoleName { get; protected set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture,
                    $"Collaborator: Id: {Id} Login: {Login}");
    }
}
