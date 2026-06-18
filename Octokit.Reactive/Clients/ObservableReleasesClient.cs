using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Threading;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Releases API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/releases/">Releases API documentation</a> for more information.
    /// </remarks>
    public class ObservableReleasesClient : IObservableReleasesClient
    {
        readonly IReleasesClient _client;
        readonly IConnection _connection;

        public ObservableReleasesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Repository.Release;
            _connection = client.Connection;
        }

        /// <summary>
        /// Generates a <see cref="GeneratedReleaseNotes"/>s for the specified repository with auto generated notes.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/releases/releases#generate-release-notes-content-for-a-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="data">The request for generating release notes</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<GeneratedReleaseNotes> GenerateReleaseNotes(string owner, string name, GenerateReleaseNotesRequest data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(data, nameof(data));

            return _client.GenerateReleaseNotes(owner, name, data, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Generates a <see cref="GeneratedReleaseNotes"/>s for the specified repository with auto generated notes.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/releases/releases#generate-release-notes-content-for-a-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="data">The request for generating release notes</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<GeneratedReleaseNotes> GenerateReleaseNotes(long repositoryId, GenerateReleaseNotesRequest data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(data, nameof(data));

            return _client.GenerateReleaseNotes(repositoryId, data, cancellationToken).ToObservable();
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
        public IObservable<Release> GetAll(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all <see cref="Release"/>s for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#list-releases-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Release> GetAll(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAll(repositoryId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all <see cref="Release"/>s for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#list-releases-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Release> GetAll(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Release>(ApiUrls.Releases(owner, name), options, cancellationToken);
        }

        /// <summary>
        /// Gets all <see cref="Release"/>s for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#list-releases-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Release> GetAll(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Release>(ApiUrls.Releases(repositoryId), options, cancellationToken);
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
        public IObservable<Release> Get(string owner, string name, long id, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, id, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets a single <see cref="Release"/> for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#get-a-release-by-tag-name">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="tag">The tag of the release</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Release> Get(string owner, string name, string tag, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(tag, nameof(tag));

            return _client.Get(owner, name, tag, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets a single <see cref="Release"/> for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#get-a-single-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the release</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Release> Get(long repositoryId, long id, CancellationToken cancellationToken = default)
        {
            return _client.Get(repositoryId, id, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets a single <see cref="Release"/> for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#get-a-release-by-tag-name">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="tag">The tag of the release</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Release> Get(long repositoryId, string tag, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(tag, nameof(tag));

            return _client.Get(repositoryId, tag, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets the latest <see cref="Release"/> for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/releases/#get-the-latest-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Release> GetLatest(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.GetLatest(owner, name, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets the latest <see cref="Release"/> for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/releases/#get-the-latest-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Release> GetLatest(long repositoryId, CancellationToken cancellationToken = default)
        {
            return _client.GetLatest(repositoryId, cancellationToken).ToObservable();
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
        public IObservable<Release> Create(string owner, string name, NewRelease data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(data, nameof(data));

            return _client.Create(owner, name, data, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Creates a new <see cref="Release"/> for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#create-a-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="data">A description of the release to create</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Release> Create(long repositoryId, NewRelease data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(data, nameof(data));

            return _client.Create(repositoryId, data, cancellationToken).ToObservable();
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
        public IObservable<Release> Edit(string owner, string name, long id, ReleaseUpdate data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(data, nameof(data));

            return _client.Edit(owner, name, id, data, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Edits an existing <see cref="Release"/> for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#edit-a-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the release</param>
        /// <param name="data">A description of the release to edit</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Release> Edit(long repositoryId, long id, ReleaseUpdate data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(data, nameof(data));

            return _client.Edit(repositoryId, id, data, cancellationToken).ToObservable();
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
        public IObservable<Unit> Delete(string owner, string name, long id, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Delete(owner, name, id, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes an existing <see cref="Release"/> for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#delete-a-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the release to delete</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<Unit> Delete(long repositoryId, long id, CancellationToken cancellationToken = default)
        {
            return _client.Delete(repositoryId, id, cancellationToken).ToObservable();
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
        public IObservable<ReleaseAsset> GetAllAssets(string owner, string name, long id, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllAssets(owner, name, id, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all <see cref="ReleaseAsset"/> for the specified release of the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#list-assets-for-a-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the <see cref="Release"/>.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<ReleaseAsset> GetAllAssets(long repositoryId, long id, CancellationToken cancellationToken = default)
        {
            return GetAllAssets(repositoryId, id, ApiOptions.None, cancellationToken);
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
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<ReleaseAsset> GetAllAssets(string owner, string name, long id, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<ReleaseAsset>(ApiUrls.ReleaseAssets(owner, name, id), options, cancellationToken);
        }

        /// <summary>
        /// Gets all <see cref="ReleaseAsset"/> for the specified release of the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#list-assets-for-a-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the <see cref="Release"/>.</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<ReleaseAsset> GetAllAssets(long repositoryId, long id, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<ReleaseAsset>(ApiUrls.ReleaseAssets(repositoryId, id), options, cancellationToken);
        }

        /// <summary>
        /// Gets the specified <see cref="ReleaseAsset"/> for the specified release of the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#get-a-single-release-asset">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="assetId">The id of the <see cref="ReleaseAsset"/></param>
        public IObservable<ReleaseAsset> GetAsset(string owner, string name, int assetId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(assetId, nameof(assetId));

            return _client.GetAsset(owner, name, assetId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets the specified <see cref="ReleaseAsset"/> for the specified release of the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#get-a-single-release-asset">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="assetId">The id of the <see cref="ReleaseAsset"/></param>
        public IObservable<ReleaseAsset> GetAsset(long repositoryId, int assetId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(assetId, nameof(assetId));

            return _client.GetAsset(repositoryId, assetId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Uploads a <see cref="ReleaseAsset"/> for the specified release.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#upload-a-release-asset">API documentation</a> for more information.
        /// </remarks>
        /// <param name="release">The <see cref="Release"/> to attach the uploaded asset to</param>
        /// <param name="data">Description of the asset with its data</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public IObservable<ReleaseAsset> UploadAsset(Release release, ReleaseAssetUpload data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(release, nameof(release));
            Ensure.ArgumentNotNull(data, nameof(data));

            return _client.UploadAsset(release, data, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Edits the <see cref="ReleaseAsset"/> for the specified release of the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#edit-a-release-asset">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="assetId">The id of the <see cref="ReleaseAsset"/></param>
        /// <param name="data">Description of the asset with its amended data</param>
        public IObservable<ReleaseAsset> EditAsset(string owner, string name, int assetId, ReleaseAssetUpdate data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(data, nameof(data));

            return _client.EditAsset(owner, name, assetId, data, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Edits the <see cref="ReleaseAsset"/> for the specified release of the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#edit-a-release-asset">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="assetId">The id of the <see cref="ReleaseAsset"/></param>
        /// <param name="data">Description of the asset with its amended data</param>
        public IObservable<ReleaseAsset> EditAsset(long repositoryId, int assetId, ReleaseAssetUpdate data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(data, nameof(data));

            return _client.EditAsset(repositoryId, assetId, data, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes the specified <see cref="ReleaseAsset"/> from the specified repository
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#delete-a-release-asset">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="assetId">The id of the <see cref="ReleaseAsset"/>.</param>
        public IObservable<Unit> DeleteAsset(string owner, string name, int assetId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.DeleteAsset(owner, name, assetId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes the specified <see cref="ReleaseAsset"/> from the specified repository
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#delete-a-release-asset">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="assetId">The id of the <see cref="ReleaseAsset"/>.</param>
        public IObservable<Unit> DeleteAsset(long repositoryId, int assetId, CancellationToken cancellationToken = default)
        {
            return _client.DeleteAsset(repositoryId, assetId, cancellationToken).ToObservable();
        }
    }
}
