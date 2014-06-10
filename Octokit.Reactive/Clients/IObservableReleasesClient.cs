using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Octokit.Reactive
{
    public interface IObservableReleasesClient
    {
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
        IObservable<Release> GetAll(string owner, string name);

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
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "Method makes a network request")]
        IObservable<Release> Get(string owner, string name, int id);

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
        IObservable<Release> CreateRelease(string owner, string name, ReleaseUpdate data);

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
        IObservable<Release> EditRelease(string owner, string name, int id, ReleaseUpdate data);

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
        IObservable<Unit> DeleteRelease(string owner, string name, int id);

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
        IObservable<ReleaseAsset> GetAssets(string owner, string name, int id);

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
        IObservable<ReleaseAsset> UploadAsset(Release release, ReleaseAssetUpload data);

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
        IObservable<ReleaseAsset> GetAsset(string owner, string name, int releaseId, int assetId);

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
        IObservable<ReleaseAsset> EditAsset(string owner, string name, int releaseId, int assetId, ReleaseAssetUpdate data);

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
        IObservable<Unit> DeleteAsset(string owner, string name, int id);
    }
}
