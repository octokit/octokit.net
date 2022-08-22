using System;
using System.Reactive;

namespace Octokit.Reactive
{
    public interface IObservablePackagesClient
    {
        IObservablePackageVersionsClient PackageVersions { get; }

        IObservable<Unit> Delete(string org, PackageType packageType, string packageName);
        
        IObservable<Package> Get(string org, PackageType packageType, string packageName);
        
        IObservable<Package> GetAll(string org, PackageType packageType, PackageVisibility? packageVisibility = null);
        
        IObservable<Unit> Restore(string org, PackageType packageType, string packageName);
    }
}