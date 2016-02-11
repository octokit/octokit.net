using System.Threading.Tasks;

namespace Octokit.Helpers
{
    /// <summary>
    /// Represents operations to simplify working with references
    /// </summary>
    public static class ReferenceExtensions
    {
        /// <summary>
        /// Creates a branch, based off the branch specified.
        /// </summary>
        /// <param name="referencesClient">The <see cref="IReferencesClient" /> this method extends</param>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="branchName">The new branch name</param>
        /// <param name="baseReference">The <see cref="Reference" /> to base the branch from</param>
        public static async Task<Reference> CreateBranch(this IReferencesClient referencesClient, string owner, string name, string branchName, Reference baseReference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branchName, "branchName");
            Ensure.ArgumentNotNull(baseReference, "baseReference");

            return await referencesClient.Create(owner, name, new NewReference("refs/heads/" + branchName, baseReference.Object.Sha));
        }

        /// <summary>
        /// Creates a branch, based off the master branch.
        /// </summary>
        /// <param name="referencesClient">The <see cref="IReferencesClient" /> this method extends</param>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="branchName">The new branch name</param>
        public static async Task<Reference> CreateBranch(this IReferencesClient referencesClient, string owner, string name, string branchName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branchName, "branchName");

            var baseBranch = await referencesClient.Get(owner, name, "heads/master");
            return await referencesClient.Create(owner, name, new NewReference("refs/heads/" + branchName, baseBranch.Object.Sha));
        }
    }
}
