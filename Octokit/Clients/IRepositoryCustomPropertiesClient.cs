using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Custom Property Values API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/repos/custom-properties">Custom Properties API documentation</a> for more information.
    /// </remarks>
    public interface IRepositoryCustomPropertiesClient
    {
        /// <summary>
        /// Get all custom property values for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/repos/custom-properties#get-all-custom-property-values-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="repoName">The name of the repository.</param>
        [ExcludeFromPaginationApiOptionsConventionTest("Pagination not supported by GitHub API (tested 15/06/2024)")]
        Task<IReadOnlyList<CustomPropertyValue>> GetAll(string owner, string repoName);

        /// <summary>
        /// Create new or update existing custom property values for a repository. Using a value of null for a custom property will remove or 'unset' the property value from the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/repos/custom-properties#create-or-update-custom-property-values-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="propertyValues">The custom property values to create or update</param>
        Task CreateOrUpdate(string owner, string repoName, UpsertRepositoryCustomPropertyValues propertyValues);
    }
}
