using System.Threading.Tasks;

namespace Octokit
{
    public class RepositoryVariablesClient : ApiClient, IRepositoryVariablesClient
    {
        /// <summary>
        /// Initializes a new GitHub Repository Variables API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public RepositoryVariablesClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// List the variables for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#list-repository-variables">API documentation</a> for more information.
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
        /// See the <a href="https://docs.github.com/v3/actions#get-a-repository-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="variableName">The name of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositoryVariable"/> instance for the repository variable.</returns>
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
        /// Create or update a variable in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#create-or-update-a-repository-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="variableName">The name of the variable</param>
        /// <param name="createVariable">The value and id of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositoryVariable"/> instance for the repository variable that was created or updated.</returns>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/actions/variables/{variableName}")]
        public async Task<RepositoryVariable> Create(string owner, string repoName, string variableName, CreateRepositoryVariable createVariable)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrEmptyString(variableName, nameof(variableName));
            Ensure.ArgumentNotNull(createVariable, nameof(createVariable));
            Ensure.ArgumentNotNullOrEmptyString(createVariable.Value, nameof(createVariable.Value));

            var url = ApiUrls.RepositoryVariable(owner, repoName, variableName);

            await ApiConnection.Put<RepositoryVariable>(url, createVariable);

            return await Get(owner, repoName, variableName);
        }

        /// <summary>
        /// Create or update a variable in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#create-or-update-a-repository-variable">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="variableName">The name of the variable</param>
        /// <param name="updateVariable">The value and id of the variable</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositoryVariable"/> instance for the repository variable that was created or updated.</returns>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/actions/variables/{variableName}")]
        public async Task<RepositoryVariable> Update(string owner, string repoName, string variableName, UpdateRepositoryVariable updateVariable)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrEmptyString(variableName, nameof(variableName));
            Ensure.ArgumentNotNull(updateVariable, nameof(updateVariable));
            Ensure.ArgumentNotNullOrEmptyString(updateVariable.Value, nameof(updateVariable.Value));

            var url = ApiUrls.RepositoryVariable(owner, repoName, variableName);

            await ApiConnection.Put<RepositoryVariable>(url, updateVariable);

            return await Get(owner, repoName, variableName);
        }

        /// <summary>
        /// Delete a variable in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#delete-a-repository-variable">API documentation</a> for more information.
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
