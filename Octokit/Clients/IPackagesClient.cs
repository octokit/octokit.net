using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IPackagesClient
    {
        IPackageVersionsClient PackageVersions { get; }

        /// <summary>
        /// List all packages for an organisations, readable by the current user
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#list-packages-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageVisibility">Optional: The visibility of the package</param>
        [ExcludeFromPaginationApiOptionsConventionTest("No api options available according to the documentation")]
        Task<IReadOnlyList<Package>> GetAllForOrg(string org, PackageType packageType, PackageVisibility? packageVisibility = null);

        /// <summary>
        /// Get a specific package for an Organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-a-package-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        Task<Package> GetForOrg(string org, PackageType packageType, string packageName);

        /// <summary>
        /// Delete a specific package for an Organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#delete-a-package-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        Task DeleteForOrg(string org, PackageType packageType, string packageName);

        /// <summary>
        /// Restore a specific package for an Organization.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#restore-a-package-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        Task RestoreForOrg(string org, PackageType packageType, string packageName);

        /// <summary>
        /// Lists packages owned by the authenticated user within the user's namespace
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#list-packages-for-the-authenticated-users-namespace">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageVisibility">Optional: The visibility of the package</param>
        [ExcludeFromPaginationApiOptionsConventionTest("No api options available according to the documentation")]
        Task<IReadOnlyList<Package>> GetAllForActiveUser(PackageType packageType, PackageVisibility? packageVisibility = null);

        /// <summary>
        /// Gets a specific package for a package owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-a-package-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        Task<Package> GetForActiveUser(PackageType packageType, string packageName);

        /// <summary>
        /// Deletes a package owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#delete-a-package-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        Task DeleteForActiveUser(PackageType packageType, string packageName);

        /// <summary>
        /// Restores a package owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#restore-a-package-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        Task RestoreForActiveUser(PackageType packageType, string packageName);

        /// <summary>
        /// Lists packages owned by the authenticated user within the user's namespace
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#list-packages-for-the-authenticated-users-namespace">API documentation</a> for more details
        /// </remarks>
        /// <param name="username">Required: Username</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageVisibility">Optional: The visibility of the package</param>
        [ExcludeFromPaginationApiOptionsConventionTest("No api options available according to the documentation")]
        Task<IReadOnlyList<Package>> GetAllForUser(string username, PackageType packageType, PackageVisibility? packageVisibility = null);

        /// <summary>
        /// Gets a specific package metadata for a public package owned by a user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-a-package-for-a-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="username">Required: Username</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        Task<Package> GetForUser(string username, PackageType packageType, string packageName);

        /// <summary>
        /// Deletes an entire package for a user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#delete-a-package-for-a-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="username">Required: Username</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        Task DeleteForUser(string username, PackageType packageType, string packageName);

        /// <summary>
        /// Restores an entire package for a user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#restore-a-package-for-a-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="username">Required: Username</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        Task RestoreForUser(string username, PackageType packageType, string packageName);
    }
}