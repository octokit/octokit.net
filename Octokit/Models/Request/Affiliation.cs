using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to filter collaborators returned by their affiliation.
    /// </summary>
    public enum Affiliation
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
