using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Organization Secrets API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/reference/actions#secrets">Organization Secrets API documentation</a> for more details.
    /// </remarks>
    public class ObservableOrganizationSecretsClient : IObservableOrganizationSecretsClient
    {
        readonly IOrganizationSecretsClient _client;
        readonly IConnection _connection;

        /// <summary>
        /// Initializes a new Organization API client.
        /// </summary>
        /// <param name="client">An <see cref="IGitHubClient" /> used to make the requests</param>
        public ObservableOrganizationSecretsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Organization.Actions.Secrets;
            _connection = client.Connection;
        }

        /// <summary>
        /// Get the public signing key to encrypt secrets for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#get-an-organization-public-key">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="SecretsPublicKey"/> instance for the organization public key.</returns>
        public IObservable<SecretsPublicKey> GetPublicKey(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return _client.GetPublicKey(org).ToObservable();
        }

        /// <summary>
        /// List the secrets for an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#list-organization-secrets">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="OrganizationSecretsCollection"/> instance for the list of organization secrets.</returns>
        public IObservable<OrganizationSecretsCollection> GetAll(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return _client.GetAll(org).ToObservable();
        }

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
        public IObservable<OrganizationSecret> Get(string org, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            return _client.Get(org, secretName).ToObservable();
        }

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
        public IObservable<OrganizationSecret> CreateOrUpdate(string org, string secretName, UpsertOrganizationSecret upsertSecret)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));
            Ensure.ArgumentNotNull(upsertSecret, nameof(upsertSecret));
            Ensure.ArgumentNotNull(upsertSecret.EncryptedValue, nameof(upsertSecret.EncryptedValue));
            Ensure.ArgumentNotNull(upsertSecret.KeyId, nameof(upsertSecret.KeyId));

            return _client.CreateOrUpdate(org, secretName, upsertSecret).ToObservable();
        }

        /// <summary>
        /// Delete a secret in an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#delete-an-organization-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="secretName">The name of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Unit> Delete(string org, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            return _client.Delete(org, secretName).ToObservable();
        }

        /// <summary>
        /// Get the list of selected sites that have access to a secret.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#list-selected-repositories-for-an-organization-secret">API documentation</a> for more information.
        /// </remarks>
        /// <param name="org">The name of the organization</param>
        /// <param name="secretName">The name of the secret</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<OrganizationSecretRepositoryCollection> GetSelectedRepositoriesForSecret(string org, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            return _client.GetSelectedRepositoriesForSecret(org, secretName).ToObservable();
        }

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
        public IObservable<Unit> SetSelectedRepositoriesForSecret(string org, string secretName, SelectedRepositoryCollection repositories)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));
            Ensure.ArgumentNotNull(repositories, nameof(repositories));

            return _client.SetSelectedRepositoriesForSecret(org, secretName, repositories).ToObservable();
        }

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
        public IObservable<Unit> AddRepoToOrganizationSecret(string org, string secretName, long repoId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));
            Ensure.ArgumentNotNull(repoId, nameof(repoId));

            return _client.AddRepoToOrganizationSecret(org, secretName, repoId).ToObservable();
        }

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
        public IObservable<Unit> RemoveRepoFromOrganizationSecret(string org, string secretName, long repoId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));
            Ensure.ArgumentNotNull(repoId, nameof(repoId));

            return _client.RemoveRepoFromOrganizationSecret(org, secretName, repoId).ToObservable();
        }
    }
}
