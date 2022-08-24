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

        public IObservablePackageVersionsClient PackageVersions { get; private set; }

        /// <summary>
        /// List all packages for an organisations, readable by the current user
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#list-packages-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageVisibility">Optional: The visibility of the package</param>
        public IObservable<Package> GetAllForOrg(string org, PackageType packageType, PackageVisibility? packageVisibility = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));

            var route = ApiUrls.PackagesOrg(org);
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
        public IObservable<Package> GetForOrg(string org, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            return _client.GetForOrg(org, packageType, packageName).ToObservable();
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
        public IObservable<Unit> DeleteForOrg(string org, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            return _client.DeleteForOrg(org, packageType, packageName).ToObservable();
        }

        /// <summary>
        /// Restore a specific package for an Organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#restore-a-package-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        public IObservable<Unit> RestoreForOrg(string org, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            return _client.RestoreForOrg(org, packageType, packageName).ToObservable();
        }

        /// <summary>
        /// Lists packages owned by the authenticated user within the user's namespace
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#list-packages-for-the-authenticated-users-namespace">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageVisibility">Optional: The visibility of the package</param>
        public IObservable<Package> GetAllForActiveUser(PackageType packageType, PackageVisibility? packageVisibility = null)
        {
            var route = ApiUrls.PackagesActiveUser();
            var parameters = ParameterBuilder.AddParameter("package_type", packageType).AddOptionalParameter("visibility", packageVisibility);

            return _connection.GetAndFlattenAllPages<Package>(route, parameters);
        }

        /// <summary>
        /// Gets a specific package for a package owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-a-package-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        public IObservable<Package> GetForActiveUser(PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            return _client.GetForActiveUser(packageType, packageName).ToObservable();
        }

        /// <summary>
        /// Deletes a package owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#delete-a-package-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        public IObservable<Unit> DeleteForActiveUser(PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            return _client.DeleteForActiveUser(packageType, packageName).ToObservable();
        }

        /// <summary>
        /// Restores a package owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#restore-a-package-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        public IObservable<Unit> RestoreForActiveUser(PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            return _client.RestoreForActiveUser(packageType, packageName).ToObservable();
        }

        /// <summary>
        /// Lists packages owned by the authenticated user within the user's namespace
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#list-packages-for-the-authenticated-users-namespace">API documentation</a> for more details
        /// </remarks>
        /// <param name="username">Required: Username</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageVisibility">Optional: The visibility of the package</param>
        public IObservable<Package> GetAllForUser(string username, PackageType packageType, PackageVisibility? packageVisibility = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(username, nameof(username));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));

            var route = ApiUrls.PackagesUser(username);
            var parameters = ParameterBuilder.AddParameter("package_type", packageType).AddOptionalParameter("visibility", packageVisibility);

            return _connection.GetAndFlattenAllPages<Package>(route, parameters);
        }

        /// <summary>
        /// Gets a specific package metadata for a public package owned by a user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-a-package-for-a-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="username">Required: Username</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        public IObservable<Package> GetForUser(string username, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(username, nameof(username));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            return _client.GetForUser(username, packageType, packageName).ToObservable();
        }

        /// <summary>
        /// Deletes an entire package for a user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#delete-a-package-for-a-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="username">Required: Username</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        public IObservable<Unit> DeleteForUser(string username, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(username, nameof(username));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            return _client.DeleteForUser(username, packageType, packageName).ToObservable();
        }

        /// <summary>
        /// Restores an entire package for a user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#restore-a-package-for-a-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="username">Required: Username</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        public IObservable<Unit> RestoreForUser(string username, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(username, nameof(username));
            Ensure.ArgumentNotNull(packageType, nameof(packageType));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            return _client.RestoreForUser(username, packageType, packageName).ToObservable();
        }
    }
}