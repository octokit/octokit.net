using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Represents an application installation.
    /// </summary>
    /// <remarks>
    /// For more information see https://developer.github.com/v3/apps/#find-installations
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Installation : InstallationId
    {
        public Installation() { }

        public Installation(long id, User account, string accessTokenUrl, string repositoriesUrl, string htmlUrl, long appId, long targetId, AccountType targetType, InstallationPermissions permissions, IReadOnlyList<string> events, string singleFileName, string repositorySelection) : base(id)
        {
            Account = account;
            HtmlUrl = htmlUrl;
            AppId = appId;
            TargetId = targetId;
            TargetType = targetType;
            Permissions = permissions;
            Events = events;
            SingleFileName = singleFileName;
            RepositorySelection = repositorySelection;
        }

        /// <summary>
        /// The user who owns the Installation.
        /// </summary>
        public User Account { get; private set; }

        /// <summary>
        /// The URL to view the Installation on GitHub.
        /// </summary>
        public string HtmlUrl { get; private set; }

        /// <summary>
        /// The Id of the associated GitHub App.
        /// </summary>
        public long AppId { get; private set; }

        /// <summary>
        /// The Id of the User/Organization the Installation is installed in
        /// </summary>
        public long TargetId { get; private set; }

        /// <summary>
        /// The type of the target (User or Organization)
        /// </summary>
        public StringEnum<AccountType> TargetType { get; private set; }

        /// <summary>
        /// The Permissions granted to the Installation
        /// </summary>
        public InstallationPermissions Permissions { get; private set; }

        /// <summary>
        /// The Events subscribed to by the Installation
        /// </summary>
        public IReadOnlyList<string> Events { get; private set; }

        /// <summary>
        /// The single file the GitHub App can manage (when Permissions.SingleFile is set to read or write)
        /// </summary>
        public string SingleFileName { get; private set; }

        /// <summary>
        /// The choice of repositories the installation is on. Can be either "selected" or "all".
        /// </summary>
        public StringEnum<InstallationRepositorySelection> RepositorySelection { get; private set; }

        internal new string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0} AppId: {1}", Id, AppId); }
        }
    }

    public enum InstallationRepositorySelection
    {
        [Parameter(Value = "all")]
        All,

        [Parameter(Value = "selected")]
        Selected
    }
}
