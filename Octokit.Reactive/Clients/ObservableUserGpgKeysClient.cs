using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's UserUser GPG Keys API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/users/gpg_keys/">User GPG Keys documentation</a> for more information.
    /// </remarks>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gpg")]
    public class ObservableUserGpgKeysClient : IObservableUserGpgKeysClient
    {
        readonly IUserGpgKeysClient _client;

        public ObservableUserGpgKeysClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.User.GpgKey;
        }

        /// <summary>
        /// Gets all GPG keys for the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/gpg_keys/#list-your-gpg-keys">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyList{GpgKey}"/> of <see cref="GpgKey"/>s for the current user.</returns>
        public IObservable<GpgKey> GetAllForCurrent()
        {
            return GetAllForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// Gets all GPG keys for the authenticated user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/gpg_keys/#list-your-gpg-keys">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyList{GpgKey}"/> of <see cref="GpgKey"/>s for the current user.</returns>
        public IObservable<GpgKey> GetAllForCurrent(ApiOptions options)
        {
            return _client.GetAllForCurrent(options).ToObservable().SelectMany(k => k);
        }

        /// <summary>
        /// View extended details of the <see cref="GpgKey"/> for the specified id.
        /// </summary>
        /// <param name="id">The Id of the GPG key</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/gpg_keys/#get-a-single-gpg-key">API documentation</a> for more information.
        /// </remarks>
        /// <returns>The <see cref="GpgKey"/> for the specified Id.</returns>
        public IObservable<GpgKey> Get(long id)
        {
            return _client.Get(id).ToObservable();
        }

        /// <summary>
        /// Creates a new <see cref="GpgKey"/> for the authenticated user.
        /// </summary>
        /// <param name="newGpgKey">The new GPG key to add.</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/gpg_keys/#create-a-gpg-key">API documentation</a> for more information.
        /// </remarks>
        /// <returns>The newly created <see cref="GpgKey"/>.</returns>
        public IObservable<GpgKey> Create(NewGpgKey newGpgKey)
        {
            Ensure.ArgumentNotNull(newGpgKey, nameof(newGpgKey));

            return _client.Create(newGpgKey).ToObservable();
        }

        /// <summary>
        /// Deletes the GPG key for the specified Id.
        /// </summary>
        /// <param name="id">The Id of the GPG key to delete.</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/gpg_keys/#delete-a-gpg-key">API documentation</a> for more information.
        /// </remarks>
        /// <returns></returns>
        public IObservable<Unit> Delete(long id)
        {
            return _client.Delete(id).ToObservable();
        }
    }
}
