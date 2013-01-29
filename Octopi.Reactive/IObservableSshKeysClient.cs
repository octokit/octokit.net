using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Octopi.Reactive
{
    public interface IObservableSshKeysClient
    {
        /// <summary>
        /// Retrieves the <see cref="SshKey"/> for the specified id.
        /// </summary>
        /// <param name="id">The ID of the SSH key.</param>
        /// <returns>A <see cref="SshKey"/></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        IObservable<SshKey> Get(long id);

        /// <summary>
        /// Retrieves the <see cref="SshKey"/> for the specified id.
        /// </summary>
        /// <param name="user">The login of the user.</param>
        /// <returns>A <see cref="IReadOnlyPagedCollection{SshKey}"/> of <see cref="SshKey"/>.</returns>
        IObservable<IReadOnlyCollection<SshKey>> GetAll(string user);

        /// <summary>
        /// Retrieves the <see cref="SshKey"/> for the specified id.
        /// </summary>
        /// <exception cref="AuthenticationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{SshKey}"/> of <see cref="SshKey"/>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Makes a network request")]
        IObservable<IReadOnlyCollection<SshKey>> GetAllForCurrent();

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="AuthenticationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        IObservable<SshKey> Create(SshKeyUpdate key);

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="key"></param>
        /// <exception cref="AuthenticationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        IObservable<SshKey> Update(long id, SshKeyUpdate key);

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="id">The id of the SSH key</param>
        /// <exception cref="AuthenticationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        IObservable<Unit> Delete(long id);
    }
}
