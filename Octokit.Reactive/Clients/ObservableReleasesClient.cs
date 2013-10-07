using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Clients
{
    public class ObservableReleasesClient : IObservableReleasesClient
    {
        readonly IReleasesClient _client;

        public ObservableReleasesClient(IReleasesClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client;
        }

        public IObservable<IReadOnlyList<Release>> GetAll(string owner, string name)
        {
            return _client.GetAll(owner, name).ToObservable();
        }

        public IObservable<Release> CreateRelease(string owner, string name, ReleaseUpdate data)
        {
            return _client.CreateRelease(owner, name, data).ToObservable();
        }

        public IObservable<ReleaseAsset> UploadAsset(Release release, ReleaseAssetUpload data)
        {
            return _client.UploadAsset(release, data).ToObservable();
        }
    }
}
