using Octokit.Reactive.Internal;
using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservablePackageVersionsClient : IObservablePackageVersionsClient
    {
        readonly IPackageVersionsClient _client;
        readonly IConnection _connection;

        public ObservablePackageVersionsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Packages.PackageVersions;
            _connection = client.Connection;
        }

        /// <summary>
        /// List all versions of a package.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-all-package-versions-for-a-package-owned-by-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="state">Optional: Return packages with a state. Defaults to Active</param>
        /// <param name="options">Optional: Paging options</param>
        public IObservable<PackageVersion> GetAll(string org, PackageType packageType, string packageName, PackageVersionState state = PackageVersionState.Active, ApiOptions options = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.ApiOptionsNotNull(ref options);

            var route = ApiUrls.PackageVersions(org, packageType, packageName);
            var parameters = ParameterBuilder.AddParameter("state", state);

            return _connection.GetAndFlattenAllPages<PackageVersion>(route, parameters);
        }

        /// <summary>
        /// Get a specific version of a package.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-a-package-version-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        public IObservable<PackageVersion> Get(string org, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            return _client.Get(org, packageType, packageName, packageVersionId).ToObservable();
        }

        /// <summary>
        /// Deletes a specific package version in an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#delete-package-version-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        public IObservable<Unit> Delete(string org, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            return _client.Delete(org, packageType, packageName, packageVersionId).ToObservable();
        }

        /// <summary>
        /// Restores a specific package version in an organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#restore-package-version-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        public IObservable<Unit> Restore(string org, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));


            return _client.Restore(org, packageType, packageName, packageVersionId).ToObservable();
        }
    }
}