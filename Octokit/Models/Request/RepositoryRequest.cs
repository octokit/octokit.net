using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to request and filter a list of repositories.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryRequest : RequestParameters
    {
        /// <summary>
        /// Gets or sets the repository type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public RepositoryType? Type { get; set; }

        /// <summary>
        /// Gets or sets the sort property.
        /// </summary>
        /// <value>
        /// The sort.
        /// </value>
        public RepositorySort? Sort { get; set; }

        /// <summary>
        /// Gets or sets the sort direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public SortDirection? Direction { get; set; }

        /// <summary>
        /// Gets or sets the visibility property.
        /// </summary>
        /// <value>
        ///  The visibility.
        /// </value>
        public RepositoryRequestVisibility? Visibility { get; set; }

        /// <summary>
        /// Gets or sets the affiliation property.
        /// </summary>
        /// <value>
        ///  The affiliation.
        /// </value>
        public RepositoryAffiliation? Affiliation { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                var propValues = new List<string>();
                if (Type.HasValue)
                    propValues.Add(string.Format(CultureInfo.InvariantCulture, "Type: {0}", Type));
                if (Sort.HasValue)
                    propValues.Add(string.Format(CultureInfo.InvariantCulture, "Sort: {0}", Sort));
                if (Direction.HasValue)
                    propValues.Add(string.Format(CultureInfo.InvariantCulture, "Direction: {0}", Direction));
                if (Visibility.HasValue)
                    propValues.Add(string.Format(CultureInfo.InvariantCulture, "Visibility: {0}", Visibility));
                if (Affiliation.HasValue)
                    propValues.Add(string.Format(CultureInfo.InvariantCulture, "Affiliation: {0}", Affiliation));

                return string.Join(", ", propValues);
            }
        }
    }

    /// <summary>
    /// The properties that repositories can be filtered by.
    /// </summary>
    public enum RepositoryType
    {
        /// <summary>
        /// Return all repositories.
        /// </summary>
        [Parameter(Value = "all")]
        All,

        /// <summary>
        /// Return repositories that the current authenticated user owns.
        /// </summary>
        [Parameter(Value = "owner")]
        Owner,

        /// <summary>
        /// Returns public repositories.
        /// </summary>
        [Parameter(Value = "public")]
        Public,

        /// <summary>
        /// The privateReturn private repositories.
        /// </summary>
        [Parameter(Value = "private")]
        Private,

        /// <summary>
        /// Return repositories for which the current authenticated user is a member of the org or team.
        /// </summary>
        [Parameter(Value = "member")]
        Member
    }

    /// <summary>
    /// The properties that repositories can be sorted by.
    /// </summary>
    public enum RepositorySort
    {
        /// <summary>
        /// Sort by the date the repository was created.
        /// </summary>
        [Parameter(Value = "created")]
        Created,

        /// <summary>
        /// Sort by the date the repository was last updated.
        /// </summary>
        [Parameter(Value = "updated")]
        Updated,

        /// <summary>
        /// Sort by the date the repository was last pushed.
        /// </summary>
        [Parameter(Value = "pushed")]
        Pushed,

        /// <summary>
        /// Sort by the repository name.
        /// </summary>
        [Parameter(Value = "full_name")]
        FullName
    }

    /// <summary>
    /// The properties that repositories can be visible by.
    /// </summary>
    public enum RepositoryRequestVisibility
    {
        /// <summary>
        /// Returns only public repositories
        /// </summary>
        [Parameter(Value = "public")]
        Public,

        /// <summary>
        /// Returns only private repositories
        /// </summary>
        [Parameter(Value = "private")]
        Private,

        /// <summary>
        /// Return both public and private repositories
        /// </summary>
        [Parameter(Value = "all")]
        All,

        /// <summary>
        /// Returns only internal repositories
        /// </summary>
        [Parameter(Value = "internal")]
        Internal,
    }

    /// <summary>
    /// The properties that repositories can be affiliated by.
    /// </summary>
    public enum RepositoryAffiliation
    {
        /// <summary>
        /// Repositories that are owned by the authenticated user
        /// </summary>
        [Parameter(Value = "owner")]
        Owner,

        /// <summary>
        /// Repositories that the user has been added to as a collaborator.
        /// </summary>
        [Parameter(Value = "collaborator")]
        Collaborator,

        /// <summary>
        /// Repositories that the user has access to through being a member of an organization.
        /// This includes every repository on every team that the user is on.
        /// </summary>
        [Parameter(Value = "organization_member")]
        OrganizationMember,

        /// <summary>
        /// Return repositories that are owned by authenticated user and added to as a collaborator.
        /// </summary>
        [Parameter(Value = "owner, collaborator")]
        OwnerAndCollaborator,

        /// <summary>
        /// Return repositories that are owned by authenticated user or user is a organization member.
        /// </summary>
        [Parameter(Value = "owner, organization_member")]
        OwnerAndOrganizationMember,

        /// <summary>
        /// Return repositories that user has been added as collaborator or user is a organization member.
        /// </summary>
        [Parameter(Value = "collaborator, organization_member")]
        CollaboratorAndOrganizationMember,

        /// <summary>
        /// Returns all repositories where user is owner,collaborator or organization member.
        /// </summary>
        [Parameter(Value = "owner, collaborator, organization_member")]
        All
    }
}
