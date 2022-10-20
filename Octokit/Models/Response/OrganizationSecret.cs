using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Octokit
{
    /// <summary>
    /// Represents an organization secret.
    /// Does not contain the secret value
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationSecret : RepositorySecret
    {
        public OrganizationSecret() { }

        public OrganizationSecret(string name, DateTime createdAt, DateTime updatedAt, string visibility, string selectedRepositoriesUrl)
        {
            Name = name;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Visibility = visibility;
            SelectedRepositoriesUrl = selectedRepositoriesUrl;
        }

        /// <summary>
        /// The visibility level of the secret within the organization
        /// </summary>
        public string Visibility { get; private set; }

        /// <summary>
        /// The URL to retrieve the list of selected repositories
        /// </summary>
        [Parameter(Key = "selected_repositories_url")]
        public string SelectedRepositoriesUrl { get; private set; }
    }
}
