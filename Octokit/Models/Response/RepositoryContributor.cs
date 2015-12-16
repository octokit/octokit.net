namespace Octokit
{
    /// <summary>
    /// Represents a users contributions on a GitHub repository.
    /// </summary>
    public class RepositoryContributor : UserWithIdentity
    {
        public RepositoryContributor() { }

        public RepositoryContributor(
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
            string name,
            int contributions)
            : base(
                  login,
                  id,
                  avatarUrl,
                  url,
                  htmlUrl,
                  followersUrl,
                  followingUrl,
                  gistsUrl,
                  starredUrl,
                  subscriptionsUrl,
                  organizationsUrl,
                  reposUrl,
                  eventsUrl,
                  receivedEventsUrl,
                  siteAdmin,
                  email,
                  name)
        {
            Contributions = contributions;
        }

        /// <summary>
        /// Gets the number of contributions this user made to the repository.
        /// </summary>
        /// <value>
        /// The contributions.
        /// </value>
        public int Contributions { get; protected set; }
    }
}