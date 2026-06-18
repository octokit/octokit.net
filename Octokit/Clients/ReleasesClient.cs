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

        /// <inheritdoc/>
        [ManualRoute("POST", "/repos/{owner}/{repo}/releases/generate-notes")]
        public Task<GeneratedReleaseNotes> GenerateReleaseNotes(string owner, string name, GenerateReleaseNotesRequest data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(data, nameof(data));

            var endpoint = ApiUrls.ReleasesGenerateNotes(owner, name);
            return ApiConnection.Post<GeneratedReleaseNotes>(endpoint, data, AcceptHeaders.StableVersion, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("POST", "/repositories/{id}/releases/generate-notes")]
        public Task<GeneratedReleaseNotes> GenerateReleaseNotes(long repositoryId, GenerateReleaseNotesRequest data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(data, nameof(data));

            var endpoint = ApiUrls.ReleasesGenerateNotes(repositoryId);
            return ApiConnection.Post<GeneratedReleaseNotes>(endpoint, data, AcceptHeaders.StableVersion, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repos/{owner}/{repo}/releases")]
        public Task<IReadOnlyList<Release>> GetAll(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repositories/{id}/releases")]
        public Task<IReadOnlyList<Release>> GetAll(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAll(repositoryId, ApiOptions.None, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repos/{owner}/{repo}/releases")]
        public Task<IReadOnlyList<Release>> GetAll(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.Releases(owner, name);
            return ApiConnection.GetAll<Release>(endpoint, null, AcceptHeaders.StableVersion, options, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repositories/{id}/releases")]
        public Task<IReadOnlyList<Release>> GetAll(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.Releases(repositoryId);
            return ApiConnection.GetAll<Release>(endpoint, null, AcceptHeaders.StableVersion, options, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repos/{owner}/{repo}/releases/{release_id}")]
        public Task<Release> Get(string owner, string name, long id, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var endpoint = ApiUrls.Releases(owner, name, id);
            return ApiConnection.Get<Release>(endpoint, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repos/{owner}/{repo}/releases/tags/{tag}")]
        public Task<Release> Get(string owner, string name, string tag, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(tag, nameof(tag));

            var endpoint = ApiUrls.Releases(owner, name, tag);
            return ApiConnection.Get<Release>(endpoint, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repositories/{id}/releases/{id}")]
        public Task<Release> Get(long repositoryId, long id, CancellationToken cancellationToken = default)
        {
            var endpoint = ApiUrls.Releases(repositoryId, id);
            return ApiConnection.Get<Release>(endpoint, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repositories/{id}/releases/tags/{tag}")]
        public Task<Release> Get(long repositoryId, string tag, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(tag, nameof(tag));

            var endpoint = ApiUrls.Releases(repositoryId, tag);
            return ApiConnection.Get<Release>(endpoint, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repos/{owner}/{repo}/releases/latest")]
        public Task<Release> GetLatest(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var endpoint = ApiUrls.LatestRelease(owner, name);
            return ApiConnection.Get<Release>(endpoint, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repositories/{id}/releases/latest")]
        public Task<Release> GetLatest(long repositoryId, CancellationToken cancellationToken = default)
        {
            var endpoint = ApiUrls.LatestRelease(repositoryId);
            return ApiConnection.Get<Release>(endpoint, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("POST", "/repos/{owner}/{repo}/releases")]
        public Task<Release> Create(string owner, string name, NewRelease data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(data, nameof(data));

            var endpoint = ApiUrls.Releases(owner, name);
            return ApiConnection.Post<Release>(endpoint, data, AcceptHeaders.StableVersion, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("POST", "/repositories/{id}/releases")]
        public Task<Release> Create(long repositoryId, NewRelease data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(data, nameof(data));

            var endpoint = ApiUrls.Releases(repositoryId);
            return ApiConnection.Post<Release>(endpoint, data, AcceptHeaders.StableVersion, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("PATCH", "/repos/{owner}/{repo}/releases/{release_id}")]
        public Task<Release> Edit(string owner, string name, long id, ReleaseUpdate data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(data, nameof(data));

            var endpoint = ApiUrls.Releases(owner, name, id);
            return ApiConnection.Patch<Release>(endpoint, data, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("PATCH", "/repositories/{id}/releases/{id}")]
        public Task<Release> Edit(long repositoryId, long id, ReleaseUpdate data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(data, nameof(data));

            var endpoint = ApiUrls.Releases(repositoryId, id);
            return ApiConnection.Patch<Release>(endpoint, data, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/releases/{release_id}")]
        public Task Delete(string owner, string name, long id, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var endpoint = ApiUrls.Releases(owner, name, id);
            return ApiConnection.Delete(endpoint, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("DELETE", "/repositories/{id}/releases/{id}")]
        public Task Delete(long repositoryId, long id, CancellationToken cancellationToken = default)
        {
            var endpoint = ApiUrls.Releases(repositoryId, id);
            return ApiConnection.Delete(endpoint, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repos/{owner}/{repo}/releases/{release_id}/assets")]
        public Task<IReadOnlyList<ReleaseAsset>> GetAllAssets(string owner, string name, long id, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllAssets(owner, name, id, ApiOptions.None, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repositories/{id}/releases/{id}/assets")]
        public Task<IReadOnlyList<ReleaseAsset>> GetAllAssets(long repositoryId, long id, CancellationToken cancellationToken = default)
        {
            return GetAllAssets(repositoryId, id, ApiOptions.None, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repos/{owner}/{repo}/releases/{release_id}/assets")]
        public Task<IReadOnlyList<ReleaseAsset>> GetAllAssets(string owner, string name, long id, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.ReleaseAssets(owner, name, id);
            return ApiConnection.GetAll<ReleaseAsset>(endpoint, null, AcceptHeaders.StableVersion, options, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repositories/{id}/releases/{id}/assets")]
        public Task<IReadOnlyList<ReleaseAsset>> GetAllAssets(long repositoryId, long id, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.ReleaseAssets(repositoryId, id);
            return ApiConnection.GetAll<ReleaseAsset>(endpoint, null, AcceptHeaders.StableVersion, options, cancellationToken);
        }

        /// <inheritdoc/>
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
                endpoint, data.RawData,
                AcceptHeaders.StableVersion,
                data.ContentType,
                cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repos/{owner}/{repo}/releases/assets/{asset_id}")]
        public Task<ReleaseAsset> GetAsset(string owner, string name, int assetId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var endpoint = ApiUrls.Asset(owner, name, assetId);
            return ApiConnection.Get<ReleaseAsset>(endpoint, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("GET", "/repositories/{id}/releases/assets/{asset_id}")]
        public Task<ReleaseAsset> GetAsset(long repositoryId, int assetId, CancellationToken cancellationToken = default)
        {
            var endpoint = ApiUrls.Asset(repositoryId, assetId);
            return ApiConnection.Get<ReleaseAsset>(endpoint, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("PATCH", "/repos/{owner}/{repo}/releases/assets/{asset_id}")]
        public Task<ReleaseAsset> EditAsset(string owner, string name, int assetId, ReleaseAssetUpdate data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(data, nameof(data));

            var endpoint = ApiUrls.Asset(owner, name, assetId);
            return ApiConnection.Patch<ReleaseAsset>(endpoint, data, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("PATCH", "/repositories/{id}/releases/assets/{asset_id}")]
        public Task<ReleaseAsset> EditAsset(long repositoryId, int assetId, ReleaseAssetUpdate data, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(data, nameof(data));

            var endpoint = ApiUrls.Asset(repositoryId, assetId);
            return ApiConnection.Patch<ReleaseAsset>(endpoint, data, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/releases/assets/{asset_id}")]
        public Task DeleteAsset(string owner, string name, int assetId, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            var endpoint = ApiUrls.Asset(owner, name, assetId);
            return ApiConnection.Delete(endpoint, cancellationToken);
        }

        /// <inheritdoc/>
        [ManualRoute("DELETE", "/repositories/{id}/releases/assets/{asset_id}")]
        public Task DeleteAsset(long repositoryId, int assetId, CancellationToken cancellationToken = default)
        {
            var endpoint = ApiUrls.Asset(repositoryId, assetId);
            return ApiConnection.Delete(endpoint, cancellationToken);
        }
    }
}
