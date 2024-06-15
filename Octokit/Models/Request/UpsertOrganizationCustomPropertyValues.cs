using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to create or update custom property values for organization repositories
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpsertOrganizationCustomPropertyValues
    {
        public UpsertOrganizationCustomPropertyValues() { }

        public UpsertOrganizationCustomPropertyValues(List<string> repositoryNames, List<CustomPropertyValueUpdate> properties)
        {
            RepositoryNames = repositoryNames;
            Properties = properties;
        }

        /// <summary>
        /// List of repository names that should create or update custom property values
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#create-or-update-custom-property-values-for-organization-repositories">API documentation</a> for more information.
        /// </remarks>
        [Parameter(Value = "repository_names")]
        public List<string> RepositoryNames { get; set; }

        /// <summary>
        /// List of organization custom properties
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/orgs/custom-properties#create-or-update-custom-properties-for-an-organization">API documentation</a> for more information.
        /// </remarks>
        [Parameter(Value = "properties")]
        public List<CustomPropertyValueUpdate> Properties { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Count: {0}", Properties?.Count);
            }
        }
    }
}
