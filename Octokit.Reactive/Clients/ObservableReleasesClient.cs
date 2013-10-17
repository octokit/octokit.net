using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Helpers;

namespace Octokit.Reactive.Clients
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

        public IObservable<Release> GetAll(string owner, string name)
        {
            return _connection.GetAndFlattenAllPages<Release>(ApiUrls.Releases(owner, name));
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
