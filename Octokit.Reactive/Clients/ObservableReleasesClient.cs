using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableReleasesClient : IObservableReleasesClient
    {
        readonly IReleasesClient _client;
        readonly IConnection _connection;

        public ObservableReleasesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Release;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets all <see cref="Release"/>s for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#list-releases-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The list of <see cref="Release"/>s for the specified repository.</returns>
        public IObservable<Release> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.GetAndFlattenAllPages<Release>(ApiUrls.Releases(owner, name));
        }

        /// <summary>
        /// Gets a single <see cref="Release"/> for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#get-a-single-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="id">The id of the release</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The <see cref="Release"/> specified by the id</returns>
        public IObservable<Release> Get(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Get(owner, name, id).ToObservable();
        }

        /// <summary>
        /// Creates a new <see cref="Release"/> for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#create-a-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="data">A description of the release to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The created <see cref="Release"/>.</returns>
        public IObservable<Release> CreateRelease(string owner, string name, ReleaseUpdate data)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(data, "data");

            return _client.CreateRelease(owner, name, data).ToObservable();
        }

        /// <summary>
        /// Edits an existing <see cref="Release"/> for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#edit-a-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="id">The id of the release</param>
        /// <param name="data">A description of the release to edit</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The updated <see cref="Release"/>.</returns>
        public IObservable<Release> EditRelease(string owner, string name, int id, ReleaseUpdate data)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(data, "data");

            return _client.EditRelease(owner, name, id, data).ToObservable();
        }

        /// <summary>
        /// Deletes an existing <see cref="Release"/> for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#delete-a-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="id">The id of the release to delete</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns></returns>
        public IObservable<Unit> DeleteRelease(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.DeleteRelease(owner, name, id).ToObservable();
        }

        /// <summary>
        /// Gets all <see cref="ReleaseAsset"/> for the specified release of the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#list-assets-for-a-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="id">The id of the <see cref="Release"/>.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The list of <see cref="ReleaseAsset"/> for the specified release of the specified repository.</returns>
        public IObservable<ReleaseAsset> GetAssets(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.GetAndFlattenAllPages<ReleaseAsset>(ApiUrls.ReleaseAssets(owner, name, id));
        }

        /// <summary>
        /// Gets the specified <see cref="ReleaseAsset"/> for the specified release of the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#get-a-single-release-asset">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="releaseId">The id of the <see cref="Release"/></param>
        /// <param name="assetId">The id of the <see cref="ReleaseAsset"/></param>
        /// <returns>The <see cref="ReleaseAsset"/> specified by the asset id.</returns>
        public IObservable<ReleaseAsset> GetAsset(string owner, string name, int releaseId, int assetId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(releaseId, "releaseId");
            Ensure.ArgumentNotNull(assetId, "assetId");

            return _client.GetAsset(owner, name, releaseId, assetId).ToObservable();
        }

        /// <summary>
        /// Uploads a <see cref="ReleaseAsset"/> for the specified release.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#upload-a-release-asset">API documentation</a> for more information.
        /// </remarks>
        /// <param name="release">The <see cref="Release"/> to attach the uploaded asset to</param>
        /// <param name="data">Description of the asset with its data</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The created <see cref="ReleaseAsset"/>.</returns>
        public IObservable<ReleaseAsset> UploadAsset(Release release, ReleaseAssetUpload data)
        {
            Ensure.ArgumentNotNull(release, "release");
            Ensure.ArgumentNotNull(data, "data");

            return _client.UploadAsset(release, data).ToObservable();
        }

        /// <summary>
        /// Edits the <see cref="ReleaseAsset"/> for the specified release of the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#edit-a-release-asset">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="releaseId">The id of the <see cref="Release"/></param>
        /// <param name="assetId">The id of the <see cref="ReleaseAsset"/></param>
        /// <param name="data">Description of the asset with its amended data</param>
        /// <returns>The edited <see cref="ReleaseAsset"/>.</returns>
        public IObservable<ReleaseAsset> EditAsset(string owner, string name, int releaseId, int assetId, ReleaseAssetUpdate data)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(releaseId, "releaseId");
            Ensure.ArgumentNotNull(assetId, "assetId");
            Ensure.ArgumentNotNull(data, "data");

            return _client.EditAsset(owner, name, releaseId, assetId, data).ToObservable();
        }

        /// <summary>
        /// Deletes the specified <see cref="ReleaseAsset"/> from the specified repository
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#delete-a-release-asset">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="id">The id of the <see cref="ReleaseAsset"/>.</param>
        /// <returns></returns>
        public IObservable<Unit> DeleteAsset(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.DeleteAsset(owner, name, id).ToObservable();
        }
    }
}
