using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's UserUser GPG Keys API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/users/gpg_keys/">User GPG Keys documentation</a> for more information.
    /// </remarks>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gpg")]
    public interface IUserGpgKeysClient
    {
        /// <summary>
        /// Gets all GPG keys for the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/gpg_keys/#list-your-gpg-keys">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyList{GpgKey}"/> of <see cref="GpgKey"/>s for the current user.</returns>
        Task<IReadOnlyList<GpgKey>> GetAllForCurrent();

        /// <summary>
        /// Gets all GPG keys for the authenticated user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/gpg_keys/#list-your-gpg-keys">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyList{GpgKey}"/> of <see cref="GpgKey"/>s for the current user.</returns>
        Task<IReadOnlyList<GpgKey>> GetAllForCurrent(ApiOptions options);

        /// <summary>
        /// View extended details of the <see cref="GpgKey"/> for the specified id.
        /// </summary>
        /// <param name="id">The Id of the GPG key</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/gpg_keys/#get-a-single-gpg-key">API documentation</a> for more information.
        /// </remarks>
        /// <returns>The <see cref="GpgKey"/> for the specified Id.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        Task<GpgKey> Get(int id);

        /// <summary>
        /// Creates a new <see cref="GpgKey"/> for the authenticated user.
        /// </summary>
        /// <param name="newGpgKey">The new GPG key to add.</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/gpg_keys/#create-a-gpg-key">API documentation</a> for more information.
        /// </remarks>
        /// <returns>The newly created <see cref="GpgKey"/>.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gpg")]
        Task<GpgKey> Create(NewGpgKey newGpgKey);

        /// <summary>
        /// Deletes the GPG key for the specified Id.
        /// </summary>
        /// <param name="id">The Id of the GPG key to delete.</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/gpg_keys/#delete-a-gpg-key">API documentation</a> for more information.
        /// </remarks>
        /// <returns></returns>
        Task Delete(int id);
    }
}
