#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
{
    public class ReleasesClient : ApiClient<Release>, IReleasesClient
    {
        public ReleasesClient(IApiConnection<Release> client) : base(client)
        {
        }

        public async Task<IReadOnlyList<Release>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "repository");

            var endpoint = "/repos/{0}/{1}/releases".FormatUri(owner, name);
            return await Client.GetAll(endpoint);
        }


        public async Task<Release> CreateRelease(string owner, string name, ReleaseUpdate data)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "repository");
            Ensure.ArgumentNotNull(data, "data");

            var endpoint = "/repos/{0}/{1}/releases".FormatUri(owner, name);
            return await Client.Create(endpoint, data);
        }


        public async Task<ReleaseAsset> UploadAsset(Release release, ReleaseAssetUpload data)
        {
            Ensure.ArgumentNotNull(release, "release");
            Ensure.ArgumentNotNull(data, "data");

            var endpoint = release.UploadUrl.ExpandUriTemplate(new { name = data.FileName });
            return await Client.Upload<ReleaseAsset>(endpoint, data.RawData, data.ContentType);
        }
    }
}
