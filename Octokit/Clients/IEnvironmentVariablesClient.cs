using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Environment Variables API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/actions/variables?apiVersion=2022-11-28">Environment Variables API documentation</a> for more details.
    /// </remarks>
    public interface IEnvironmentVariablesClient
    {
		/// <summary>
		/// List the variables for an environment.
		/// </summary>
		/// <remarks>
		/// See the <a href="https://docs.github.com/en/rest/actions/variables?apiVersion=2022-11-28#list-environment-variables">API documentation</a> for more information.
		/// </remarks>
		/// <param name="repoId">The unique identifier of the repository for the environment.</param>
		/// <param name="envName">The name of the environment. The name must be URL encoded. For example, any slashes in the name must be replaced with %2F</param>
		/// <param name="perPage">Optional number of pages to return.  Default is 30, per GitHub</param>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns>A <see cref="EnvironmentVariablesCollection"/> instance for the list of environment variables.</returns>
		Task<EnvironmentVariablesCollection> GetAll(int repoId, string envName, int? perPage = 30);

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
		Task<EnvironmentVariable> Get(int repoId, string envName, string variableName);

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
		Task<EnvironmentVariable> Create(int repoId, string envName, Variable newVariable);

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
		Task<EnvironmentVariable> Update(int repoId, string envName, Variable variable);

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
		Task Delete(int repoId, string envName, string variableName);
    }
}
