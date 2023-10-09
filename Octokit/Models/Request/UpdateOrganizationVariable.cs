using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Represents request to update the value of a variable for an organization.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpdateOrganizationVariable : UpdateRepositoryVariable
    {
        public UpdateOrganizationVariable() { }

        public UpdateOrganizationVariable(string value, string visibility, IEnumerable<long> selectedRepositoriesIds)
        {
            Value = value;
            Visibility = visibility;
            SelectedRepositoriesIds = selectedRepositoriesIds;
        }

        /// <summary>
        /// The visibility level of the variable
        /// </summary>
        [Parameter(Key = "visibility")]
        public string Visibility { get; set; }

        /// <summary>
        /// The list of ids for the repositories with selected visibility to the variable
        /// </summary>
        [Parameter(Key = "selected_repository_ids")]
        public IEnumerable<long> SelectedRepositoriesIds { get; set; }

        internal new string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "UpdateOrganizationVariable: Value: {0}", Value);
    }
}
