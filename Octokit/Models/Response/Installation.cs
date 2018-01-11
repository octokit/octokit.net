using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents an application installation.
    /// </summary>
    /// <remarks>
    /// For more information see https://developer.github.com/v3/apps/#find-installations
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Installation
    {
        public Installation() { }

        public Installation(int id, // Account account,
            string accessTokenUrl, string repositoriesUrl, string htmlUrl,
            int appId, int targetId, AccountType targetType,
            string singleFileName, string repositorySelection)
        {
            Id = id;
            // Account = account;
            AccessTokensUrl = accessTokenUrl;
            RepositoriesUrl = repositoriesUrl;
            HtmlUrl = htmlUrl;
            AppId = appId;
            TargetId = targetId;
            TargetType = targetType;
            SingleFileName = singleFileName;
            RepositorySelection = repositorySelection;
        }

        public int Id { get; protected set; }
        // public Account Account { get; protected set; }
        public string AccessTokensUrl { get; protected set; }
        public string RepositoriesUrl { get; protected set; }
        public string HtmlUrl { get; protected set; }

        public int AppId { get; protected set; }
        public int TargetId { get; protected set; }
        public AccountType TargetType { get; protected set; }
        // TODO - add permissions
        // TODO - add events
        public string SingleFileName { get; protected set; }
        public string RepositorySelection { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Installation Id: {0} ; App Id: {1}", Id, AppId); }
        }
    }
}
