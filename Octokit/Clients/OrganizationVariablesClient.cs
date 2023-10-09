using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Organization Variables API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://docs.github.com/v3/actions#variables/">Organization Variables API documentation</a> for more details.
    /// </remarks>
    public class OrganizationVariablesClient : ApiClient, IOrganizationVariablesClient
    {
        public OrganizationVariablesClient(IApiConnection apiConnection) : base(apiConnection) { }

        /// <summary>
        /// List the variables for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#list-organization-variables">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="OrganizationVariablesCollection"/> instance for the list of organization variables.</returns>
        [ManualRoute("GET", "/orgs/{org}/actions/variables")]
        public Task<OrganizationVariablesCollection> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return ApiConnection.Get<OrganizationVariablesCollection>(ApiUrls.OrganizationRepositoryVariables(org));
        }

        /// <summary>
        /// Get a variable from an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#get-an-organization-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="variableName">The name of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="OrganizationVariable"/> instance for the organization variable.</returns>
        [ManualRoute("GET", "/orgs/{org}/actions/variables/{variableName}")]
        public Task<OrganizationVariable> Get(string org, string variableName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(variableName, nameof(variableName));

            return ApiConnection.Get<OrganizationVariable>(ApiUrls.OrganizationRepositoryVariable(org, variableName));
        }

        /// <summary>
        /// Create or update a variable in an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#create-or-update-an-organization-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="variableName">The name of the variable</param>
        /// <param name="createVariable">The value and visibility info to upsert</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="OrganizationVariable"/> instance for the organization variable that was created or updated.</returns>
        [ManualRoute("PUT", "/orgs/{org}/actions/variables/{variableName}")]
        public async Task<OrganizationVariable> Create(string org, string variableName, CreateOrganizationVariable createVariable)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(variableName, nameof(variableName));
            Ensure.ArgumentNotNull(createVariable, nameof(createVariable));
            Ensure.ArgumentNotNullOrEmptyString(createVariable.Value, nameof(createVariable.Value));
            Ensure.ArgumentNotNullOrEmptyString(createVariable.Visibility, nameof(createVariable.Visibility));

            await ApiConnection.Post<OrganizationVariable>(ApiUrls.OrganizationRepositoryVariable(org, variableName), createVariable);
            return await ApiConnection.Get<OrganizationVariable>(ApiUrls.OrganizationRepositoryVariable(org, variableName));
        }

        /// <summary>
        /// Create or update a variable in an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#create-or-update-an-organization-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="variableName">The name of the variable</param>
        /// <param name="updateVariable">The value and visibility info to upsert</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="OrganizationVariable"/> instance for the organization variable that was created or updated.</returns>
        [ManualRoute("PUT", "/orgs/{org}/actions/variables/{variableName}")]
        public async Task<OrganizationVariable> Update(string org, string variableName, UpdateOrganizationVariable updateVariable)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(variableName, nameof(variableName));
            Ensure.ArgumentNotNull(updateVariable, nameof(updateVariable));
            Ensure.ArgumentNotNullOrEmptyString(updateVariable.Value, nameof(updateVariable.Value));
            Ensure.ArgumentNotNullOrEmptyString(updateVariable.Visibility, nameof(updateVariable.Visibility));

            await ApiConnection.Patch<OrganizationVariable>(ApiUrls.OrganizationRepositoryVariable(org, variableName), updateVariable);
            return await ApiConnection.Get<OrganizationVariable>(ApiUrls.OrganizationRepositoryVariable(org, variableName));
        }

        /// <summary>
        /// Delete a variable in an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#delete-an-organization-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="variableName">The name of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("DELETE", "/orgs/{org}/actions/variables/{variableName}")]
        public Task Delete(string org, string variableName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(variableName, nameof(variableName));

            return ApiConnection.Delete(ApiUrls.OrganizationRepositoryVariable(org, variableName));
        }

        /// <summary>
        /// Get the list of selected sites that have access to a variable.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#list-selected-repositories-for-an-organization-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="variableName">The name of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/orgs/{org}/actions/variables/{variableName}/repositories")]
        public Task<OrganizationVariableRepositoryCollection> GetSelectedRepositoriesForVariable(string org, string variableName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(variableName, nameof(variableName));

            return ApiConnection.Get<OrganizationVariableRepositoryCollection>(ApiUrls.OrganizationRepositoryVariableRepositories(org, variableName));
        }

        /// <summary>
        /// Set the list of selected sites that have access to a variable.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#set-selected-repositories-for-an-organization-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="variableName">The name of the variable</param>
        /// <param name="repositories">The list of repositories that should have access to view and use the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("PUT", "/orgs/{org}/actions/variables/{variableName}/repositories")]
        public async Task SetSelectedRepositoriesForVariable(string org, string variableName, SelectedRepositoryCollection repositories)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(variableName, nameof(variableName));
            Ensure.ArgumentNotNull(repositories, nameof(repositories));
            Ensure.ArgumentNotNull(repositories.SelectedRepositoryIds, nameof(repositories.SelectedRepositoryIds));

            await ApiConnection.Put<SelectedRepositoryCollection>(ApiUrls.OrganizationRepositoryVariableRepositories(org, variableName), repositories);
            return;
        }

        /// <summary>
        /// Add a selected site to the visibility list of a variable.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#add-selected-repository-to-an-organization-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="variableName">The name of the variable</param>
        /// <param name="repoId">The id of the repo to add to the visibility list of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("PUT", "/orgs/{org}/actions/variables/{variableName}/repositories/{repoId}")]
        public Task AddRepoToOrganizationVariable(string org, string variableName, long repoId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(variableName, nameof(variableName));
            Ensure.ArgumentNotNull(repoId, nameof(repoId));

            return ApiConnection.Put(ApiUrls.OrganizationRepositoryVariableRepository(org, variableName, repoId));
        }

        /// <summary>
        /// ARemoved a selected site from the visibility list of a variable.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#remove-selected-repository-from-an-organization-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="variableName">The name of the variable</param>
        /// <param name="repoId">The id of the repo to add to the visibility list of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("DELETE", "/orgs/{org}/actions/variables/{variableName}/repositories/{repoId}")]
        public Task RemoveRepoFromOrganizationVariable(string org, string variableName, long repoId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(variableName, nameof(variableName));
            Ensure.ArgumentNotNull(repoId, nameof(repoId));

            return ApiConnection.Delete(ApiUrls.OrganizationRepositoryVariableRepository(org, variableName, repoId));
        }
    }
}
