using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationOidcSubjectClaim
    {
        public OrganizationOidcSubjectClaim()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationOidcSubjectClaim"/> class
        /// </summary>
        /// <param name="includeClaimKeys">Array of unique strings. Each claim key can only contain alphanumeric characters and underscores.</param>
        public OrganizationOidcSubjectClaim(List<string> includeClaimKeys)
        {
            this.IncludeClaimKeys = includeClaimKeys;
        }


        /// <summary>
        /// Array of unique strings. Each claim key can only contain alphanumeric characters and underscores.
        /// </summary>
        public IReadOnlyList<string> IncludeClaimKeys { get; protected set; }


        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "OrganizationOidcSubjectClaimResponse: IncludeClaimKeys {0}", IncludeClaimKeys);
            }
        }
    }
}
