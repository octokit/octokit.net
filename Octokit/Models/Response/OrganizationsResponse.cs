using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationsResponse
    {
        public OrganizationsResponse()
        {
        }

        public OrganizationsResponse(int totalCount, IReadOnlyList<Repository> organizations)
        {
            TotalCount = totalCount;
            Organizations = organizations;
        }

        /// <summary>
        /// The total number of organizations
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// The retrieved organizations
        /// </summary>
        public IReadOnlyList<Repository> Organizations { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "TotalCount: {0}, Organizations: {1}", TotalCount, Organizations.Count);
    }
}
