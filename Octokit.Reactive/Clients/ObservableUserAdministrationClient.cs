﻿using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;
using System.Reactive.Linq;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's User Administration API (GitHub Enterprise)
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/users/administration/">Administration API documentation</a> for more details.
    /// </remarks>
    public class ObservableUserAdministrationClient : IObservableUserAdministrationClient
    {
        readonly IUserAdministrationClient _client;
        readonly IConnection _connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableUserAdministrationClient"/> class.
        /// </summary>
        /// <param name="client">An <see cref="IGitHubClient" /> used to make the requests</param>
        public ObservableUserAdministrationClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.User.Administration;
            _connection = client.Connection;
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
        public IObservable<User> Create(NewUser newUser)
        {
            return _client.Create(newUser).ToObservable();
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
        public IObservable<UserRenameResponse> Rename(string login, UserRename userRename)
        {
            return _client.Rename(login, userRename).ToObservable();
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
        public IObservable<Authorization> CreateImpersonationToken(string login, NewImpersonationToken newImpersonationToken)
        {
            return _client.CreateImpersonationToken(login, newImpersonationToken).ToObservable();
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
        public IObservable<Unit> DeleteImpersonationToken(string login)
        {
            return _client.DeleteImpersonationToken(login).ToObservable();
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
        public IObservable<Unit> Promote(string login)
        {
            return _client.Promote(login).ToObservable();
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
        public IObservable<Unit> Demote(string login)
        {
            return _client.Demote(login).ToObservable();
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
        public IObservable<Unit> Suspend(string login)
        {
            return _client.Suspend(login).ToObservable();
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
        public IObservable<Unit> Unsuspend(string login)
        {
            return _client.Unsuspend(login).ToObservable();
        }

        /// <summary>
        /// List all public keys (must be Site Admin user).
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/enterprise/2.5/v3/users/administration/#list-all-public-keys">API documentation</a>
        /// for more information.
        /// </remarks>
        /// <returns></returns>
        public IObservable<PublicKey> ListAllPublicKeys()
        {
            return _connection.GetAndFlattenAllPages<PublicKey>(ApiUrls.UserAdministrationPublicKeys());
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
        public IObservable<Unit> Delete(string login)
        {
            return _client.Delete(login).ToObservable();
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
        public IObservable<Unit> DeletePublicKey(int keyId)
        {
            return _client.DeletePublicKey(keyId).ToObservable();
        }
    }
}
