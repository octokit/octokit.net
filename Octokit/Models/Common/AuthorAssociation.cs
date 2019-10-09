using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// States of a Team/Organization Membership
    /// </summary>
    public enum AuthorAssociation
    {
        /// <summary>
        /// Author has been invited to collaborate on the repository.
        /// </summary>
        [Parameter(Value = "COLLABORATOR")]
        Collaborator,

        /// <summary>
        /// Author has previously committed to the repository.
        /// </summary>
        [Parameter(Value = "CONTRIBUTOR")]
        Contributor,

        /// <summary>
        /// Author has not previously committed to GitHub.
        /// </summary>
        [Parameter(Value = "FIRST_TIMER")]
        FirstTimer,

        /// <summary>
        /// Author has not previously committed to the repository.
        /// </summary>
        [Parameter(Value = "FIRST_TIME_CONTRIBUTOR")]
        FirstTimeContributor,

        /// <summary>
        /// Author is a member of the organization that owns the repository.
        /// </summary>
        [Parameter(Value = "MEMBER")]
        Member,

        /// <summary>
        /// Author is the owner of the repository. 
        /// </summary>
        [Parameter(Value = "OWNER")]
        Owner,

        /// <summary>
        /// Author has no association with the repository.
        /// </summary>
        [Parameter(Value = "NONE")]
        None
    }
}
