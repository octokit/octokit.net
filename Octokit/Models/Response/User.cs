using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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

        public User(string avatarUrl, string bio, string blog, int collaborators, string company, DateTimeOffset createdAt, int diskUsage, string email, int followers, int following, bool? hireable, string htmlUrl, int totalPrivateRepos, int id, string location, string login, string name, int ownedPrivateRepos, Plan plan, int privateGists, int publicGists, int publicRepos, string url, bool siteAdmin, string ldapDistinguishedName)
            : base(avatarUrl, bio, blog, collaborators, company, createdAt, diskUsage, email, followers, following, hireable, htmlUrl, totalPrivateRepos, id, location, login, name, ownedPrivateRepos, plan, privateGists, publicGists, publicRepos, AccountType.User, url)
        {
            SiteAdmin = siteAdmin;
            LdapDistinguishedName = ldapDistinguishedName;
        }

        /// <summary>
        /// Whether or not the user is an administrator of the site
        /// </summary>
        public bool SiteAdmin { get; protected set; }

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
