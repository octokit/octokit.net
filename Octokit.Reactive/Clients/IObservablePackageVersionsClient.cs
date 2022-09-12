using System;
using System.Reactive;

namespace Octokit.Reactive
{
    public interface IObservablePackageVersionsClient
    {
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
        IObservable<PackageVersion> GetAllForOrg(string org, PackageType packageType, string packageName, PackageVersionState state = PackageVersionState.Active, ApiOptions options = null);

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

        IObservable<PackageVersion> GetForOrg(string org, PackageType packageType, string packageName, int packageVersionId);

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
        IObservable<Unit> DeleteForOrg(string org, PackageType packageType, string packageName, int packageVersionId);

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
        IObservable<Unit> RestoreForOrg(string org, PackageType packageType, string packageName, int packageVersionId);

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
        IObservable<PackageVersion> GetAllForActiveUser(PackageType packageType, string packageName, PackageVersionState state = PackageVersionState.Active, ApiOptions options = null);

        /// <summary>
        /// Gets a specific package version for a package owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-a-package-version-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        IObservable<PackageVersion> GetForActiveUser(PackageType packageType, string packageName, int packageVersionId);

        /// <summary>
        /// Deletes a specific package version for a package owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#delete-a-package-version-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        IObservable<Unit> DeleteForActiveUser(PackageType packageType, string packageName, int packageVersionId);

        /// <summary>
        /// Restores a package version owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#restore-a-package-version-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        /// <param name="packageVersionId">Required: The id of the package version</param>
        IObservable<Unit> RestoreForActiveUser(PackageType packageType, string packageName, int packageVersionId);

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
        IObservable<PackageVersion> GetAllForUser(string username, PackageType packageType, string packageName, PackageVersionState state = PackageVersionState.Active, ApiOptions options = null);

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
        IObservable<PackageVersion> GetForUser(string username, PackageType packageType, string packageName, int packageVersionId);

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
        IObservable<Unit> DeleteForUser(string username, PackageType packageType, string packageName, int packageVersionId);

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
        IObservable<Unit> RestoreForUser(string username, PackageType packageType, string packageName, int packageVersionId);
    }
}