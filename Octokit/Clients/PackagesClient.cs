using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Packages API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/rest/packages">Packages API documentation</a> for more details.
    /// </remarks>
    public class PackagesClient : ApiClient, IPackagesClient
    {
        public PackagesClient(IApiConnection apiConnection) : base(apiConnection)
        {
            PackageVersions = new PackageVersionsClient(apiConnection);
        }

        public IPackageVersionsClient PackageVersions { get; private set; }

        #region Organization
        /// <summary>
        /// List all packages for an organisations, readable by the current user
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#list-packages-for-an-organization">API documentation</a> for more details
        /// </remarks>
        /// <param name="org">Required: Organisation Name</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageVisibility">Optional: The visibility of the package</param>
        [ManualRoute("GET", "/orgs/{org}/packages")]
        public Task<IReadOnlyList<Package>> GetAllForOrg(string org, PackageType packageType, PackageVisibility? packageVisibility = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            var route = ApiUrls.PackagesOrg(org);
            var parameters = ParameterBuilder.AddParameter("package_type", packageType).AddOptionalParameter("visibility", packageVisibility);

            return ApiConnection.GetAll<Package>(route, parameters);
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
        [ManualRoute("GET", "/orgs/{org}/packages/{package_type}/{package_name}")]
        public Task<Package> GetForOrg(string org, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            var route = ApiUrls.PackageOrg(org, packageType, packageName);

            return ApiConnection.Get<Package>(route);
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
        [ManualRoute("DELETE", "/orgs/{org}/packages/{package_type}/{package_name}")]
        public Task DeleteForOrg(string org, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            var route = ApiUrls.PackageOrg(org, packageType, packageName);

            return ApiConnection.Delete(route);
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
        [ManualRoute("POST", "/orgs/{org}/packages/{package_type}/{package_name}/restore")]
        public Task RestoreForOrg(string org, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            var route = ApiUrls.PackageRestoreOrg(org, packageType, packageName);

            return ApiConnection.Post(route);
        }
        #endregion

        #region Active User
        /// <summary>
        /// Lists packages owned by the authenticated user within the user's namespace
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#list-packages-for-the-authenticated-users-namespace">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageVisibility">Optional: The visibility of the package</param>
        [ManualRoute("GET", "/user/packages")]
        public Task<IReadOnlyList<Package>> GetAllForActiveUser(PackageType packageType, PackageVisibility? packageVisibility = null)
        {
            var route = ApiUrls.PackagesActiveUser();
            var parameters = ParameterBuilder.AddParameter("package_type", packageType).AddOptionalParameter("visibility", packageVisibility);

            return ApiConnection.GetAll<Package>(route, parameters);
        }

        /// <summary>
        /// Gets a specific package for a package owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#get-a-package-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        [ManualRoute("GET", "/user/packages/{package_type}/{package_name}")]
        public Task<Package> GetForActiveUser(PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            var route = ApiUrls.PackageActiveUser(packageType, packageName);

            return ApiConnection.Get<Package>(route);
        }

        /// <summary>
        /// Deletes a package owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#delete-a-package-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        [ManualRoute("DELETE", "/user/packages/{package_type}/{package_name}")]
        public Task DeleteForActiveUser(PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            var route = ApiUrls.PackageActiveUser(packageType, packageName);

            return ApiConnection.Delete(route);
        }

        /// <summary>
        /// Restores a package owned by the authenticated user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#restore-a-package-for-the-authenticated-user">API documentation</a> for more details
        /// </remarks>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageName">Required: The name of the package</param>
        [ManualRoute("POST", "/user/packages/{package_type}/{package_name}/restore")]
        public Task RestoreForActiveUser(PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            var route = ApiUrls.PackageRestoreActiveUser(packageType, packageName);

            return ApiConnection.Post(route);
        }
        #endregion

        #region Specific User
        /// <summary>
        /// Lists packages owned by the authenticated user within the user's namespace
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/rest/packages#list-packages-for-the-authenticated-users-namespace">API documentation</a> for more details
        /// </remarks>
        /// <param name="username">Required: Username</param>
        /// <param name="packageType">Required: The type of package</param>
        /// <param name="packageVisibility">Optional: The visibility of the package</param>
        [ManualRoute("GET", "/users/{username}/packages")]
        public Task<IReadOnlyList<Package>> GetAllForUser(string username, PackageType packageType, PackageVisibility? packageVisibility = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(username, nameof(username));

            var route = ApiUrls.PackagesUser(username);
            var parameters = ParameterBuilder.AddParameter("package_type", packageType).AddOptionalParameter("visibility", packageVisibility);

            return ApiConnection.GetAll<Package>(route, parameters);
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
        [ManualRoute("GET", "/users/{username}/packages/{package_type}/{package_name}")]
        public Task<Package> GetForUser(string username, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(username, nameof(username));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            var route = ApiUrls.PackageUser(username, packageType, packageName);

            return ApiConnection.Get<Package>(route);
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
        [ManualRoute("DELETE", "/users/{username}/packages/{package_type}/{package_name}")]
        public Task DeleteForUser(string username, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(username, nameof(username));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            var route = ApiUrls.PackageUser(username, packageType, packageName);

            return ApiConnection.Delete(route);
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
        [ManualRoute("POST", "/users/{username}/packages/{package_type}/{package_name}/restore")]
        public Task RestoreForUser(string username, PackageType packageType, string packageName)
        {
            Ensure.ArgumentNotNullOrEmptyString(username, nameof(username));
            Ensure.ArgumentNotNullOrEmptyString(packageName, nameof(packageName));

            var route = ApiUrls.PackageRestoreUser(username, packageType, packageName);

            return ApiConnection.Post(route);
        }
        #endregion
    }
}
