using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    public class EnvironmentVariablesClient : ApiClient, IEnvironmentVariablesClient
    {
        /// <summary>
        /// Initializes a new GitHub Repository Branches API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public EnvironmentVariablesClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

		/// <summary>
		/// List the variables for an environment.
		/// </summary>
		/// <remarks>
		/// See the <a href="https://docs.github.com/en/rest/actions/variables?apiVersion=2022-11-28#list-repository-variables">API documentation</a> for more information.
		/// </remarks>
		/// <param name="repoId">The unique identifier of the repository for the environment.</param>
		/// <param name="envName">The name of the environment. The name must be URL encoded. For example, any slashes in the name must be replaced with %2F</param>
		/// <param name="perPage">Optional number of pages to return.  Default is 30, per GitHub</param>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns>A <see cref="EnvironmentVariablesCollection"/> instance for the list of environment variables.</returns>
		[ManualRoute("GET", "repositories/{repoId}/environments/{envName}/variables")]
        public Task<EnvironmentVariablesCollection> GetAll(int repoId, string envName, int? perPage = 30)
		{
			Ensure.ArgumentNotNull(repoId, nameof(repoId));
			Ensure.ArgumentNotNullOrEmptyString(envName, nameof(envName));

			var url = ApiUrls.EnvironmentVariables(repoId, envName, perPage);

			return ApiConnection.Get<EnvironmentVariablesCollection>(url);
        }

		/// <summary>
		/// Get a variable from an environment.
		/// </summary>
		/// <remarks>
		/// See the <a href="https://docs.github.com/en/rest/actions/variables?apiVersion=2022-11-28#get-an-environment-variable">API documentation</a> for more information.
		/// </remarks>
		/// <param name="repoId">The unique identifier of the repository for the environment.</param>
		/// <param name="envName">The name of the environment. The name must be URL encoded. For example, any slashes in the name must be replaced with %2F</param>
		/// <param name="variableName">The name of the variable</param>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns>A <see cref="EnvironmentVariable"/> instance for the environment variable.</returns>
		[ManualRoute("GET", "/repositories/{repoId}/environments/{envName}/variables/{variableName}")]
        public Task<EnvironmentVariable> Get(int repoId, string envName, string variableName)
        {
			Ensure.ArgumentNotNull(repoId, nameof(repoId));
			Ensure.ArgumentNotNullOrEmptyString(envName, nameof(envName));
			Ensure.ArgumentNotNullOrEmptyString(variableName, nameof(variableName));

            var url = ApiUrls.EnvironmentVariable(repoId, envName, variableName);

            return ApiConnection.Get<EnvironmentVariable>(url);
        }

		/// <summary>
		/// Create a variable in an environment.
		/// </summary>
		/// <remarks>
		/// See the <a href="https://docs.github.com/en/rest/actions/variables?apiVersion=2022-11-28#create-an-environment-variable">API documentation</a> for more information.
		/// </remarks>
		/// <param name="repoId">The unique identifier of the repository for the environment.</param>
		/// <param name="envName">The name of the environment. The name must be URL encoded. For example, any slashes in the name must be replaced with %2F</param>
		/// <param name="newVariable">The variable to create</param>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns>A <see cref="EnvironmentVariable"/> instance for the environment variable that was created.</returns>
		[ManualRoute("POST", "/repositories/{repoId}/environments/{envName}/variables")]
        public async Task<EnvironmentVariable> Create(int repoId, string envName, Variable newVariable)
        {
			Ensure.ArgumentNotNull(repoId, nameof(repoId));
			Ensure.ArgumentNotNullOrEmptyString(envName, nameof(envName));
			Ensure.ArgumentNotNullOrDefault(newVariable, nameof(newVariable));
            Ensure.ArgumentNotNullOrEmptyString(newVariable.Name, nameof(newVariable.Name));
            Ensure.ArgumentNotNullOrEmptyString(newVariable.Value, nameof(newVariable.Value));

            var url = ApiUrls.EnvironmentVariables(repoId, envName);

            await ApiConnection.Post<EnvironmentVariable>(url, newVariable);

            return await Get(repoId, envName, newVariable.Name);
        }

		/// <summary>
		/// Update a variable in an environment.
		/// </summary>
		/// <remarks>
		/// See the <a href="https://docs.github.com/en/rest/actions/variables?apiVersion=2022-11-28#update-an-environment-variable">API documentation</a> for more information.
		/// </remarks>
		/// <param name="repoId">The unique identifier of the repository for the environment.</param>
		/// <param name="envName">The name of the environment. The name must be URL encoded. For example, any slashes in the name must be replaced with %2F</param>
		/// <param name="variable">The variable to update</param>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns>A <see cref="EnvironmentVariable"/> instance for the environment variable that was updated.</returns>
		[ManualRoute("PATCH", "/repositories/{repoId}/environments/{envName}/variables/{variable.Name}")]
        public async Task<EnvironmentVariable> Update(int repoId, string envName, Variable variable)
        {
			Ensure.ArgumentNotNull(repoId, nameof(repoId));
			Ensure.ArgumentNotNullOrEmptyString(envName, nameof(envName));
			Ensure.ArgumentNotNullOrDefault(variable, nameof(variable));
            Ensure.ArgumentNotNullOrEmptyString(variable.Name, nameof(variable.Name));
            Ensure.ArgumentNotNullOrEmptyString(variable.Value, nameof(variable.Value));

            var url = ApiUrls.EnvironmentVariable(repoId, envName, variable.Name);

            await ApiConnection.Patch<EnvironmentVariable>(url, variable);

            return await Get(repoId, envName, variable.Name);
        }

		/// <summary>
		/// Delete a variable in an environment.
		/// </summary>
		/// <remarks>
		/// See the <a href="https://docs.github.com/en/rest/actions/variables?apiVersion=2022-11-28#delete-an-environment-variable">API documentation</a> for more information.
		/// </remarks>
		/// <param name="repoId">The unique identifier of the repository for the environment.</param>
		/// <param name="envName">The name of the environment. The name must be URL encoded. For example, any slashes in the name must be replaced with %2F</param>
		/// <param name="variableName">The name of the variable</param>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		[ManualRoute("DELETE", "/repositories/{repoId}/environments/{envName}/variables/{variableName}")]
        public Task Delete(int repoId, string envName, string variableName)
        {
			Ensure.ArgumentNotNull(repoId, nameof(repoId));
			Ensure.ArgumentNotNullOrEmptyString(envName, nameof(envName));
			Ensure.ArgumentNotNullOrEmptyString(variableName, nameof(variableName));

            var url = ApiUrls.EnvironmentVariable(repoId, envName, variableName);

            return ApiConnection.Delete(url);
        }
    }
}
