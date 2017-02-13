using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Represents a user on GitHub.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class User : Account
    {
        public User() { }

        public User(string avatarUrl, string bio, string blog, int collaborators, string company, DateTimeOffset createdAt, int diskUsage, string email, int followers, int following, bool? hireable, string htmlUrl, int totalPrivateRepos, int id, string location, string login, string name, int ownedPrivateRepos, Plan plan, int privateGists, int publicGists, int publicRepos, string url, RepositoryPermissions permissions, bool siteAdmin, string ldapDistinguishedName, DateTimeOffset? suspendedAt)
            : base(avatarUrl, bio, blog, collaborators, company, createdAt, diskUsage, email, followers, following, hireable, htmlUrl, totalPrivateRepos, id, location, login, name, ownedPrivateRepos, plan, privateGists, publicGists, publicRepos, AccountType.User, url)
        {
            Permissions = permissions;
            SiteAdmin = siteAdmin;
            LdapDistinguishedName = ldapDistinguishedName;
            SuspendedAt = suspendedAt;
        }

        public RepositoryPermissions Permissions { get; protected set; }

        /// <summary>
        /// Whether or not the user is an administrator of the site
        /// </summary>
        public bool SiteAdmin { get; protected set; }

        /// <summary>
        /// When the user was suspended, if at all (GitHub Enterprise)
        /// </summary>
        public DateTimeOffset? SuspendedAt { get; protected set; }

        /// <summary>
        /// Whether or not the user is currently suspended
        /// </summary>
        public bool Suspended { get { return SuspendedAt.HasValue; } }

        /// <summary>
        /// LDAP Binding (GitHub Enterprise only)
        /// </summary>
        [Parameter(Key = "ldap_dn")]
        public string LdapDistinguishedName { get; protected set; }

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
