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
        public IObservable<PackageVersion> GetAllForOrg(string org, PackageType packageType, string packageName, PackageVersionState state = PackageVersionState.Active, ApiOptions options = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.ApiOptionsNotNull(ref options);

            var route = ApiUrls.PackageVersionsOrg(org, packageType, packageName);
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
        public IObservable<PackageVersion> GetForOrg(string org, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            return _client.GetForOrg(org, packageType, packageName, packageVersionId).ToObservable();
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
        public IObservable<Unit> DeleteForOrg(string org, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            return _client.DeleteForOrg(org, packageType, packageName, packageVersionId).ToObservable();
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
        public IObservable<Unit> RestoreForOrg(string org, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            return _client.RestoreForOrg(org, packageType, packageName, packageVersionId).ToObservable();
        }

        /// <summary>
        /// Returns all package versions for a package owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-all-package-versions-for-a-package-owned-by-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="state">Optional: Return packages with a state. Defaults to Active</param>
        /// <param name="options">Optional: Paging options</param>
        public IObservable<PackageVersion> GetAllForActiveUser(PackageType packageType, string packageName, PackageVersionState state = PackageVersionState.Active, ApiOptions options = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.ApiOptionsNotNull(ref options);

            var route = ApiUrls.PackageVersionsActiveUser(packageType, packageName);
            var parameters = ParameterBuilder.AddParameter("state", state);

            return _connection.GetAndFlattenAllPages<PackageVersion>(route, parameters);
        }

        /// <summary>
        /// Gets a specific package version for a package owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-a-package-version-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        public IObservable<PackageVersion> GetForActiveUser(PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            return _client.GetForActiveUser(packageType, packageName, packageVersionId).ToObservable();
        }

        /// <summary>
        /// Deletes a specific package version for a package owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#delete-a-package-version-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        public IObservable<Unit> DeleteForActiveUser(PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            return _client.DeleteForActiveUser(packageType, packageName, packageVersionId).ToObservable();
        }

        /// <summary>
        /// Restores a package version owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#restore-a-package-version-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        public IObservable<Unit> RestoreForActiveUser(PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            return _client.RestoreForActiveUser(packageType, packageName, packageVersionId).ToObservable();
        }

        /// <summary>
        /// Returns all package versions for a public package owned by a specified user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-all-package-versions-for-a-package-owned-by-a-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="username">Required: Username</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="state">Optional: Return packages with a state. Defaults to Active</param>
        /// <param name="options">Optional: Paging options</param>
        public IObservable<PackageVersion> GetAllForUser(string username, PackageType packageType, string packageName, PackageVersionState state = PackageVersionState.Active, ApiOptions options = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(username, nameof(username));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.ApiOptionsNotNull(ref options);

            var route = ApiUrls.PackageVersionsUser(username, packageType, packageName);
            var parameters = ParameterBuilder.AddParameter("state", state);

            return _connection.GetAndFlattenAllPages<PackageVersion>(route, parameters);
        }

        /// <summary>
        /// Gets a specific package version for a public package owned by a specified user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-a-package-version-for-a-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="username">Required: Username</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        public IObservable<PackageVersion> GetForUser(string username, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(username, nameof(username));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            return _client.GetForUser(username, packageType, packageName, packageVersionId).ToObservable();
        }

        /// <summary>
        /// Deletes a specific package version for a user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#delete-package-version-for-a-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="username">Required: Username</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        public IObservable<Unit> DeleteForUser(string username, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(username, nameof(username));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            return _client.DeleteForUser(username, packageType, packageName, packageVersionId).ToObservable();
        }

        /// <summary>
        /// Restores a specific package version for a user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#restore-package-version-for-a-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="username">Required: Username</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        public IObservable<Unit> RestoreForUser(string username, PackageType packageType, string packageName, int packageVersionId)
        {
            Ensure.ArgumentNotNullOrEmptyString(username, nameof(username));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));
            Ensure.GreaterThanZero(packageVersionId, nameof(packageVersionId));

            return _client.RestoreForUser(username, packageType, packageName, packageVersionId).ToObservable();
        }
    }
}