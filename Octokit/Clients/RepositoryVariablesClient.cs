using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    public class RepositoryVariablesClient : ApiClient, IRepositoryVariablesClient
    {
        /// <summary>
        /// Initializes a new GitHub Repository Branches API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public RepositoryVariablesClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// List the organization variables for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/actions/variables?apiVersion=2022-11-28#list-repository-organization-variables">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositoryVariablesCollection"/> instance for the list of repository variables.</returns>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/organization-variables")]
        public Task<RepositoryVariablesCollection> GetAllOrganization(string owner, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));

            var url = ApiUrls.RepositoryOrganizationVariables(owner, repoName);

            return ApiConnection.Get<RepositoryVariablesCollection>(url);
        }

        /// <summary>
        /// List the variables for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/actions/variables?apiVersion=2022-11-28#list-repository-variables">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositoryVariablesCollection"/> instance for the list of repository variables.</returns>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/variables")]
        public Task<RepositoryVariablesCollection> GetAll(string owner, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));

            var url = ApiUrls.RepositoryVariables(owner, repoName);

            return ApiConnection.Get<RepositoryVariablesCollection>(url);
        }

        /// <summary>
        /// Get a variable from a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/actions/variables?apiVersion=2022-11-28#get-a-repository-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="variableName">The name of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositoryVariable"/> instance for the repository secret.</returns>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/variables/{variableName}")]
        public Task<RepositoryVariable> Get(string owner, string repoName, string variableName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrEmptyString(variableName, nameof(variableName));

            var url = ApiUrls.RepositoryVariable(owner, repoName, variableName);

            return ApiConnection.Get<RepositoryVariable>(url);
        }

        /// <summary>
        /// Create a variable in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/actions/variables?apiVersion=2022-11-28#create-a-repository-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="newVariable">The variable to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositoryVariable"/> instance for the repository variable that was created.</returns>
        [ManualRoute("POST", "/repos/{owner}/{repo}/actions/variables")]
        public async Task<RepositoryVariable> Create(string owner, string repoName, Variable newVariable)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrDefault(newVariable, nameof(newVariable));
            Ensure.ArgumentNotNullOrEmptyString(newVariable.Name, nameof(newVariable.Name));
            Ensure.ArgumentNotNullOrEmptyString(newVariable.Value, nameof(newVariable.Value));

            var url = ApiUrls.RepositoryVariables(owner, repoName);

            await ApiConnection.Post<RepositoryVariable>(url, newVariable);

            return await Get(owner, repoName, newVariable.Name);
        }

        /// <summary>
        /// Update a variable in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/actions/variables?apiVersion=2022-11-28#update-a-repository-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="variable">The variable to update</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositoryVariable"/> instance for the repository variable that was updated.</returns>
        [ManualRoute("PATCH", "/repos/{owner}/{repo}/actions/variables/{variable.Name}")]
        public async Task<RepositoryVariable> Update(string owner, string repoName, Variable variable)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrDefault(variable, nameof(variable));
            Ensure.ArgumentNotNullOrEmptyString(variable.Name, nameof(variable.Name));
            Ensure.ArgumentNotNullOrEmptyString(variable.Value, nameof(variable.Value));

            var url = ApiUrls.RepositoryVariable(owner, repoName, variable.Name);

            await ApiConnection.Patch<RepositoryVariable>(url, variable);

            return await Get(owner, repoName, variable.Name);
        }

        /// <summary>
        /// Delete a variable in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/actions/variables?apiVersion=2022-11-28#delete-a-repository-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="variableName">The name of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/actions/variables/{variableName}")]
        public Task Delete(string owner, string repoName, string variableName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrEmptyString(variableName, nameof(variableName));

            var url = ApiUrls.RepositoryVariable(owner, repoName, variableName);

            return ApiConnection.Delete(url);
        }
    }
}
