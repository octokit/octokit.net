using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents updateable fields on an organization. Values that are null will not be sent in the request.
    /// Use string.empty to clear a value.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationUpdate
    {
        /// <summary>
        /// Billing email address. This address is not publicized.
        /// </summary>
        public string BillingEmail { get; set; }

        /// <summary>
        /// The company name.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// The publicly visible email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// The shorthand name of the company.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the organization.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the default permission level members have for organization repositories.
        /// </summary>
        public DefaultRepositoryPermission DefaultRepositoryPermission { get; set; }
        
        /// <summary>
        /// Toggles the ability of non-admin organization members to create repositories.
        /// </summary>
        public bool MembersCanCreateRepositories { get; set; }
        
        /// <summary>
        /// Specifies which types of repositories non-admin organization members can create. 
        /// </summary>
        public MembersAllowedRepositoryCreationType MembersAllowedRepositoryCreationType { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "{0}", Name);
            }
        }
    }
}
