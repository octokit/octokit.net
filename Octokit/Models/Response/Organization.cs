using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [ExcludeFromCtorWithAllPropertiesConventionTest(nameof(Type))]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Organization : Account
    {
        public Organization() { }

        public Organization(string avatarUrl, string bio, string blog, int collaborators, string company, DateTimeOffset createdAt, int diskUsage, string email, int followers, int following, bool? hireable, string htmlUrl, int totalPrivateRepos, int id, string nodeId, string location, string login, string name, int ownedPrivateRepos, Plan plan, int privateGists, int publicGists, int publicRepos, string url, string billingAddress, StringEnum<DefaultRepositoryPermission> defaultRepositoryPermission, bool membersCanCreateRepositories, StringEnum<MembersAllowedRepositoryCreationType> membersAllowedRepositoryCreationType)
            : base(avatarUrl, bio, blog, collaborators, company, createdAt, diskUsage, email, followers, following, hireable, htmlUrl, totalPrivateRepos, id, location, login, name, nodeId, ownedPrivateRepos, plan, privateGists, publicGists, publicRepos, AccountType.Organization, url)
        {
            BillingAddress = billingAddress;
            DefaultRepositoryPermission = defaultRepositoryPermission;
            MembersCanCreateRepositories = membersCanCreateRepositories;
            MembersAllowedRepositoryCreationType = membersAllowedRepositoryCreationType;
        }

        /// <summary>
        /// The billing address for an organization. This is only returned when updating 
        /// an organization.
        /// </summary>
        public string BillingAddress { get; protected set; }
        
        /// <summary>
        /// Default permission level members have for organization repositories.
        /// </summary>
        public StringEnum<DefaultRepositoryPermission> DefaultRepositoryPermission { get; protected set; }

        /// <summary>
        /// Toggles the ability of non-admin organization members to create repositories.
        /// </summary>
        public bool MembersCanCreateRepositories { get; protected set; }

        /// <summary>
        /// Specifies which types of repositories non-admin organization members can create. 
        /// </summary>
        public StringEnum<MembersAllowedRepositoryCreationType> MembersAllowedRepositoryCreationType { get; protected set; }

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