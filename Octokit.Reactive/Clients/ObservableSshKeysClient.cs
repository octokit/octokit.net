using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableSshKeysClient : IObservableSshKeysClient
    {
        readonly ISshKeysClient _client;
        readonly IConnection _connection;

        /// <summary>
        /// Initializes a new SSH Key API client.
        /// </summary>
        /// <param name="client">An <see cref="IGitHubClient" /> used to make the requests</param>
        public ObservableSshKeysClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.SshKey;
            _connection = client.Connection;
        }

        /// <summary>
        /// Retrieves the <see cref="SshKey"/> for the specified id.
        /// </summary>
        /// <param name="id">The ID of the SSH key</param>
        /// <returns>A <see cref="SshKey"/></returns>
        [Obsolete("This method is obsolete. Please use User.Keys.Get(int) instead.")]
        public IObservable<SshKey> Get(int id)
        {
            return _client.Get(id).ToObservable();
        }

        /// <summary>
        /// Retrieves the <see cref="SshKey"/> for the specified id.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <returns>A <see cref="IReadOnlyPagedCollection{SshKey}"/> of <see cref="SshKey"/>.</returns>
        [Obsolete("This method is obsolete. Please use User.Keys.GetAll(string) instead.")]
        public IObservable<SshKey> GetAll(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return _connection.GetAndFlattenAllPages<SshKey>(ApiUrls.SshKeys(user));
        }

        /// <summary>
        /// Retrieves the <see cref="SshKey"/> for the specified id.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{SshKey}"/> of <see cref="SshKey"/>.</returns>
        [Obsolete("This method is obsolete. Please use User.Keys.GetAll() instead.")]
        public IObservable<SshKey> GetAllForCurrent()
        {
            return _connection.GetAndFlattenAllPages<SshKey>(ApiUrls.SshKeys());
        }

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="key">The SSH Key contents</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        [Obsolete("This method is obsolete. Please use User.Keys.Create(NewPublicKey) instead.")]
        public IObservable<SshKey> Create(SshKeyUpdate key)
        {
            Ensure.ArgumentNotNull(key, "key");

            return _client.Create(key).ToObservable();
        }

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="id">The ID of the SSH key</param>
        /// <param name="key">The SSH Key contents</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        [Obsolete("This method is no longer supported in the GitHub API. Delete and Create the key again instead.")]
        public IObservable<SshKey> Update(int id, SshKeyUpdate key)
        {
            Ensure.ArgumentNotNull(key, "key");

            return _client.Update(id, key).ToObservable();
        }

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="id">The id of the SSH key</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        [Obsolete("This method is obsolete. Please use User.Keys.Delete(int) instead.")]
        public IObservable<Unit> Delete(int id)
        {
            return _client.Delete(id).ToObservable();
        }
    }
}
