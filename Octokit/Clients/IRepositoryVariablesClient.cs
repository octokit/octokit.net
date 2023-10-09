using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Variables API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/actions/variables?apiVersion=2022-11-28">Repository Variables API documentation</a> for more details.
    /// </remarks>
    public interface IRepositoryVariablesClient
    {
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
        Task<RepositoryVariablesCollection> GetAllOrganization(string owner, string repoName);

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
        Task<RepositoryVariablesCollection> GetAll(string owner, string repoName);

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
        Task<RepositoryVariable> Get(string owner, string repoName, string variableName);

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
        Task<RepositoryVariable> Create(string owner, string repoName, Variable newVariable);

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
        Task<RepositoryVariable> Update(string owner, string repoName, Variable variable);

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
        Task Delete(string owner, string repoName, string variableName);
    }
}
