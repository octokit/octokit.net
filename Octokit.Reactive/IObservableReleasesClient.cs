using System;

namespace Octokit.Reactive
{
    public interface IObservableReleasesClient
    {
        IObservable<IReadOnlyList<Release>> GetAll(string owner, string name);
        IObservable<Release> CreateRelease(string owner, string name, ReleaseUpdate data);
        IObservable<ReleaseAsset> UploadAsset(Release release, ReleaseAssetUpload data);
    }
}
