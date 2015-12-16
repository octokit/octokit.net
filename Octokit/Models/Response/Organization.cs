using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Organization : Account
    {
        public Organization() { }

        public Organization(string avatarUrl, string bio, string blog, int collaborators, string company, DateTimeOffset createdAt, int diskUsage, string email, int followers, int following, bool? hireable, string htmlUrl, int totalPrivateRepos, int id, string location, string login, string name, int ownedPrivateRepos, Plan plan, int privateGists, int publicGists, int publicRepos, string url, string billingAddress)
            : base(avatarUrl, bio, blog, collaborators, company, createdAt, diskUsage, email, followers, following, hireable, htmlUrl, totalPrivateRepos, id, location, login, name, ownedPrivateRepos, plan, privateGists, publicGists, publicRepos, AccountType.Organization, url)
        {
            BillingAddress = billingAddress;
        }

        /// <summary>
        /// The billing address for an organization. This is only returned when updating 
        /// an organization.
        /// </summary>
        public string BillingAddress { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Organization: Id: {0} Login: {1}", Id, Login);
            }
        }
    }
}