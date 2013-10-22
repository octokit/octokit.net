#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;

namespace Octokit
{
    public class ReleasesClient : ApiClient, IReleasesClient
    {
        public ReleasesClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public async Task<IReadOnlyList<Release>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "repository");

            var endpoint = ApiUrls.Releases(owner, name);
            return await ApiConnection.GetAll<Release>(endpoint, null, "application/vnd.github.manifold-preview");
        }

        public async Task<Release> CreateRelease(string owner, string name, ReleaseUpdate data)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "repository");
            Ensure.ArgumentNotNull(data, "data");

            var endpoint = ApiUrls.Releases(owner, name);
            return await ApiConnection.Post<Release>(endpoint, data, "application/vnd.github.manifold-preview");
        }

        public async Task<ReleaseAsset> UploadAsset(Release release, ReleaseAssetUpload data)
        {
            Ensure.ArgumentNotNull(release, "release");
            Ensure.ArgumentNotNull(data, "data");

            var endpoint = release.UploadUrl.ExpandUriTemplate(new {name = data.FileName});
            return await ApiConnection.Post<ReleaseAsset>(
                endpoint,
                data.RawData,
                "application/vnd.github.manifold-preview",
                data.ContentType);
        }
    }
}
