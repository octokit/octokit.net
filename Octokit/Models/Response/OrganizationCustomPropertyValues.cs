using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// List of custom property values for a repository
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationCustomPropertyValues
    {
        public OrganizationCustomPropertyValues() { }

        public OrganizationCustomPropertyValues(long repositoryId, string repositoryName, string repositoryFullName, IReadOnlyList<CustomPropertyValue> properties)
        {
            RepositoryId = repositoryId;
            RepositoryName = repositoryName;
            RepositoryFullName = repositoryFullName;
            Properties = properties;
        }

        /// <summary>
        /// The repository Id
        /// </summary>
        public long RepositoryId { get; private set; }

        /// <summary>
        /// The name of the repository
        /// </summary>
        public string RepositoryName { get; private set; }

        /// <summary>
        /// The full name of the repository (owner/repo)
        /// </summary>
        public string RepositoryFullName { get; private set; }

        /// <summary>
        /// List of custom property names and associated values
        /// </summary>
        public IReadOnlyList<CustomPropertyValue> Properties { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, "RepositoryFullName: {0}", RepositoryFullName);
    }
}
