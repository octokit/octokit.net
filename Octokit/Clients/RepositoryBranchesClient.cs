using System;
using System.Collections.Generic;
using System.Net;
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

        /// <summary>
        /// Get the branch protection settings for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<BranchProtectionSettings> GetBranchProtection(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<BranchProtectionSettings>(ApiUrls.RepoBranchProtection(owner, name, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Get the branch protection settings for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<BranchProtectionSettings> GetBranchProtection(int repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<BranchProtectionSettings>(ApiUrls.RepoBranchProtection(repositoryId, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Update the branch protection settings for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#update-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">Branch protection settings</param>
        public Task<BranchProtectionSettings> UpdateBranchProtection(string owner, string name, string branch, BranchProtectionSettingsUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

            return ApiConnection.Put<BranchProtectionSettings>(ApiUrls.RepoBranchProtection(owner, name, branch), update, null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Update the branch protection settings for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#update-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">Branch protection settings</param>
        public Task<BranchProtectionSettings> UpdateBranchProtection(int repositoryId, string branch, BranchProtectionSettingsUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

            return ApiConnection.Put<BranchProtectionSettings>(ApiUrls.RepoBranchProtection(repositoryId, branch), update, null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Remove the branch protection settings for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public async Task<bool> DeleteBranchProtection(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            var endpoint = ApiUrls.RepoBranchProtection(owner, name, branch);
            try
            {
                var httpStatusCode = await Connection.Delete(endpoint, null, AcceptHeaders.ProtectedBranchesApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Remove the branch protection settings for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public async Task<bool> DeleteBranchProtection(int repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            var endpoint = ApiUrls.RepoBranchProtection(repositoryId, branch);
            try
            {
                var httpStatusCode = await Connection.Delete(endpoint, null, AcceptHeaders.ProtectedBranchesApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Get the required status checks for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<BranchProtectionRequiredStatusChecks> GetRequiredStatusChecks(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<BranchProtectionRequiredStatusChecks>(ApiUrls.RepoRequiredStatusChecks(owner, name, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Get the required status checks for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<BranchProtectionRequiredStatusChecks> GetRequiredStatusChecks(int repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<BranchProtectionRequiredStatusChecks>(ApiUrls.RepoRequiredStatusChecks(repositoryId, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Edit required status checks for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#update-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">Required status checks</param>
        public Task<BranchProtectionRequiredStatusChecks> UpdateRequiredStatusChecks(string owner, string name, string branch, BranchProtectionRequiredStatusChecksUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

            return ApiConnection.Put<BranchProtectionRequiredStatusChecks>(ApiUrls.RepoRequiredStatusChecks(owner, name, branch), update, null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Edit required status checks for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#update-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">Required status checks</param>
        public Task<BranchProtectionRequiredStatusChecks> UpdateRequiredStatusChecks(int repositoryId, string branch, BranchProtectionRequiredStatusChecksUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

            return ApiConnection.Put<BranchProtectionRequiredStatusChecks>(ApiUrls.RepoRequiredStatusChecks(repositoryId, branch), update, null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Remove required status checks for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public async Task<bool> DeleteRequiredStatusChecks(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            var endpoint = ApiUrls.RepoRequiredStatusChecks(owner, name, branch);

            try
            {
                var httpStatusCode = await Connection.Delete(endpoint, null, AcceptHeaders.ProtectedBranchesApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Remove required status checks for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public async Task<bool> DeleteRequiredStatusChecks(int repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            var endpoint = ApiUrls.RepoRequiredStatusChecks(repositoryId, branch);

            try
            {
                var httpStatusCode = await Connection.Delete(endpoint, null, AcceptHeaders.ProtectedBranchesApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Get the required status checks contexts for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<IReadOnlyList<string>> GetRequiredStatusChecksContexts(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<IReadOnlyList<string>>(ApiUrls.RepoRequiredStatusChecksContexts(owner, name, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Get the required status checks contexts for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<IReadOnlyList<string>> GetRequiredStatusChecksContexts(int repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<IReadOnlyList<string>>(ApiUrls.RepoRequiredStatusChecksContexts(repositoryId, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Replace the required status checks contexts for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#replace-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="contexts">The contexts to replace</param>
        public Task<IReadOnlyList<string>> UpdateRequiredStatusChecksContexts(string owner, string name, string branch, IReadOnlyList<string> contexts)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(contexts, "contexts");

            return ApiConnection.Put<IReadOnlyList<string>>(ApiUrls.RepoRequiredStatusChecksContexts(owner, name, branch), contexts, null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Replace the required status checks contexts for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#replace-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="contexts">The contexts to replace</param>
        public Task<IReadOnlyList<string>> UpdateRequiredStatusChecksContexts(int repositoryId, string branch, IReadOnlyList<string> contexts)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(contexts, "contexts");

            return ApiConnection.Put<IReadOnlyList<string>>(ApiUrls.RepoRequiredStatusChecksContexts(repositoryId, branch), contexts, null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Add the required status checks context for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#add-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="contexts">The contexts to add</param>
        public Task<IReadOnlyList<string>> AddRequiredStatusChecksContexts(string owner, string name, string branch, IReadOnlyList<string> contexts)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(contexts, "contexts");

            return ApiConnection.Post<IReadOnlyList<string>>(ApiUrls.RepoRequiredStatusChecksContexts(owner, name, branch), contexts, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Add the required status checks contexts for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#add-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="contexts">The contexts to add</param>
        public Task<IReadOnlyList<string>> AddRequiredStatusChecksContexts(int repositoryId, string branch, IReadOnlyList<string> contexts)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(contexts, "contexts");

            return ApiConnection.Post<IReadOnlyList<string>>(ApiUrls.RepoRequiredStatusChecksContexts(repositoryId, branch), contexts, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Remove the required status checks context for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="contexts">The contexts to remove</param>
        public Task<IReadOnlyList<string>> DeleteRequiredStatusChecksContexts(string owner, string name, string branch, IReadOnlyList<string> contexts)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(contexts, "contexts");

            return ApiConnection.Delete<IReadOnlyList<string>>(ApiUrls.RepoRequiredStatusChecksContexts(owner, name, branch), contexts, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Remove the required status checks contexts for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="contexts">The contexts to remove</param>
        public Task<IReadOnlyList<string>> DeleteRequiredStatusChecksContexts(int repositoryId, string branch, IReadOnlyList<string> contexts)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(contexts, "contexts");

            return ApiConnection.Delete<IReadOnlyList<string>>(ApiUrls.RepoRequiredStatusChecksContexts(repositoryId, branch), contexts, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Get the restrictions for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<ProtectedBranchRestrictions> GetProtectedBranchRestrictions(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<ProtectedBranchRestrictions>(ApiUrls.RepoRestrictions(owner, name, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Get the restrictions for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<ProtectedBranchRestrictions> GetProtectedBranchRestrictions(int repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<ProtectedBranchRestrictions>(ApiUrls.RepoRestrictions(repositoryId, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }
    }
}
