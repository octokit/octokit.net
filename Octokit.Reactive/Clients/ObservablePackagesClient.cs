using Octokit.Reactive.Internal;
using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservablePackagesClient : IObservablePackagesClient
    {
        readonly IPackagesClient _client;
        readonly IConnection _connection;

        public ObservablePackagesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Packages;
            _connection = client.Connection;
        }

        /// <summary>
        /// List all packages for an organisations, readable by the current user
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#list-packages-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageVisibility">Optional: The visibility of the package</param>
        public IObservable<Package> GetAll(string org, PackageType packageType, PackageVisibility? packageVisibility = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));

            var route = ApiUrls.Packages(org);
            var parameters = ParameterBuilder.AddParameter("package_type", packageType).AddOptionalParameter("visibility", packageVisibility);

            return _connection.GetAndFlattenAllPages<Package>(route, parameters);
        }

        /// <summary>
        /// Get a specific package for an Organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-a-package-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        public IObservable<Package> Get(string org, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            return _client.Get(org, packageType, packageName).ToObservable();
        }

        /// <summary>
        /// Delete a specific package for an Organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#delete-a-package-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        public IObservable<Unit> Delete(string org, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            return _client.Delete(org, packageType, packageName).ToObservable();
        }

        public IObservable<Unit> Restore(string org, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            return _client.Restore(org, packageType, packageName).ToObservable();
        }
    }
}