using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Dependency Review API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/dependency-graph/dependency-review">Git Dependency Review API documentation</a> for more information.
    /// </remarks>
    public class DependencyReviewClient : ApiClient, IDependencyReviewClient
    {
        /// <summary>
        /// Instantiate a new GitHub Dependency Review API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public DependencyReviewClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

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
        [ManualRoute("GET", "/repos/{owner}/{repo}/dependency-graph/compare/{base}...{head}")]
        public Task<IReadOnlyList<DependencyDiff>> GetAll(string owner, string name, string @base, string head)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(@base, nameof(@base));
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));

            return ApiConnection.GetAll<DependencyDiff>(ApiUrls.DependencyReview(owner, name, @base, head));
        }

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
        [ManualRoute("GET", "/repositories/{id}/dependency-graph/compare/{base}...{head}")]
        public Task<IReadOnlyList<DependencyDiff>> GetAll(long repositoryId, string @base, string head)
        {
            Ensure.ArgumentNotNullOrEmptyString(@base, nameof(@base));
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));

            return ApiConnection.GetAll<DependencyDiff>(ApiUrls.DependencyReview(repositoryId, @base, head));
        }
    }
}
