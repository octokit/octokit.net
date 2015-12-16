namespace Octokit
{
    public class UserWithIdentity : Author
    {
        public UserWithIdentity()
        {
        }

        public UserWithIdentity(
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
            bool siteAdmin,
            string email,
            string name)
            : base(login, id, avatarUrl, url, htmlUrl, eventsUrl, followersUrl, followingUrl, gistsUrl, organizationsUrl, receivedEventsUrl, reposUrl, starredUrl, subscriptionsUrl, siteAdmin)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; protected set; }
        public string Email { get; protected set; }
    }
}
