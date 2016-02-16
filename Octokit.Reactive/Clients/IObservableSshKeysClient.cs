using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Octokit.Reactive
{
    public interface IObservableSshKeysClient
    {
        /// <summary>
        /// Retrieves the <see cref="SshKey"/> for the specified id.
        /// </summary>
        /// <param name="id">The ID of the SSH key</param>
        /// <returns>A <see cref="SshKey"/></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        [Obsolete("This method is obsolete. Please use User.Keys.Get(int) instead.")]
        IObservable<SshKey> Get(int id);

        /// <summary>
        /// Retrieves the <see cref="SshKey"/> for the specified id.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <returns>A <see cref="IReadOnlyPagedCollection{SshKey}"/> of <see cref="SshKey"/>.</returns>
        [Obsolete("This method is obsolete. Please use User.Keys.GetAll(string) instead.")]
        IObservable<SshKey> GetAll(string user);

        /// <summary>
        /// Retrieves the <see cref="SshKey"/> for the specified id.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{SshKey}"/> of <see cref="SshKey"/>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Makes a network request")]
        [Obsolete("This method is obsolete. Please use User.Keys.GetAll() instead.")]
        IObservable<SshKey> GetAllForCurrent();

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="key">The SSH Key contents</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        [Obsolete("This method is obsolete. Please use User.Keys.Create(NewPublicKey) instead.")]
        IObservable<SshKey> Create(SshKeyUpdate key);

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="id">The ID of the SSH key</param>
        /// <param name="key">The SSH Key contents</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        [Obsolete("This method is no longer supported in the GitHub API. Delete and Create the key again instead.")]
        IObservable<SshKey> Update(int id, SshKeyUpdate key);

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="id">The id of the SSH key</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        [Obsolete("This method is obsolete. Please use User.Keys.Delete(int) instead.")]
        IObservable<Unit> Delete(int id);
    }
}
