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
    public class Installation
    {
        public Installation() { }

        public Installation(long id, User account, string accessTokenUrl, string repositoriesUrl, string htmlUrl, long appId, long targetId, AccountType targetType, InstallationPermissions permissions, IReadOnlyList<string> events, string singleFileName, string repositorySelection)
        {
            Id = id;
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
        /// The Installation Id.
        /// </summary>
        public long Id { get; protected set; }

        /// <summary>
        /// The Account associated with the Installation.
        /// </summary>
        public User Account { get; protected set; }

        /// <summary>
        /// The URL to view the Installation on GitHub.
        /// </summary>
        public string HtmlUrl { get; protected set; }

        /// <summary>
        /// The Id of the associated GitHub App.
        /// </summary>
        public long AppId { get; protected set; }

        /// <summary>
        /// The Id of the target user/organization
        /// </summary>
        public long TargetId { get; protected set; }

        /// <summary>
        /// The type of the target (User or Organization)
        /// </summary>
        public StringEnum<AccountType> TargetType { get; protected set; }
        
        /// <summary>
        /// The Permissions granted to the Installation
        /// </summary>
        public InstallationPermissions Permissions { get; private set; }

        /// <summary>
        /// The Events subscribed to by the Installation
        /// </summary>
        public IReadOnlyList<string> Events { get; private set; }

        /// <summary>
        /// The single file the GitHub App can managem (when Permissions.SingleFile is set to read or write)
        /// </summary>
        public string SingleFileName { get; protected set; }

        /// <summary>
        /// The choice of repositories the installation is on. Can be either "selected" or "all".
        /// </summary>
        public StringEnum<InstallationRepositorySelection> RepositorySelection { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Installation Id: {0} ; App Id: {1}", Id, AppId); }
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
