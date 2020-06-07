using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Deploy Keys API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/keys/">Deploy Keys API documentation</a> for more information.
    /// </remarks>
    public class RepositoryDeployKeysClient : ApiClient, IRepositoryDeployKeysClient
    {
        /// <summary>
        /// Instantiates a new GitHub repository deploy keys API client.
        /// </summary>
        /// <param name="apiConnection">The API connection.</param>
        public RepositoryDeployKeysClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Get a single deploy key by number for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#get"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="number">The id of the deploy key.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/keys/{number}")]
        public Task<DeployKey> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<DeployKey>(ApiUrls.RepositoryDeployKey(owner, name, number));
        }

        /// <summary>
        /// Get a single deploy key by number for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#get"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="number">The id of the deploy key.</param>
        [ManualRoute("GET", "/repositories/{id}/keys/{number}")]
        public Task<DeployKey> Get(long repositoryId, int number)
        {
            return ApiConnection.Get<DeployKey>(ApiUrls.RepositoryDeployKey(repositoryId, number));
        }

        /// <summary>
        /// Get all deploy keys for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#list"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/keys")]
        public Task<IReadOnlyList<DeployKey>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Get all deploy keys for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#list"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        [ManualRoute("GET", "/repositories/{id}/keys")]
        public Task<IReadOnlyList<DeployKey>> GetAll(long repositoryId)
        {
            return GetAll(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Get all deploy keys for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#list"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/keys")]
        public Task<IReadOnlyList<DeployKey>> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<DeployKey>(ApiUrls.RepositoryDeployKeys(owner, name), options);
        }

        /// <summary>
        /// Get all deploy keys for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#list"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/keys")]
        public Task<IReadOnlyList<DeployKey>> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<DeployKey>(ApiUrls.RepositoryDeployKeys(repositoryId), options);
        }

        /// <summary>
        /// Creates a new deploy key for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#create"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="newDeployKey">The deploy key to create for the repository.</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/keys")]
        public Task<DeployKey> Create(string owner, string name, NewDeployKey newDeployKey)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newDeployKey, nameof(newDeployKey));

            if (string.IsNullOrWhiteSpace(newDeployKey.Title))
                throw new ArgumentException("The new deploy key's title must not be null.");

            if (string.IsNullOrWhiteSpace(newDeployKey.Key))
                throw new ArgumentException("The new deploy key's key must not be null.");

            return ApiConnection.Post<DeployKey>(ApiUrls.RepositoryDeployKeys(owner, name), newDeployKey);
        }

        /// <summary>
        /// Creates a new deploy key for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#create"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="newDeployKey">The deploy key to create for the repository.</param>
        [ManualRoute("POST", "/repositories/{id}/keys")]
        public Task<DeployKey> Create(long repositoryId, NewDeployKey newDeployKey)
        {
            Ensure.ArgumentNotNull(newDeployKey, nameof(newDeployKey));

            if (string.IsNullOrWhiteSpace(newDeployKey.Title))
                throw new ArgumentException("The new deploy key's title must not be null.");

            if (string.IsNullOrWhiteSpace(newDeployKey.Key))
                throw new ArgumentException("The new deploy key's key must not be null.");

            return ApiConnection.Post<DeployKey>(ApiUrls.RepositoryDeployKeys(repositoryId), newDeployKey);
        }

        /// <summary>
        /// Deletes a deploy key from a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#delete"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="number">The id of the deploy key to delete.</param>
        [ManualRoute("DELETE", "/repositories/{id}/keys/{number}")]
        public Task Delete(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Delete(ApiUrls.RepositoryDeployKey(owner, name, number));
        }

        /// <summary>
        /// Deletes a deploy key from a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#delete"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository.</param>
        /// <param name="number">The id of the deploy key to delete.</param>
        [ManualRoute("DELETE", "/repositories/{id}/keys/{number}")]
        public Task Delete(long repositoryId, int number)
        {
            return ApiConnection.Delete(ApiUrls.RepositoryDeployKey(repositoryId, number));
        }
    }
}
