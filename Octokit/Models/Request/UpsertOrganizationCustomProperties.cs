using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to create or update custom property values for a repository
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpsertOrganizationCustomProperties
    {
        public UpsertOrganizationCustomProperties() { }

        public UpsertOrganizationCustomProperties(List<OrganizationCustomPropertyUpdate> properties)
        {
            Properties = properties;
        }

        /// <summary>
        /// List of organization custom properties
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#create-or-update-custom-properties-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        [Parameter(Value = "properties")]
        public List<OrganizationCustomPropertyUpdate> Properties { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Count: {0}", Properties?.Count);
            }
        }
    }
}
