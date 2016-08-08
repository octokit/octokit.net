using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Branches API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/branches">Repository Branches API documentation</a> for more details.
    /// </remarks>
    public class RepositoryBranchesClient : ApiClient, IRepositoryBranchesClient
    {
        /// <summary>
        /// Initializes a new GitHub Repository Branches API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public RepositoryBranchesClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public Task<IReadOnlyList<Branch>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAll(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public Task<IReadOnlyList<Branch>> GetAll(int repositoryId)
        {
            return GetAll(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public Task<IReadOnlyList<Branch>> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Branch>(ApiUrls.RepoBranches(owner, name), null, AcceptHeaders.ProtectedBranchesApiPreview, options);
        }

        /// <summary>
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public Task<IReadOnlyList<Branch>> GetAll(int repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Branch>(ApiUrls.RepoBranches(repositoryId), null, AcceptHeaders.ProtectedBranchesApiPreview, options);
        }

        /// <summary>
        /// Gets the specified branch.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<Branch> Get(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<Branch>(ApiUrls.RepoBranch(owner, name, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Gets the specified branch.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<Branch> Get(int repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branchName");

            return ApiConnection.Get<Branch>(ApiUrls.RepoBranch(repositoryId, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Edit the specified branch with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">New values to update the branch with</param>
        [Obsolete("BranchProtection preview functionality in the GitHub API has had breaking changes.  This existing implementation will cease to work when the preview period ends.")]
        public Task<Branch> Edit(string owner, string name, string branch, BranchUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

            return ApiConnection.Patch<Branch>(ApiUrls.RepoBranch(owner, name, branch), update, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Edit the specified branch with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">New values to update the branch with</param>
        [Obsolete("BranchProtection preview functionality in the GitHub API has had breaking changes.  This existing implementation will cease to work when the preview period ends.")]
        public Task<Branch> Edit(int repositoryId, string branch, BranchUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

            return ApiConnection.Patch<Branch>(ApiUrls.RepoBranch(repositoryId, branch), update, AcceptHeaders.ProtectedBranchesApiPreview);
        }
    }
}
