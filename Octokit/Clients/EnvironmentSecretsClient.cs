using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    public class EnvironmentSecretsClient : ApiClient, IEnvironmentSecretsClient
    {
        /// <summary>
        /// Initializes a new GitHub Repository Environment API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public EnvironmentSecretsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

		/// <summary>
		/// Get the public signing key to encrypt secrets for a repository environment (environment).
		/// </summary>
		/// <remarks>
		/// See the <a href="https://docs.github.com/rest/actions/secrets#get-an-environment-public-key">API documentation</a> for more information.
		/// </remarks>
		/// <param name="repoId">The unique identifier of the repository for the environment.</param>
		/// <param name="envName">The name of the environment. The name must be URL encoded. For example, any slashes in the name must be replaced with %2F</param>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns>A <see cref="SecretsPublicKey"/> instance for the environment public key.</returns>
		[ManualRoute("GET", "repositories/{repoId}/environments/{envName}/secrets/public-key")]
        public Task<SecretsPublicKey> GetPublicKey(int repoId, string envName)
        {
            Ensure.ArgumentNotNull(repoId, nameof(repoId));
            Ensure.ArgumentNotNullOrEmptyString(envName, nameof(envName));

            var url = ApiUrls.EnvironmentSecretsPublicKey(repoId, envName);

            return ApiConnection.Get<SecretsPublicKey>(url);
        }

		/// <summary>
		/// List the secrets for an environment.
		/// </summary>
		/// <remarks>
		/// See the <a href="https://docs.github.com/rest/actions/secrets#list-environment-secrets">API documentation</a> for more information.
		/// </remarks>
		/// <param name="repoId">The unique identifier of the repository for the environment.</param>
		/// <param name="envName">The name of the environment. The name must be URL encoded. For example, any slashes in the name must be replaced with %2F</param>
		/// <param name="perPage">Optional number of pages to return.  Default is 30, per GitHub</param>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns>A <see cref="EnvironmentSecretsCollection"/> instance for the list of environment secrets.</returns>
		[ManualRoute("GET", "repositories/{repoId}/environments/{envName}/secrets")]
        public Task<EnvironmentSecretsCollection> GetAll(int repoId, string envName, int? perPage = 30)
        {
			Ensure.ArgumentNotNull(repoId, nameof(repoId));
			Ensure.ArgumentNotNullOrEmptyString(envName, nameof(envName));

			var url = ApiUrls.EnvironmentSecrets(repoId, envName, perPage);

            return ApiConnection.Get<EnvironmentSecretsCollection>(url);
        }

		/// <summary>
		/// Get a secret from an environment.
		/// </summary>
		/// <remarks>
		/// See the <a href="https://docs.github.com/rest/actions/secrets/#get-an-environment-secret">API documentation</a> for more information.
		/// </remarks>
		/// <param name="repoId">The unique identifier of the repository for the environment.</param>
		/// <param name="envName">The name of the environment. The name must be URL encoded. For example, any slashes in the name must be replaced with %2F</param>
		/// <param name="secretName">The name of the secret</param>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns>A <see cref="EnvironmentSecret"/> instance for the environment secret.</returns>
		[ManualRoute("GET", "repositories/{repoId}/environments/{envName}/secrets/{secretName}")]
        public Task<EnvironmentSecret> Get(int repoId, string envName, string secretName)
        {
			Ensure.ArgumentNotNull(repoId, nameof(repoId));
			Ensure.ArgumentNotNullOrEmptyString(envName, nameof(envName));
			Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

			var url = ApiUrls.EnvironmentSecret(repoId, envName, secretName);

            return ApiConnection.Get<EnvironmentSecret>(url);
        }

		/// <summary>
		/// Create or update a secret in an environment.
		/// </summary>
		/// <remarks>
		/// See the <a href="https://docs.github.com/rest/actions/secrets/#create-or-update-an-environment-secret">API documentation</a> for more information.
		/// </remarks>
		/// <param name="repoId">The unique identifier of the repository for the environment.</param>
		/// <param name="envName">The name of the environment. The name must be URL encoded. For example, any slashes in the name must be replaced with %2F</param>
		/// <param name="secretName">The name of the secret</param>
		/// <param name="upsertSecret">The encrypted value and id of the encryption key</param>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		/// <returns>A <see cref="EnvironmentSecret"/> instance for the environment secret that was created or updated.</returns>
		[ManualRoute("PUT", "repositories/{repoId}/environments/{envName}/secrets/{secretName}")]
        public async Task<EnvironmentSecret> CreateOrUpdate(int repoId, string envName, string secretName, UpsertEnvironmentSecret upsertSecret)
        {
			Ensure.ArgumentNotNull(repoId, nameof(repoId));
			Ensure.ArgumentNotNullOrEmptyString(envName, nameof(envName));
			Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));
			Ensure.ArgumentNotNull(upsertSecret, nameof(upsertSecret));
            Ensure.ArgumentNotNullOrEmptyString(upsertSecret.EncryptedValue, nameof(upsertSecret.EncryptedValue));
            Ensure.ArgumentNotNullOrEmptyString(upsertSecret.KeyId, nameof(upsertSecret.KeyId));

            var url = ApiUrls.EnvironmentSecret(repoId, envName, secretName);

            await ApiConnection.Put<EnvironmentSecret>(url, upsertSecret);

            return await Get(repoId, envName, secretName);
        }

		/// <summary>
		/// Delete a secret in an environment.
		/// </summary>
		/// <remarks>
		/// See the <a href="https://docs.github.com/rest/actions/secrets/#delete-an-environment-secret">API documentation</a> for more information.
		/// </remarks>
		/// <param name="repoId">The unique identifier of the repository for the environment.</param>
		/// <param name="envName">The name of the environment. The name must be URL encoded. For example, any slashes in the name must be replaced with %2F</param>
		/// <param name="secretName">The name of the secret</param>
		/// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
		[ManualRoute("DELETE", "repositories/{repoId}/environments/{envName}/secrets/{secretName}")]
        public Task Delete(int repoId, string envName, string secretName)
        {
			Ensure.ArgumentNotNull(repoId, nameof(repoId));
			Ensure.ArgumentNotNullOrEmptyString(envName, nameof(envName));
			Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

			var url = ApiUrls.EnvironmentSecret(repoId, envName, secretName);

            return ApiConnection.Delete(url);
        }
    }
}