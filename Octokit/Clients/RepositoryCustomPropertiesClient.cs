using System.Threading.Tasks;
using System.Collections.Generic;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Custom Property Values API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/repos/custom-properties">Custom Properties API documentation</a> for more information.
    /// </remarks>
    public class RepositoryCustomPropertiesClient : ApiClient, IRepositoryCustomPropertiesClient
    {
        /// <summary>
        /// Initializes a new GitHub repository custom property values API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public RepositoryCustomPropertiesClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Get all custom property values for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/repos/custom-properties#get-all-custom-property-values-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="repoName">The name of the repository.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/repos/{owner}/{repo}/properties/values")]
        public Task<IReadOnlyList<CustomPropertyValue>> GetAll(string owner, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));

            var url = ApiUrls.RepositoryCustomPropertyValues(owner, repoName);

            return ApiConnection.Get<IReadOnlyList<CustomPropertyValue>>(url, null);
        }

        /// <summary>
        /// Create new or update existing custom property values for a repository. Using a value of null for a custom property will remove or 'unset' the property value from the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/repos/custom-properties#create-or-update-custom-property-values-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="propertyValues">The custom property values to create or update</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("PATCH", "/repos/{owner}/{repo}/properties/values")]
        public Task CreateOrUpdate(string owner, string repoName, UpsertRepositoryCustomPropertyValues propertyValues)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNull(propertyValues, nameof(propertyValues));
            Ensure.ArgumentNotNullOrEmptyEnumerable(propertyValues.Properties, nameof(propertyValues.Properties));

            var url = ApiUrls.RepositoryCustomPropertyValues(owner, repoName);

            return ApiConnection.Patch(url, propertyValues);
        }
    }
}
