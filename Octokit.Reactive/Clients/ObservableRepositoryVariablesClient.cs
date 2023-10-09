using Octokit.Reactive.Internal;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Text;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Repository Variables API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/actions/variables?apiVersion=2022-11-28">Repository Variables API documentation</a> for more details.
    /// </remarks>
    public class ObservableRepositoryVariablesClient : IObservableRepositoryVariablesClient
    {
        readonly IRepositoryVariablesClient _client;
        readonly IConnection _connection;

        public ObservableRepositoryVariablesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Repository.Actions.Variables;
            _connection = client.Connection;
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
        public IObservable<RepositoryVariablesCollection> GetAllOrganization(string owner, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));

            return _client.GetAll(owner, repoName).ToObservable();
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
        public IObservable<RepositoryVariablesCollection> GetAll(string owner, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));

            return _client.GetAll(owner, repoName).ToObservable();
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
        public IObservable<RepositoryVariable> Get(string owner, string repoName, string variableName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrEmptyString(variableName, nameof(variableName));

            return _client.Get(owner, repoName, variableName).ToObservable();
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
        public IObservable<RepositoryVariable> Create(string owner, string repoName, Variable newVariable)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrDefault(newVariable, nameof(newVariable));
            Ensure.ArgumentNotNullOrEmptyString(newVariable.Name, nameof(newVariable.Name));
            Ensure.ArgumentNotNullOrEmptyString(newVariable.Value, nameof(newVariable.Value));

            return _client.Create(owner, repoName, newVariable).ToObservable();
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
        public IObservable<RepositoryVariable> Update(string owner, string repoName, Variable variable)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrDefault(variable, nameof(variable));
            Ensure.ArgumentNotNullOrEmptyString(variable.Name, nameof(variable.Name));
            Ensure.ArgumentNotNullOrEmptyString(variable.Value, nameof(variable.Value));

            return _client.Update(owner, repoName, variable).ToObservable();
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
        public IObservable<Unit> Delete(string owner, string repoName, string variableName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrEmptyString(variableName, nameof(variableName));

            return _client.Delete(owner, repoName, variableName).ToObservable();
        }
    }
}
