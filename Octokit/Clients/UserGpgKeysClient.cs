﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    public class UserGpgKeysClient : ApiClient, IUserGpgKeysClient
    {
        /// <summary>
        /// Instatiates a new GitHub User GPG Keys API client.
        /// </summary>
        /// <param name="apiConnection">The API connection.</param>
        public UserGpgKeysClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all GPG keys for the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/gpg_keys/#list-your-gpg-keys">API documentation</a> for more information.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyList{GpgKey}"/> of <see cref="GpgKey"/>s for the current user.</returns>
        public Task<IReadOnlyList<GpgKey>> GetAllForCurrent()
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
        public Task<IReadOnlyList<GpgKey>> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<GpgKey>(ApiUrls.GpgKeys(), null, AcceptHeaders.GpgKeysPreview, options);
        }

        /// <summary>
        /// View extended details of the <see cref="GpgKey"/> for the specified id.
        /// </summary>
        /// <param name="id">The ID of the GPG key</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/gpg_keys/#get-a-single-gpg-key">API documentation</a> for more information.
        /// </remarks>
        /// <returns>The <see cref="GpgKey"/> for the specified ID.</returns>
        public Task<GpgKey> Get(int id)
        {
            return ApiConnection.Get<GpgKey>(ApiUrls.GpgKeys(id), null, AcceptHeaders.GpgKeysPreview);
        }

        /// <summary>
        /// Creates a new <see cref="GpgKey"/> for the authenticated user.
        /// </summary>
        /// <param name="newGpgKey">The new GPG key to add.</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/gpg_keys/#create-a-gpg-key">API documentation</a> for more information.
        /// </remarks>
        /// <returns>The newly created <see cref="GpgKey"/>.</returns>
        public Task<GpgKey> Create(NewGpgKey newGpgKey)
        {
            Ensure.ArgumentNotNull(newGpgKey, "newGpgKey");

            return ApiConnection.Post<GpgKey>(ApiUrls.GpgKeys(), newGpgKey, AcceptHeaders.GpgKeysPreview);
        }

        /// <summary>
        /// Deletes the GPG key for the specified ID.
        /// </summary>
        /// <param name="id">The ID of the GPG key to delete.</param>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/users/gpg_keys/#delete-a-gpg-key">API documentation</a> for more information.
        /// </remarks>
        /// <returns></returns>
        public Task Delete(int id)
        {
            return ApiConnection.Delete(ApiUrls.GpgKeys(id), new object(), AcceptHeaders.GpgKeysPreview);
        }
    }
}