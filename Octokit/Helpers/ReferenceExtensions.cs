using System;
using System.Globalization;
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
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branchName, nameof(branchName));
            Ensure.ArgumentNotNull(baseReference, nameof(baseReference));

            if (branchName.StartsWith("refs/heads"))
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The specified branch name '{0}' appears to be a ref name and not a branch name because it starts with the string 'refs/heads'. Either specify just the branch name or use the Create method if you need to specify the full ref name", branchName), "branchName");
            }

            var newReference = new NewReference("refs/heads/" + branchName, baseReference.Object.Sha);
            return await referencesClient.Create(owner, name, newReference).ConfigureAwait(false);
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
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branchName, nameof(branchName));

            if (branchName.StartsWith("refs/heads"))
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The specified branch name '{0}' appears to be a ref name and not a branch name because it starts with the string 'refs/heads'. Either specify just the branch name or use the Create method if you need to specify the full ref name", branchName), "branchName");
            }

            var baseBranch = await referencesClient.Get(owner, name, "heads/master").ConfigureAwait(false);
            var newReference = new NewReference("refs/heads/" + branchName, baseBranch.Object.Sha);
            return await referencesClient.Create(owner, name, newReference).ConfigureAwait(false);
        }
    }
}
