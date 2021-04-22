using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    public class RepositorySecretsClient : ApiClient, IRepositorySecretsClient
    {
        /// <summary>
        /// Initializes a new GitHub Repository Branches API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public RepositorySecretsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Get the public signing key to encrypt secrets for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#get-a-repository-public-key">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="SecretsPublicKey"/> instance for the repository public key.</returns>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/secrets/public-key")]
        public Task<SecretsPublicKey> GetPublicKey(string owner, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));

            var url = ApiUrls.RepositorySecretsPublicKey(owner, repoName);

            return ApiConnection.Get<SecretsPublicKey>(url);
        }

        /// <summary>
        /// List the secrets for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#list-repository-secrets">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositorySecretsCollection"/> instance for the list of repository secrets.</returns>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/secrets")]
        public Task<RepositorySecretsCollection> GetAll(string owner, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));

            var url = ApiUrls.RepositorySecrets(owner, repoName);

            return ApiConnection.Get<RepositorySecretsCollection>(url);
        }

        /// <summary>
        /// Get a secret from a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#get-a-repository-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="secretName">The name of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositorySecret"/> instance for the repository secret.</returns>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/secrets/{secretName}")]
        public Task<RepositorySecret> Get(string owner, string repoName, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            var url = ApiUrls.RepositorySecret(owner, repoName, secretName);

            return ApiConnection.Get<RepositorySecret>(url);
        }

        /// <summary>
        /// Create or update a secret in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#create-or-update-a-repository-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="secretName">The name of the secret</param>
        /// <param name="upsertSecret">The encrypted value and id of the encryption key</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositorySecret"/> instance for the repository secret that was created or updated.</returns>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/actions/secrets/{secretName}")]
        public async Task<RepositorySecret> CreateOrUpdate(string owner, string repoName, string secretName, UpsertRepositorySecret upsertSecret)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));
            Ensure.ArgumentNotNull(upsertSecret, nameof(upsertSecret));
            Ensure.ArgumentNotNullOrEmptyString(upsertSecret.EncryptedValue, nameof(upsertSecret.EncryptedValue));
            Ensure.ArgumentNotNullOrEmptyString(upsertSecret.KeyId, nameof(upsertSecret.KeyId));

            var url = ApiUrls.RepositorySecret(owner, repoName, secretName);

            await ApiConnection.Put<RepositorySecret>(url, upsertSecret);

            return await Get(owner, repoName, secretName);
        }

        /// <summary>
        /// Delete a secret in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#delete-a-repository-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repoName">The name of the repository</param>
        /// <param name="secretName">The name of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/actions/secrets/{secretName}")]
        public Task Delete(string owner, string repoName, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            var url = ApiUrls.RepositorySecret(owner, repoName, secretName);

            return ApiConnection.Delete(url);
        }
    }
}
