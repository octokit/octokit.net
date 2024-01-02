using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationOidcSubjectClaimRequest
    {
        public OrganizationOidcSubjectClaimRequest()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationOidcSubjectClaimRequest"/> class
        /// </summary>
        /// <param name="includeClaimKeys">Array of unique strings. Each claim key can only contain alphanumeric characters and underscores.</param>
        public OrganizationOidcSubjectClaimRequest(List<string> includeClaimKeys)
        {
            this.IncludeClaimKeys = includeClaimKeys;
        }


        /// <summary>
        /// Array of unique strings. Each claim key can only contain alphanumeric characters and underscores.
        /// </summary>
        public List<string> IncludeClaimKeys { get; set; }


        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "OrganizationOidcSubjectClaimRequest: IncludeClaimKeys {0}", IncludeClaimKeys);
            }
        }
    }
}
