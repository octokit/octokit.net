namespace Octokit
{
    /// <summary>
    /// Represents a user on GitHub.
    /// </summary>
    public class User : Account
    {
        /// <summary>
        /// Hex Gravatar identifier
        /// </summary>
        public string GravatarId { get; set; }

        /// <summary>
        /// Whether or not the user is an administrator of the site
        /// </summary>
        public bool SiteAdmin { get; set; }
    }
}