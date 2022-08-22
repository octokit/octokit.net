using System;
using System.Reactive;

namespace Octokit.Reactive
{
    public interface IObservablePackageVersionsClient
    {
        IObservable<Unit> Delete(string org, PackageType packageType, string packageName, int packageVersionId);
        IObservable<PackageVersion> Get(string org, PackageType packageType, string packageName, int packageVersionId);
        IObservable<PackageVersion> GetAll(string org, PackageType packageType, string packageName, PackageVersionState state = PackageVersionState.Active, ApiOptions options = null);
        IObservable<Unit> Restore(string org, PackageType packageType, string packageName, int packageVersionId);
    }
}