using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Organization Secrets API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/reference/actions#secrets">Organization Secrets API documentation</a> for more details.
    /// </remarks>
    public interface IOrganizationSecretsClient
    {
        /// <summary>
        /// Get the public signing key to encrypt secrets for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#get-an-organization-public-key">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="SecretsPublicKey"/> instance for the organization public key.</returns>
        Task<SecretsPublicKey> GetPublicKey(string org);

        /// <summary>
        /// List the secrets for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#list-organization-secrets">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="OrganizationSecretsCollection"/> instance for the list of organization secrets.</returns>
        Task<OrganizationSecretsCollection> GetAll(string org);

        /// <summary>
        /// Get a secret from an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#get-an-organization-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="secretName">The name of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="OrganizationSecret"/> instance for the organization secret.</returns>
        Task<OrganizationSecret> Get(string org, string secretName);

        /// <summary>
        /// Create or update a secret in an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#create-or-update-an-organization-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="secretName">The name of the secret</param>
        /// <param name="upsertSecret">The encrypted value, id of the encryption key, and visibility info to upsert</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="OrganizationSecret"/> instance for the organization secret that was created or updated.</returns>
        Task<OrganizationSecret> CreateOrUpdate(string org, string secretName, UpsertOrganizationSecret upsertSecret);

        /// <summary>
        /// Delete a secret in an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#delete-an-organization-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="secretName">The name of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task Delete(string org, string secretName);

        /// <summary>
        /// Get the list of selected sites that have access to a secret.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#list-selected-repositories-for-an-organization-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="secretName">The name of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<OrganizationSecretRepositoryCollection> GetSelectedRepositoriesForSecret(string org, string secretName);

        /// <summary>
        /// Set the list of selected sites that have access to a secret.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#set-selected-repositories-for-an-organization-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="secretName">The name of the secret</param>
        /// <param name="repositories">The list of repositories that should have access to view and use the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task SetSelectedRepositoriesForSecret(string org, string secretName, SelectedRepositoryCollection repositories);

        /// <summary>
        /// Add a selected site to the visibility list of a secret.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#add-selected-repository-to-an-organization-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="secretName">The name of the secret</param>
        /// <param name="repoId">The id of the repo to add to the visibility list of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task AddRepoToOrganizationSecret(string org, string secretName, long repoId);

        /// <summary>
        /// ARemoved a selected site from the visibility list of a secret.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#remove-selected-repository-from-an-organization-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="secretName">The name of the secret</param>
        /// <param name="repoId">The id of the repo to add to the visibility list of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task RemoveRepoFromOrganizationSecret(string org, string secretName, long repoId);
    }
}
