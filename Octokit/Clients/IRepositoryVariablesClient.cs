using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Variables API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/actions/variables/">Repository Variables API documentation</a> for more details.
    /// </remarks>
    public interface IRepositoryVariablesClient
    {
        /// <summary>
        /// List the variables for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/actions/variables/#list-repository-variables">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositoryVariablesCollection"/> instance for the list of repository variables.</returns>
        Task<RepositoryVariablesCollection> GetAll (string owner, string repoName);

        /// <summary>
        /// Get a variable from a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/actions/variables/#get-a-repository-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="variableName">The name of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositoryVariable"/> instance for the repository variable.</returns>
        Task<RepositoryVariable> Get(string owner, string repoName, string variableName);

        /// <summary>
        /// Create or update a variable in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/actions/variables/#create-or-update-a-repository-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="variableName">The name of the variable</param>
        /// <param name="createVariable">The value of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositoryVariable"/> instance for the repository variable that was created or updated.</returns>
        Task<RepositoryVariable> Create(string owner, string repoName, string variableName, CreateRepositoryVariable createVariable);
        
        /// <summary>
        /// Create or update a variable in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/actions/variables/#create-or-update-a-repository-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="variableName">The name of the variable</param>
        /// <param name="updateVariable">The value of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositoryVariable"/> instance for the repository variable that was created or updated.</returns>
        Task<RepositoryVariable> Update(string owner, string repoName, string variableName, UpdateRepositoryVariable updateVariable);

        /// <summary>
        /// Delete a variable in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/actions/variables/#delete-a-repository-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="variableName">The name of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task Delete(string owner, string repoName, string variableName);
    }
}
