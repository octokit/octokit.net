using System;
using System.Diagnostics;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Represents an organization variable.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationVariable : RepositoryVariable
    {
        public OrganizationVariable() { }

        public OrganizationVariable(string name, string value, DateTime createdAt, DateTime updatedAt, string visibility, string selectedRepositoriesUrl)
        {
            Name = name;
            Value = value;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Visibility = visibility;
            SelectedRepositoriesUrl = selectedRepositoriesUrl;
        }

        /// <summary>
        /// The visibility level of the variable within the organization
        /// </summary>
        public string Visibility { get; private set; }

        /// <summary>
        /// The URL to retrieve the list of selected repositories
        /// </summary>
        [Parameter(Key = "selected_repositories_url")]
        public string SelectedRepositoriesUrl { get; private set; }
    }
}
