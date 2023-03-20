using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Represents response of variables for an organization.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationVariablesCollection
    {
        public OrganizationVariablesCollection()
        {
        }

        public OrganizationVariablesCollection(int count, IReadOnlyList<OrganizationVariable> variables)
        {
            Count = count;
            Variables = variables;
        }

        /// <summary>
        /// The total count of variables for the organization
        /// </summary>
        [Parameter(Key = "total_count")]
        public int Count { get; private set; }

        /// <summary>
        /// The list of variables for the organization
        /// </summary>
        [Parameter(Key = "variables")]
        public IReadOnlyList<OrganizationVariable> Variables { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "OrganizationVariablesCollection: Count: {0}", Count);
    }
}
