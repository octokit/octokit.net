using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's User Administration API (GitHub Enterprise)
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/users/administration/">Administration API documentation</a> for more details.
    /// </remarks>
    public interface IUserAdministrationClient
    {
        /// <summary>
        /// Create a new user (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/enterprise/2.5/v3/users/administration/#create-a-new-user">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="newUser">The <see cref="NewUser"/> object describing the user to create</param>
        /// <returns>The created <see cref="User"/> object</returns>
        Task<User> Create(NewUser newUser);

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
        Task<UserRenameResponse> Rename(string login, UserRename userRename);

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
        Task<Authorization> CreateImpersonationToken(string login, NewImpersonationToken newImpersonationToken);

        /// <summary>
        /// Deletes an impersonation OAuth token (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/enterprise/2.5/v3/users/administration/#delete-an-impersonation-oauth-token">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="login">The user to remove impersonation token from</param>
        /// <returns></returns>
        Task DeleteImpersonationToken(string login);

        /// <summary>
        /// Promotes ordinary user to a site administrator (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/administration/#promote-an-ordinary-user-to-a-site-administrator">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="login">The user to promote to administrator.</param>
        /// <returns></returns>
        Task Promote(string login);

        /// <summary>
        /// Demotes a site administrator to an ordinary user (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/administration/#demote-a-site-administrator-to-an-ordinary-user">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="login">The user to demote from administrator.</param>
        /// <returns></returns>
        Task Demote(string login);

        /// <summary>
        /// Suspends a user (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/administration/#suspend-a-user">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="login">The user to suspend.</param>
        /// <returns></returns>
        Task Suspend(string login);

        /// <summary>
        /// Unsuspends a user (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/administration/#unsuspend-a-user">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="login">The user to unsuspend.</param>
        /// <returns></returns>
        Task Unsuspend(string login);

        /// <summary>
        /// List all public keys (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/enterprise/2.5/v3/users/administration/#list-all-public-keys">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <returns></returns>
        Task<IReadOnlyList<PublicKey>> ListAllPublicKeys();

        /// <summary>
        /// Delete a user (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/enterprise/2.5/v3/users/administration/#delete-a-user">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="login">The user to delete</param>
        /// <returns></returns>
        Task Delete(string login);

        /// <summary>
        /// Delete a public key (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/enterprise/2.5/v3/users/administration/#delete-a-public-key">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <param name="keyId">The key to delete</param>
        /// <returns></returns>
        Task DeletePublicKey(int keyId);
    }
}
