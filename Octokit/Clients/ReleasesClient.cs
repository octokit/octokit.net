#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;

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
        /// <param name="connection">An API connection.</param>
        public ReleasesClient(IApiConnection connection) : base(connection)
        {
        }

        /// <summary>
        /// Gets all <see cref="Release"/>s for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#list-assets-for-a-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner.</param>
        /// <param name="name">The repository's name.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The list of <see cref="Release"/>s for the specified repository.</returns>
        public async Task<IReadOnlyList<Release>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "repository");

            var endpoint = "/repos/{0}/{1}/releases".FormatUri(owner, name);
            return await Client.GetAll<Release>(endpoint, null, "application/vnd.github.manifold-preview");
        }

        /// <summary>
        /// Creates a new <see cref="Release"/> for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#create-a-release">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The repository's owner.</param>
        /// <param name="name">The repository's name.</param
        /// <param name="data">A description of the release to create.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The created <see cref="Release"/>.</returns>
        public async Task<Release> CreateRelease(string owner, string name, ReleaseUpdate data)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "repository");
            Ensure.ArgumentNotNull(data, "data");

            var endpoint = "/repos/{0}/{1}/releases".FormatUri(owner, name);
            return await Client.Post<Release>(endpoint, data, "application/vnd.github.manifold-preview");
        }

        /// <summary>
        /// Uploads a <see cref="ReleaseAsset"/> for the specified release.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/releases/#upload-a-release-asset">API documentation</a> for more information.
        /// </remarks>
        /// <param name="release">The <see cref="Release"/> to attach the uploaded asset to.</param>
        /// <param name="data">Description of the asset with its data.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The created <see cref="ReleaseAsset"/>.</returns>
        public async Task<ReleaseAsset> UploadAsset(Release release, ReleaseAssetUpload data)
        {
            Ensure.ArgumentNotNull(release, "release");
            Ensure.ArgumentNotNull(data, "data");

            var endpoint = release.UploadUrl.ExpandUriTemplate(new {name = data.FileName});
            return await Client.Post<ReleaseAsset>(
                endpoint,
                data.RawData,
                "application/vnd.github.manifold-preview",
                data.ContentType);
        }
    }
}
