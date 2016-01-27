using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Author
    {
        public Author() { }

        public Author(string login, int id, string avatarUrl, string url, string htmlUrl, string followersUrl, string followingUrl, string gistsUrl, string type, string starredUrl, string subscriptionsUrl, string organizationsUrl, string reposUrl, string eventsUrl, string receivedEventsUrl, bool siteAdmin)
        {
            Login = login;
            Id = id;
            AvatarUrl = avatarUrl;
            Url = url;
            HtmlUrl = htmlUrl;
            FollowersUrl = followersUrl;
            FollowingUrl = followingUrl;
            GistsUrl = gistsUrl;
            Type = type;
            StarredUrl = starredUrl;
            SubscriptionsUrl = subscriptionsUrl;
            OrganizationsUrl = organizationsUrl;
            ReposUrl = reposUrl;
            EventsUrl = eventsUrl;
            ReceivedEventsUrl = receivedEventsUrl;
            SiteAdmin = siteAdmin;
        }

        public string Login { get; protected set; }

        public int Id { get; protected set; }

        public string AvatarUrl { get; protected set; }

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

        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "This is what is returned from the api")]
        public string Type { get; protected set; }

        public bool SiteAdmin { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Author: Id: {0} Login: {1}", Id, Login);
            }
        }
    }
}