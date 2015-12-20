using System.Globalization;

namespace Octokit
{
    // Schema: simple_user_schema
    public class Author : AccountSimple
    {
        public Author()
        {
        }

        public Author(
            string login,
            int id,
            string avatarUrl,
            string url,
            string htmlUrl,
            string eventsUrl,
            string followersUrl,
            string followingUrl,
            string gistsUrl,
            string organizationsUrl,
            string receivedEventsUrl,
            string reposUrl,
            string starredUrl,
            string subscriptionsUrl,
            bool siteAdmin)
            : base(login, id, avatarUrl, url, htmlUrl, AccountType.User)
        {
            EventsUrl = eventsUrl;
            FollowersUrl = followersUrl;
            FollowingUrl = followingUrl;
            GistsUrl = gistsUrl;
            OrganizationsUrl = organizationsUrl;
            ReceivedEventsUrl = receivedEventsUrl;
            ReposUrl = reposUrl;
            StarredUrl = starredUrl;
            SubscriptionsUrl = subscriptionsUrl;
            SiteAdmin = siteAdmin;
        }

        public string EventsUrl { get; protected set; }
        public string FollowersUrl { get; protected set; }
        public string FollowingUrl { get; protected set; }
        public string GistsUrl { get; protected set; }
        public string OrganizationsUrl { get; protected set; }
        public string ReceivedEventsUrl { get; protected set; }
        public string ReposUrl { get; protected set; }
        public string StarredUrl { get; protected set; }
        public string SubscriptionsUrl { get; protected set; }
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
