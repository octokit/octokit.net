using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Dependency Submission API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/dependency-graph/dependency-submission">Dependency Submission API documentation</a> for more details.
    /// </remarks>
    public interface IDependencySubmissionClient
    {
        /// <summary>
        /// Creates a new dependency snapshot.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/dependency-graph/dependency-submission">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="snapshot">The dependency snapshot to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs</exception>
        /// <returns>A <see cref="DependencySnapshotSubmission"/> instance for the created snapshot</returns>
        Task<DependencySnapshotSubmission> Create(string owner, string name, NewDependencySnapshot snapshot);

        /// <summary>
        /// Creates a new dependency snapshot.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/dependency-graph/dependency-submission">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="snapshot">The dependency snapshot to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs</exception>
        /// <returns>A <see cref="DependencySnapshotSubmission"/> instance for the created snapshot</returns>
        Task<DependencySnapshotSubmission> Create(long repositoryId, NewDependencySnapshot snapshot);
    }
}
