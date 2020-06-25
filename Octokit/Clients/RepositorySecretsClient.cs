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
        /// See the <a href="https://developer.github.com/v3/actions/secrets/#get-a-repository-public-key">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoName">The owner of the repository</param>
        /// <param name="owner">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositorySecretsPublicKey"/> instance for the repository public key.</returns>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/secrets/public-key")]
        public Task<RepositorySecretsPublicKey> GetPublicKey(string owner, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));

            var url = ApiUrls.RepositorySecretsPublicKey(owner, repoName);

            return ApiConnection.Get<RepositorySecretsPublicKey>(url);
        }

        /// <summary>
        /// List the secrets for a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/actions/secrets/#list-repository-secrets">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoName">The owner of the repository</param>
        /// <param name="owner">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IEnumerable{RepositorySecret}"/> instance for the list of repository secrets.</returns>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/secrets")]
        public Task<IReadOnlyList<RepositorySecret>> GetSecretsList(string owner, string repoName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));

            var url = ApiUrls.RepositorySecretsList(owner, repoName);

            return ApiConnection.GetAll<RepositorySecret>(url);
        }

        /// <summary>
        /// Get a secret from a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/actions/secrets/#get-a-repository-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoName">The owner of the repository</param>
        /// <param name="owner">The name of the repository</param>
        /// <param name="secretName">The name of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositorySecret"/> instance for the repository secret.</returns>
        [ManualRoute("GET", "/repos/{owner}/{repo}/actions/secrets/{secretName}")]
        public Task<RepositorySecret> GetSecret(string owner, string repoName, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            var url = ApiUrls.RepositorySecrets(owner, repoName, secretName);

            return ApiConnection.Get<RepositorySecret>(url);
        }

        /// <summary>
        /// Create or update a secret in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/actions/secrets/#create-or-update-a-repository-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoName">The owner of the repository</param>
        /// <param name="owner">The name of the repository</param>
        /// <param name="secretName">The name of the secret</param>
        /// <param name="encryptedSecretValue">The value of the secret. See the <a href="https://developer.github.com/v3/actions/secrets/#create-or-update-a-repository-secret">API documentation</a> for more information on how to encrypt the secret</param>
        /// /// <param name="encryptionKeyId">The id of the encryption key used to encrypt the secret. Get key and id from <see cref="GetPublicKey(string, string)"/> and use the <a href="https://developer.github.com/v3/actions/secrets/#create-or-update-a-repository-secret">API documentation</a> for more information on how to encrypt the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="RepositorySecret"/> instance for the repository secret that was created or updated.</returns>
        [ManualRoute("PUT", "/repos/{owner}/{repo}/actions/secrets/{secretName}")]
        public async Task<RepositorySecret> CreateOrUpdateSecret(string owner, string repoName, string secretName, string encryptedSecretValue, string encryptionKeyId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));
            Ensure.ArgumentNotNullOrEmptyString(encryptedSecretValue, nameof(encryptedSecretValue));
            Ensure.ArgumentNotNullOrEmptyString(encryptionKeyId, nameof(encryptionKeyId));

            var data = new UpsertRepositorySecret
            {
                EncryptedValue = encryptedSecretValue,
                EncryptionKeyId = encryptionKeyId
            };

            var url = ApiUrls.RepositorySecrets(owner, repoName, secretName);

            return await ApiConnection.Put<RepositorySecret>(url, data);
        }

        /// <summary>
        /// Delete a secret in a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/actions/secrets/#delete-a-repository-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoName">The owner of the repository</param>
        /// <param name="owner">The name of the repository</param>
        /// <param name="secretName">The name of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/actions/secrets/{secretName}")]
        public Task DeleteSecret(string owner, string repoName, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repoName, nameof(repoName));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            var url = ApiUrls.RepositorySecrets(owner, repoName, secretName);

            return ApiConnection.Delete(url);
        }
    }
}
