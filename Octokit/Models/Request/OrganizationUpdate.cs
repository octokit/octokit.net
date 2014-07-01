using System;
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

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "{0}", Name);
            }
        }
    }
}
