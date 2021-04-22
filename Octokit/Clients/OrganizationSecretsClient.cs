using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Organization Secrets API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://docs.github.com/v3/actions#secrets/">Organization Secrets API documentation</a> for more details.
    /// </remarks>
    public class OrganizationSecretsClient : ApiClient, IOrganizationSecretsClient
    {
        public OrganizationSecretsClient(IApiConnection apiConnection) : base(apiConnection) { }

        /// <summary>
        /// Get the public signing key to encrypt secrets for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#get-an-organization-public-key">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="SecretsPublicKey"/> instance for the organization public key.</returns>
        [ManualRoute("GET", "/orgs/{org}/actions/secrets/public-key")]
        public Task<SecretsPublicKey> GetPublicKey(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return ApiConnection.Get<SecretsPublicKey>(ApiUrls.OrganizationRepositorySecretPublicKey(org));
        }

        /// <summary>
        /// List the secrets for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#list-organization-secrets">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="OrganizationSecretsCollection"/> instance for the list of organization secrets.</returns>
        [ManualRoute("GET", "/orgs/{org}/actions/secrets")]
        public Task<OrganizationSecretsCollection> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return ApiConnection.Get<OrganizationSecretsCollection>(ApiUrls.OrganizationRepositorySecrets(org));
        }

        /// <summary>
        /// Get a secret from an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#get-an-organization-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="secretName">The name of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="OrganizationSecret"/> instance for the organization secret.</returns>
        [ManualRoute("GET", "/orgs/{org}/actions/secrets/{secretName}")]
        public Task<OrganizationSecret> Get(string org, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            return ApiConnection.Get<OrganizationSecret>(ApiUrls.OrganizationRepositorySecret(org, secretName));
        }

        /// <summary>
        /// Create or update a secret in an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#create-or-update-an-organization-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="secretName">The name of the secret</param>
        /// <param name="upsertSecret">The encrypted value, id of the encryption key, and visibility info to upsert</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="OrganizationSecret"/> instance for the organization secret that was created or updated.</returns>
        [ManualRoute("PUT", "/orgs/{org}/actions/secrets/{secretName}")]
        public async Task<OrganizationSecret> CreateOrUpdate(string org, string secretName, UpsertOrganizationSecret upsertSecret)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));
            Ensure.ArgumentNotNull(upsertSecret, nameof(upsertSecret));
            Ensure.ArgumentNotNullOrEmptyString(upsertSecret.KeyId, nameof(upsertSecret.KeyId));
            Ensure.ArgumentNotNullOrEmptyString(upsertSecret.EncryptedValue, nameof(upsertSecret.EncryptedValue));
            Ensure.ArgumentNotNullOrEmptyString(upsertSecret.Visibility, nameof(upsertSecret.Visibility));

            await ApiConnection.Put<OrganizationSecret>(ApiUrls.OrganizationRepositorySecret(org, secretName), upsertSecret);
            return await ApiConnection.Get<OrganizationSecret>(ApiUrls.OrganizationRepositorySecret(org, secretName));
        }

        /// <summary>
        /// Delete a secret in an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#delete-an-organization-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="secretName">The name of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("DELETE", "/orgs/{org}/actions/secrets/{secretName}")]
        public Task Delete(string org, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            return ApiConnection.Delete(ApiUrls.OrganizationRepositorySecret(org, secretName));
        }

        /// <summary>
        /// Get the list of selected sites that have access to a secret.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#list-selected-repositories-for-an-organization-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="secretName">The name of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/orgs/{org}/actions/secrets/{secretName}/repositories")]
        public Task<OrganizationSecretRepositoryCollection> GetSelectedRepositoriesForSecret(string org, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            return ApiConnection.Get<OrganizationSecretRepositoryCollection>(ApiUrls.OrganizationRepositorySecretRepositories(org, secretName));
        }

        /// <summary>
        /// Set the list of selected sites that have access to a secret.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#set-selected-repositories-for-an-organization-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="secretName">The name of the secret</param>
        /// <param name="repositories">The list of repositories that should have access to view and use the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("PUT", "/orgs/{org}/actions/secrets/{secretName}/repositories")]
        public async Task SetSelectedRepositoriesForSecret(string org, string secretName, SelectedRepositoryCollection repositories)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));
            Ensure.ArgumentNotNull(repositories, nameof(repositories));
            Ensure.ArgumentNotNull(repositories.SelectedRepositoryIds, nameof(repositories.SelectedRepositoryIds));

            await ApiConnection.Put<SelectedRepositoryCollection>(ApiUrls.OrganizationRepositorySecretRepositories(org, secretName), repositories);
            return;
        }

        /// <summary>
        /// Add a selected site to the visibility list of a secret.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#add-selected-repository-to-an-organization-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="secretName">The name of the secret</param>
        /// <param name="repoId">The id of the repo to add to the visibility list of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("PUT", "/orgs/{org}/actions/secrets/{secretName}/repositories/{repoId}")]
        public Task AddRepoToOrganizationSecret(string org, string secretName, long repoId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));
            Ensure.ArgumentNotNull(repoId, nameof(repoId));

            return ApiConnection.Put(ApiUrls.OrganizationRepositorySecretRepository(org, secretName, repoId));
        }

        /// <summary>
        /// ARemoved a selected site from the visibility list of a secret.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#remove-selected-repository-from-an-organization-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="secretName">The name of the secret</param>
        /// <param name="repoId">The id of the repo to add to the visibility list of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("DELETE", "/orgs/{org}/actions/secrets/{secretName}/repositories/{repoId}")]
        public Task RemoveRepoFromOrganizationSecret(string org, string secretName, long repoId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));
            Ensure.ArgumentNotNull(repoId, nameof(repoId));

            return ApiConnection.Delete(ApiUrls.OrganizationRepositorySecretRepository(org, secretName, repoId));
        }
    }
}
