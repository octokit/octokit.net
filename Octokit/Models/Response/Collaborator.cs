using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Collaborator
    {
        public Collaborator() { }

        public Collaborator(string login, int id, string email, string name, string nodeId, string avatarUrl, string gravatarUrl, string url, string htmlUrl, string followersUrl, string followingUrl, string gistsUrl, string type, string starredUrl, string subscriptionsUrl, string organizationsUrl, string reposUrl, string eventsUrl, string receivedEventsUrl, bool siteAdmin, CollaboratorPermission permissions, string roleName)
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

        public int Id { get; protected set; }

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

        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "This is what is returned from the API")]
        public string Type { get; protected set; }

        public bool SiteAdmin { get; protected set; }

        public StringEnum<CollaboratorPermission> Permissions { get; protected set; }

        public string RoleName { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Collaborator: Id: {0} Login: {1}", Id, Login);
            }
        }
    }
}