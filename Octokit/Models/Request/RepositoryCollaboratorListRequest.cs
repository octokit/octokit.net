using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to request and filter a list of repository collaborators
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryCollaboratorListRequest : RequestParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryCollaboratorListRequest" /> class.
        /// </summary>
        public RepositoryCollaboratorListRequest()
        {
            Affiliation = CollaboratorAffiliation.All; // Default in accordance with the documentation
        }

        /// <summary>
        /// Gets or sets the collaborator affiliation property.
        /// </summary>
        /// <value>
        /// The collaborator affiliation
        /// </value>
        public CollaboratorAffiliation Affiliation { get; set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Affiliation: {0}", Affiliation); }
        }
    }

    /// <summary>
    /// The collaborator affiliation
    /// </summary>
    public enum CollaboratorAffiliation
    {
        /// <summary>
        /// All collaborators the authenticated user can see.
        /// </summary>
        [Parameter(Value = "all")]
        All,
        /// <summary>
        /// All collaborators with permissions to an organization-owned repository,
        /// regardless of organization membership status.
        /// </summary>
        [Parameter(Value = "direct")]
        Direct,
        /// <summary>
        /// All outside collaborators of an organization-owned repository.
        /// </summary>
        [Parameter(Value = "outside")]
        Outside
    }
}
