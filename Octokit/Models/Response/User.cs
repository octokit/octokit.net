using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Represents a user on GitHub.
    /// </summary>
    [ExcludeFromCtorWithAllPropertiesConventionTest(nameof(Type))]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class User : Account
    {
        public User() { }

        public User(string avatarUrl, string bio, string blog, int collaborators, string company, DateTimeOffset createdAt, DateTimeOffset updatedAt, int diskUsage, string email, int followers, int following, bool? hireable, string htmlUrl, int totalPrivateRepos, int id, string location, string login, string name, string nodeId, int ownedPrivateRepos, Plan plan, int privateGists, int publicGists, int publicRepos, string url, RepositoryPermissions permissions, bool siteAdmin, string ldapDistinguishedName, DateTimeOffset? suspendedAt)
            : base(avatarUrl, bio, blog, collaborators, company, createdAt, diskUsage, email, followers, following, hireable, htmlUrl, totalPrivateRepos, id, location, login, name, nodeId, ownedPrivateRepos, plan, privateGists, publicGists, publicRepos, AccountType.User, url)
        {
            Permissions = permissions;
            SiteAdmin = siteAdmin;
            LdapDistinguishedName = ldapDistinguishedName;
            SuspendedAt = suspendedAt;
            UpdatedAt = updatedAt;
        }

        public RepositoryPermissions Permissions { get; private set; }

        /// <summary>
        /// Whether or not the user is an administrator of the site
        /// </summary>
        public bool SiteAdmin { get; private set; }

        /// <summary>
        /// When the user was suspended, if at all (GitHub Enterprise)
        /// </summary>
        public DateTimeOffset? SuspendedAt { get; private set; }

        /// <summary>
        /// Whether or not the user is currently suspended
        /// </summary>
        public bool Suspended { get { return SuspendedAt.HasValue; } }

        /// <summary>
        /// LDAP Binding (GitHub Enterprise only)
        /// </summary>
        [Parameter(Key = "ldap_dn")]
        public string LdapDistinguishedName { get; private set; }

        /// <summary>
        /// Date the user account was updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "User: Id: {0} Login: {1}", Id, Login);
            }
        }
    }
}
