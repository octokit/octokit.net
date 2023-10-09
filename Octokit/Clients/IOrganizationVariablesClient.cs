using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Organization Variables API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/reference/actions#variables">Organization Variables API documentation</a> for more details.
    /// </remarks>
    public interface IOrganizationVariablesClient
    {
        /// <summary>
        /// List the variables for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#list-organization-variables">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="OrganizationVariablesCollection"/> instance for the list of organization variables.</returns>
        Task<OrganizationVariablesCollection> GetAll(string org);

        /// <summary>
        /// Get a variable from an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#get-an-organization-variables">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="variableName">The name of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="OrganizationVariable"/> instance for the organization variable.</returns>
        Task<OrganizationVariable> Get(string org, string variableName);

        /// <summary>
        /// Create or update a variable in an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#create-or-update-an-organization-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="variableName">The name of the variable</param>
        /// <param name="createVariable">The value and visibility info to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="OrganizationVariable"/> instance for the organization variable that was created or updated.</returns>
        Task<OrganizationVariable> Create(string org, string variableName, CreateOrganizationVariable createVariable);
        
        /// <summary>
        /// Create or update a variable in an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#create-or-update-an-organization-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="variableName">The name of the variable</param>
        /// <param name="updateVariable">The value and visibility info to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="OrganizationVariable"/> instance for the organization variable that was created or updated.</returns>
        Task<OrganizationVariable> Update(string org, string variableName, UpdateOrganizationVariable updateVariable);

        /// <summary>
        /// Delete a variable in an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#delete-an-organization-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="variableName">The name of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task Delete(string org, string variableName);

        /// <summary>
        /// Get the list of selected sites that have access to a variable.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#list-selected-repositories-for-an-organization-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="variableName">The name of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<OrganizationVariableRepositoryCollection> GetSelectedRepositoriesForVariable(string org, string variableName);

        /// <summary>
        /// Set the list of selected sites that have access to a variable.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#set-selected-repositories-for-an-organization-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="variableName">The name of the variable</param>
        /// <param name="repositories">The list of repositories that should have access to view and use the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task SetSelectedRepositoriesForVariable(string org, string variableName, SelectedRepositoryCollection repositories);

        /// <summary>
        /// Add a selected site to the visibility list of a variable.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#add-selected-repository-to-an-organization-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="variableName">The name of the variable</param>
        /// <param name="repoId">The id of the repo to add to the visibility list of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task AddRepoToOrganizationVariable(string org, string variableName, long repoId);

        /// <summary>
        /// ARemoved a selected site from the visibility list of a variable.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#remove-selected-repository-from-an-organization-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="variableName">The name of the variable</param>
        /// <param name="repoId">The id of the repo to add to the visibility list of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task RemoveRepoFromOrganizationVariable(string org, string variableName, long repoId);
    }
}
