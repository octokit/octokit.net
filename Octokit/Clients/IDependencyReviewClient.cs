using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Dependency Review API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/dependency-graph/dependency-review">Git Dependency Review API documentation</a> for more information.
    /// </remarks>
    public interface IDependencyReviewClient
    {
        /// <summary>
        /// Gets all <see cref="DependencyDiff"/>s for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/dependency-graph/dependency-review">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The base revision</param>
        /// <param name="head">The head revision</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ExcludeFromPaginationApiOptionsConventionTest("Pagination not supported by GitHub API.")]
        Task<IReadOnlyList<DependencyDiff>> GetAll(string owner, string name, string @base, string head);

        /// <summary>
        /// Gets all <see cref="DependencyDiff"/>s for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/dependency-graph/dependency-review">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="base">The base revision</param>
        /// <param name="head">The head revision</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ExcludeFromPaginationApiOptionsConventionTest("Pagination not supported by GitHub API.")]
        Task<IReadOnlyList<DependencyDiff>> GetAll(long repositoryId, string @base, string head);
    }
}
