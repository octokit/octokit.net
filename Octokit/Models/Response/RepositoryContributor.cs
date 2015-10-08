using System.Diagnostics;

namespace Octokit
{
    /// <summary>
    /// Represents a users contributions on a GitHub repository.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryContributor : Author
    {
        public RepositoryContributor() { }

        public RepositoryContributor(string login, int id, string avatarUrl, string url, string htmlUrl, string followersUrl, string followingUrl, string gistsUrl, string type, string starredUrl, string subscriptionsUrl, string organizationsUrl, string reposUrl, string eventsUrl, string receivedEventsUrl, bool siteAdmin, int contributions)
            : base(login, id, avatarUrl, url, htmlUrl, followersUrl, followingUrl, gistsUrl, type, starredUrl, subscriptionsUrl, organizationsUrl, reposUrl, eventsUrl, receivedEventsUrl, siteAdmin)
        {
            Contributions = contributions;
        }

        public int Contributions { get; protected set; }
    }
}