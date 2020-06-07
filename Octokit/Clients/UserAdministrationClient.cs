using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's User Administration API (GitHub Enterprise)
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/users/administration/">Administration API documentation</a> for more details.
    /// </remarks>
    public class UserAdministrationClient : ApiClient, IUserAdministrationClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAdministrationClient"/> class.
        /// </summary>
        /// <param name="apiConnection">The client's connection</param>
        public UserAdministrationClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Create a new user (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/enterprise/2.5/v3/users/administration/#create-a-new-user">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="newUser">The <see cref="NewUser"/> object describing the user to create</param>
        /// <returns>The created <see cref="User"/> object</returns>
        [ManualRoute("POST", "/admin/users")]
        public Task<User> Create(NewUser newUser)
        {
            Ensure.ArgumentNotNull(newUser, nameof(newUser));

            var endpoint = ApiUrls.UserAdministration();

            return ApiConnection.Post<User>(endpoint, newUser);
        }

        /// <summary>
        /// Rename an existing user (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/enterprise/2.5/v3/users/administration/#rename-an-existing-user">API documentation</a>
        /// for more information.
        /// Note that this queues a request to rename a user, rather than execute it straight away
        /// </remarks>
        /// <param name="login">The username to rename</param>
        /// <param name="userRename">The <see cref="UserRename"/> request, specifying the new login</param>
        /// <returns>A <see cref="UserRenameResponse"/> object indicating the queued task message and Url to the user</returns>
        [ManualRoute("POST", "/admin/users/{username}")]
        public Task<UserRenameResponse> Rename(string login, UserRename userRename)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));
            Ensure.ArgumentNotNull(userRename, nameof(userRename));

            var endpoint = ApiUrls.UserAdministration(login);

            return ApiConnection.Patch<UserRenameResponse>(endpoint, userRename);
        }

        /// <summary>
        /// Create an impersonation OAuth token (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/enterprise/2.5/v3/users/administration/#create-an-impersonation-oauth-token">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="login">The user to impersonate</param>
        /// <param name="newImpersonationToken">The <see cref="NewImpersonationToken"/> request specifying the required scopes</param>
        /// <returns>An <see cref="Authorization"/> object containing the impersonation token</returns>
        [ManualRoute("POST", "/admin/users/{username}/authorizations")]
        public Task<Authorization> CreateImpersonationToken(string login, NewImpersonationToken newImpersonationToken)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));
            Ensure.ArgumentNotNull(newImpersonationToken, nameof(newImpersonationToken));

            var endpoint = ApiUrls.UserAdministrationAuthorization(login);

            return ApiConnection.Post<Authorization>(endpoint, newImpersonationToken);
        }

        /// <summary>
        /// Deletes an impersonation OAuth token (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/enterprise/2.5/v3/users/administration/#delete-an-impersonation-oauth-token">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="login">The user to remove impersonation token from</param>
        /// <returns></returns>
        [ManualRoute("DELETE", "/admin/users/{username}/authorizations")]
        public async Task DeleteImpersonationToken(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));

            var endpoint = ApiUrls.UserAdministrationAuthorization(login);

            var response = await Connection.Delete(endpoint).ConfigureAwait(false);
            if (response != HttpStatusCode.NoContent)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 204", response);
            }
        }

        /// <summary>
        /// Promotes ordinary user to a site administrator (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/administration/#promote-an-ordinary-user-to-a-site-administrator">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="login">The user to promote to administrator.</param>
        /// <returns></returns>
        [ManualRoute("PUT", "/users/{username}/site_admin")]
        public Task Promote(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));
            var endpoint = ApiUrls.UserAdministrationSiteAdmin(login);
            return ApiConnection.Put(endpoint);
        }

        /// <summary>
        /// Demotes a site administrator to an ordinary user (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/administration/#demote-a-site-administrator-to-an-ordinary-user">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="login">The user to demote from administrator.</param>
        /// <returns></returns>
        [ManualRoute("DELETE", "/users/{username}/site_admin")]
        public Task Demote(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));
            var endpoint = ApiUrls.UserAdministrationSiteAdmin(login);
            return ApiConnection.Delete(endpoint);
        }

        /// <summary>
        /// Suspends a user (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/administration/#suspend-a-user">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="login">The user to suspend.</param>
        /// <returns></returns>
        [ManualRoute("PUT", "/users/{username}/suspended")]
        public Task Suspend(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));
            var endpoint = ApiUrls.UserAdministrationSuspension(login);
            return ApiConnection.Put(endpoint);
        }

        /// <summary>
        /// Unsuspends a user (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/administration/#unsuspend-a-user">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="login">The user to unsuspend.</param>
        /// <returns></returns>
        [ManualRoute("DELETE", "/users/{username}/suspended")]
        public Task Unsuspend(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));
            var endpoint = ApiUrls.UserAdministrationSuspension(login);
            return ApiConnection.Delete(endpoint);
        }

        /// <summary>
        /// List all public keys (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/enterprise/2.5/v3/users/administration/#list-all-public-keys">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <returns></returns>
        [ManualRoute("PUT", "/admin/keys")]
        public Task<IReadOnlyList<PublicKey>> ListAllPublicKeys()
        {
            var endpoint = ApiUrls.UserAdministrationPublicKeys();
            return ApiConnection.GetAll<PublicKey>(endpoint);
        }

        /// <summary>
        /// Delete a user (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/enterprise/2.5/v3/users/administration/#delete-a-user">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="login">The user to delete</param>
        /// <returns></returns>
        [ManualRoute("DELETE", "/admin/users/{username}")]
        public async Task Delete(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));
            var endpoint = ApiUrls.UserAdministration(login);

            var response = await Connection.Delete(endpoint).ConfigureAwait(false);
            if (response != HttpStatusCode.NoContent)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 204", response);
            }
        }

        /// <summary>
        /// Delete a public key (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/enterprise/2.5/v3/users/administration/#delete-a-public-key">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="keyId">The key to delete</param>
        /// <returns></returns>
        [ManualRoute("DELETE", "/admin/keys/{key_id}")]
        public async Task DeletePublicKey(int keyId)
        {
            Ensure.ArgumentNotNull(keyId, nameof(keyId));
            var endpoint = ApiUrls.UserAdministrationPublicKeys(keyId);

            var response = await Connection.Delete(endpoint).ConfigureAwait(false);
            if (response != HttpStatusCode.NoContent)
            {
                throw new ApiException("Invalid Status Code returned. Expected a 204", response);
            }
        }
    }
}
