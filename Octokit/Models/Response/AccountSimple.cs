namespace Octokit
{
    /// <summary>
    /// Some API responses contain a bit of information about a GitHub account, but not all the information.
    /// This is the base class for those response types.
    /// </summary>
    public abstract class AccountSimple
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountSimple"/> class.
        /// </summary>
        protected AccountSimple() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountSimple"/> class.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="avatarUrl">The avatar URL.</param>
        /// <param name="url">The URL.</param>
        /// <param name="htmlUrl">The HTML URL.</param>
        /// <param name="type">The type.</param>
        protected AccountSimple(string login, int id, string avatarUrl, string url, string htmlUrl, AccountType type)
        {
            Login = login;
            Id = id;
            AvatarUrl = avatarUrl;
            Url = url;
            HtmlUrl = htmlUrl;
            Type = type;
        }

        /// <summary>
        /// URL of the account's avatar.
        /// </summary>
        public string AvatarUrl { get; protected set; }

        /// <summary>
        /// The HTML URL for the account on github.com (or GitHub Enterprise).
        /// </summary>
        public string HtmlUrl { get; protected set; }

        /// <summary>
        /// The account's system-wide unique ID.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The account's login.
        /// </summary>
        public string Login { get; protected set; }

        /// <summary>
        /// The type of account associated with this entity
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public AccountType? Type { get; protected set; }

        /// <summary>
        /// The account's API URL.
        /// </summary>
        public string Url { get; protected set; }
    }
}
