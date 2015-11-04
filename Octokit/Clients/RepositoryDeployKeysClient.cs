using System;
using System.Threading.Tasks;
#if NET_45
using System.Collections.Generic;
#endif

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
        public Task<DeployKey> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<DeployKey>(ApiUrls.RepositoryDeployKey(owner, name, number));
        }

        /// <summary>
        /// Get all deploy keys for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/keys/#list"> API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        public Task<IReadOnlyList<DeployKey>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<DeployKey>(ApiUrls.RepositoryDeployKeys(owner, name));
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
        /// <returns></returns>
        public Task<DeployKey> Create(string owner, string name, NewDeployKey newDeployKey)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newDeployKey, "newDeployKey");

            if (string.IsNullOrWhiteSpace(newDeployKey.Title))
                throw new ArgumentException("The new deploy key's title must not be null.");

            if (string.IsNullOrWhiteSpace(newDeployKey.Key))
                throw new ArgumentException("The new deploy key's key must not be null.");

            return ApiConnection.Post<DeployKey>(ApiUrls.RepositoryDeployKeys(owner, name), newDeployKey);
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
        /// <returns></returns>
        public Task Delete(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(number, "number");

            return ApiConnection.Delete(ApiUrls.RepositoryDeployKey(owner, name, number));
        }
    }
}
