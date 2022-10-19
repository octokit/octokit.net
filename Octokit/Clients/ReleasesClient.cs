using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Releases API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/releases/">Releases API documentation</a> for more information.
    /// </remarks>
    public class ReleasesClient : ApiClient, IReleasesClient
    {
        /// <summary>
        /// Initializes a new GitHub Releases API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ReleasesClient(IApiConnection apiConnection) : base(apiConnection)
        {
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
        [ManualRoute("POST", "/repos/{owner}/{repo}/releases/generate-notes")]
        public Task<GeneratedReleaseNotes> GenerateReleaseNotes(string owner, string name, GenerateReleaseNotesRequest data)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(data, nameof(data));

            var endpoint = ApiUrls.ReleasesGenerateNotes(owner, name);
            return ApiConnection.Post<GeneratedReleaseNotes>(endpoint, data, AcceptHeaders.StableVersion);
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
        [ManualRoute("POST", "/repositories/{id}/releases/generate-notes")]
        public Task<GeneratedReleaseNotes> GenerateReleaseNotes(long repositoryId, GenerateReleaseNotesRequest data)
        {
            Ensure.ArgumentNotNull(data, nameof(data));

            var endpoint = ApiUrls.ReleasesGenerateNotes(repositoryId);
            return ApiConnection.Post<GeneratedReleaseNotes>(endpoint, data, AcceptHeaders.StableVersion);
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/releases")]
        public Task<IReadOnlyList<Release>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all <see cref="Release"/>s for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#list-releases-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/repositories/{id}/releases")]
        public Task<IReadOnlyList<Release>> GetAll(long repositoryId)
        {
            return GetAll(repositoryId, ApiOptions.None);
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/releases")]
        public Task<IReadOnlyList<Release>> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.Releases(owner, name);
            return ApiConnection.GetAll<Release>(endpoint, null, AcceptHeaders.StableVersion, options);
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
        [ManualRoute("GET", "/repositories/{id}/releases")]
        public Task<IReadOnlyList<Release>> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.Releases(repositoryId);
            return ApiConnection.GetAll<Release>(endpoint, null, AcceptHeaders.StableVersion, options);
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/releases/{release_id}")]
        public Task<Release> Get(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var endpoint = ApiUrls.Releases(owner, name, id);
            return ApiConnection.Get<Release>(endpoint);
        }

        /// <summary>
        /// Gets a single <see cref="Release"/> for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/releases/#get-a-release-by-tag-name">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner</param>
        /// <param name="name">The repository's name</param>
        /// <param name="tag">The tag of the release</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/repos/{owner}/{repo}/releases/tags/{tag}")]
        public Task<Release> Get(string owner, string name, string tag)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(tag, nameof(tag));

            var endpoint = ApiUrls.Releases(owner, name, tag);
            return ApiConnection.Get<Release>(endpoint);
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
        [ManualRoute("GET", "/repositories/{id}/releases/{id}")]
        public Task<Release> Get(long repositoryId, int id)
        {
            var endpoint = ApiUrls.Releases(repositoryId, id);
            return ApiConnection.Get<Release>(endpoint);
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
        [ManualRoute("GET", "/repositories/{id}/releases/tags/{tag}")]
        public Task<Release> Get(long repositoryId, string tag)
        {
            Ensure.ArgumentNotNullOrEmptyString(tag, nameof(tag));

            var endpoint = ApiUrls.Releases(repositoryId, tag);
            return ApiConnection.Get<Release>(endpoint);
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/releases/latest")]
        public Task<Release> GetLatest(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var endpoint = ApiUrls.LatestRelease(owner, name);
            return ApiConnection.Get<Release>(endpoint);
        }

        /// <summary>
        /// Gets the latest <see cref="Release"/> for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/releases/#get-the-latest-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/repositories/{id}/releases/latest")]
        public Task<Release> GetLatest(long repositoryId)
        {
            var endpoint = ApiUrls.LatestRelease(repositoryId);
            return ApiConnection.Get<Release>(endpoint);
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
        [ManualRoute("POST", "/repos/{owner}/{repo}/releases")]
        public Task<Release> Create(string owner, string name, NewRelease data)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(data, nameof(data));

            var endpoint = ApiUrls.Releases(owner, name);
            return ApiConnection.Post<Release>(endpoint, data, AcceptHeaders.StableVersion);
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
        [ManualRoute("POST", "/repositories/{id}/releases")]
        public Task<Release> Create(long repositoryId, NewRelease data)
        {
            Ensure.ArgumentNotNull(data, nameof(data));

            var endpoint = ApiUrls.Releases(repositoryId);
            return ApiConnection.Post<Release>(endpoint, data, AcceptHeaders.StableVersion);
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
        [ManualRoute("PATCH", "/repos/{owner}/{repo}/releases/{release_id}")]
        public Task<Release> Edit(string owner, string name, int id, ReleaseUpdate data)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(data, nameof(data));

            var endpoint = ApiUrls.Releases(owner, name, id);
            return ApiConnection.Patch<Release>(endpoint, data);
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
        [ManualRoute("PATCH", "/repositories/{id}/releases/{id}")]
        public Task<Release> Edit(long repositoryId, int id, ReleaseUpdate data)
        {
            Ensure.ArgumentNotNull(data, nameof(data));

            var endpoint = ApiUrls.Releases(repositoryId, id);
            return ApiConnection.Patch<Release>(endpoint, data);
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
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/releases/{release_id}")]
        public Task Delete(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var endpoint = ApiUrls.Releases(owner, name, id);
            return ApiConnection.Delete(endpoint);
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
        [ManualRoute("DELETE", "/repositories/{id}/releases/{id}")]
        public Task Delete(long repositoryId, int id)
        {
            var endpoint = ApiUrls.Releases(repositoryId, id);
            return ApiConnection.Delete(endpoint);
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/releases/{release_id}/assets")]
        public Task<IReadOnlyList<ReleaseAsset>> GetAllAssets(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllAssets(owner, name, id, ApiOptions.None);
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
        [ManualRoute("GET", "/repositories/{id}/releases/{id}/assets")]
        public Task<IReadOnlyList<ReleaseAsset>> GetAllAssets(long repositoryId, int id)
        {
            return GetAllAssets(repositoryId, id, ApiOptions.None);
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/releases/{release_id}/assets")]
        public Task<IReadOnlyList<ReleaseAsset>> GetAllAssets(string owner, string name, int id, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.ReleaseAssets(owner, name, id);
            return ApiConnection.GetAll<ReleaseAsset>(endpoint, null, AcceptHeaders.StableVersion, options);
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
        [ManualRoute("GET", "/repositories/{id}/releases/{id}/assets")]
        public Task<IReadOnlyList<ReleaseAsset>> GetAllAssets(long repositoryId, int id, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.ReleaseAssets(repositoryId, id);
            return ApiConnection.GetAll<ReleaseAsset>(endpoint, null, AcceptHeaders.StableVersion, options);
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
        [ManualRoute("POST", "{server}/repos/{owner}/{repo}/releases/{release_id}/assets")]
        public Task<ReleaseAsset> UploadAsset(Release release, ReleaseAssetUpload data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(release, nameof(release));
            Ensure.ArgumentNotNull(data, nameof(data));

            var endpoint = release.UploadUrl.ExpandUriTemplate(new { name = data.FileName });

            if (data.Timeout.HasValue)
            {
                return ApiConnection.Post<ReleaseAsset>(
                    endpoint,
                    data.RawData,
                    AcceptHeaders.StableVersion,
                    data.ContentType,
                    data.Timeout.GetValueOrDefault(),
                    cancellationToken);
            }

            return ApiConnection.Post<ReleaseAsset>(
                endpoint,
                data.RawData,
                AcceptHeaders.StableVersion,
                data.ContentType,
                cancellationToken);
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/releases/assets/{asset_id}")]
        public Task<ReleaseAsset> GetAsset(string owner, string name, int assetId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var endpoint = ApiUrls.Asset(owner, name, assetId);
            return ApiConnection.Get<ReleaseAsset>(endpoint);
        }

        /// <summary>
        /// Gets the specified <see cref="ReleaseAsset"/> for the specified release of the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#get-a-single-release-asset">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="assetId">The id of the <see cref="ReleaseAsset"/></param>
        [ManualRoute("GET", "/repositories/{id}/releases/assets/{asset_id}")]
        public Task<ReleaseAsset> GetAsset(long repositoryId, int assetId)
        {
            var endpoint = ApiUrls.Asset(repositoryId, assetId);
            return ApiConnection.Get<ReleaseAsset>(endpoint);
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
        [ManualRoute("PATCH", "/repos/{owner}/{repo}/releases/assets/{asset_id}")]
        public Task<ReleaseAsset> EditAsset(string owner, string name, int assetId, ReleaseAssetUpdate data)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(data, nameof(data));

            var endpoint = ApiUrls.Asset(owner, name, assetId);
            return ApiConnection.Patch<ReleaseAsset>(endpoint, data);
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
        [ManualRoute("PATCH", "/repositories/{id}/releases/assets/{asset_id}")]
        public Task<ReleaseAsset> EditAsset(long repositoryId, int assetId, ReleaseAssetUpdate data)
        {
            Ensure.ArgumentNotNull(data, nameof(data));

            var endpoint = ApiUrls.Asset(repositoryId, assetId);
            return ApiConnection.Patch<ReleaseAsset>(endpoint, data);
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
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/releases/assets/{asset_id}")]
        public Task DeleteAsset(string owner, string name, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var endpoint = ApiUrls.Asset(owner, name, id);
            return ApiConnection.Delete(endpoint);
        }

        /// <summary>
        /// Deletes the specified <see cref="ReleaseAsset"/> from the specified repository
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#delete-a-release-asset">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="id">The id of the <see cref="ReleaseAsset"/>.</param>
        [ManualRoute("DELETE", "/repositories/{id}/releases/assets/{asset_id}")]
        public Task DeleteAsset(long repositoryId, int id)
        {
            var endpoint = ApiUrls.Asset(repositoryId, id);
            return ApiConnection.Delete(endpoint);
        }
    }
}
