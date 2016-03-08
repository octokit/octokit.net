using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's User Keys API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/users/keys/">Users API documentation</a> for more information.
    /// </remarks>
    public interface ISshKeysClient
    {
        /// <summary>
        /// Retrieves the <see cref="SshKey"/> for the specified id.
        /// </summary>
        /// <param name="id">The ID of the SSH key</param>
        /// <returns>A <see cref="SshKey"/></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        [Obsolete("This method is obsolete. Please use User.Keys.Get(int) instead.")]
        Task<SshKey> Get(int id);

        /// <summary>
        /// Retrieves the <see cref="SshKey"/> for the specified id.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <returns>A <see cref="IReadOnlyPagedCollection{SshKey}"/> of <see cref="SshKey"/>.</returns>
        [Obsolete("This method is obsolete. Please use User.Keys.GetAll(string) instead.")]
        Task<IReadOnlyList<SshKey>> GetAll(string user);

        /// <summary>
        /// Retrieves the <see cref="SshKey"/> for the specified id.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{SshKey}"/> of <see cref="SshKey"/>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Makes a network request")]
        [Obsolete("This method is obsolete. Please use User.Keys.GetAll() instead.")]
        Task<IReadOnlyList<SshKey>> GetAllForCurrent();

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="key">The SSH Key contents</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        [Obsolete("This method is obsolete. Please use User.Keys.Create(NewPublicKey) instead.")]
        Task<SshKey> Create(SshKeyUpdate key);

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="id">The ID of the SSH key</param>
        /// <param name="key">The SSH Key contents</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        [Obsolete("This method is no longer supported in the GitHub API. Delete and Create the key again instead.")]
        Task<SshKey> Update(int id, SshKeyUpdate key);

        /// <summary>
        /// Update the specified <see cref="UserUpdate"/>.
        /// </summary>
        /// <param name="id">The id of the SSH key</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        [Obsolete("This method is obsolete. Please use User.Keys.Delete(int) instead.")]
        Task Delete(int id);
    }
}
