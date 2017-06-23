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
        public Task<IReadOnlyList<Branch>> GetAll(long repositoryId)
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
        public Task<IReadOnlyList<Branch>> GetAll(long repositoryId, ApiOptions options)
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
        public Task<Branch> Get(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branchName");

            return ApiConnection.Get<Branch>(ApiUrls.RepoBranch(repositoryId, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Get the branch protection settings for the specified branch
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
        /// Get the branch protection settings for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<BranchProtectionSettings> GetBranchProtection(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<BranchProtectionSettings>(ApiUrls.RepoBranchProtection(repositoryId, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Update the branch protection settings for the specified branch
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
        /// Update the branch protection settings for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#update-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">Branch protection settings</param>
        public Task<BranchProtectionSettings> UpdateBranchProtection(long repositoryId, string branch, BranchProtectionSettingsUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

            return ApiConnection.Put<BranchProtectionSettings>(ApiUrls.RepoBranchProtection(repositoryId, branch), update, null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Remove the branch protection settings for the specified branch
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
        /// Remove the branch protection settings for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public async Task<bool> DeleteBranchProtection(long repositoryId, string branch)
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
        /// Get the required status checks for the specified branch
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
        /// Get the required status checks for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<BranchProtectionRequiredStatusChecks> GetRequiredStatusChecks(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<BranchProtectionRequiredStatusChecks>(ApiUrls.RepoRequiredStatusChecks(repositoryId, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Replace required status checks for the specified branch
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

            return ApiConnection.Patch<BranchProtectionRequiredStatusChecks>(ApiUrls.RepoRequiredStatusChecks(owner, name, branch), update, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Replace required status checks for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#update-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">Required status checks</param>
        public Task<BranchProtectionRequiredStatusChecks> UpdateRequiredStatusChecks(long repositoryId, string branch, BranchProtectionRequiredStatusChecksUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

            return ApiConnection.Patch<BranchProtectionRequiredStatusChecks>(ApiUrls.RepoRequiredStatusChecks(repositoryId, branch), update, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Remove required status checks for the specified branch
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
        /// Remove required status checks for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public async Task<bool> DeleteRequiredStatusChecks(long repositoryId, string branch)
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
        /// Get the required status checks contexts for the specified branch
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
        /// Get the required status checks contexts for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<IReadOnlyList<string>> GetRequiredStatusChecksContexts(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<IReadOnlyList<string>>(ApiUrls.RepoRequiredStatusChecksContexts(repositoryId, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Replace the required status checks contexts for the specified branch
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
        /// Replace the required status checks contexts for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#replace-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="contexts">The contexts to replace</param>
        public Task<IReadOnlyList<string>> UpdateRequiredStatusChecksContexts(long repositoryId, string branch, IReadOnlyList<string> contexts)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(contexts, "contexts");

            return ApiConnection.Put<IReadOnlyList<string>>(ApiUrls.RepoRequiredStatusChecksContexts(repositoryId, branch), contexts, null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Add the required status checks context for the specified branch
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
        /// Add the required status checks contexts for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#add-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="contexts">The contexts to add</param>
        public Task<IReadOnlyList<string>> AddRequiredStatusChecksContexts(long repositoryId, string branch, IReadOnlyList<string> contexts)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(contexts, "contexts");

            return ApiConnection.Post<IReadOnlyList<string>>(ApiUrls.RepoRequiredStatusChecksContexts(repositoryId, branch), contexts, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Remove the required status checks context for the specified branch
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
        /// Remove the required status checks contexts for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="contexts">The contexts to remove</param>
        public Task<IReadOnlyList<string>> DeleteRequiredStatusChecksContexts(long repositoryId, string branch, IReadOnlyList<string> contexts)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(contexts, "contexts");

            return ApiConnection.Delete<IReadOnlyList<string>>(ApiUrls.RepoRequiredStatusChecksContexts(repositoryId, branch), contexts, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Get admin enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<EnforceAdmins> GetAdminEnforcement(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<EnforceAdmins>(ApiUrls.RepoProtectedBranchAdminEnforcement(owner, name, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Get admin enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<EnforceAdmins> GetAdminEnforcement(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<EnforceAdmins>(ApiUrls.RepoProtectedBranchAdminEnforcement(repositoryId, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);

        }

        /// <summary>
        /// Add admin enforcement to protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#add-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<EnforceAdmins> AddAdminEnforcement(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Post<EnforceAdmins>(ApiUrls.RepoProtectedBranchAdminEnforcement(owner, name, branch), new object(), AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Add admin enforcement to protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#add-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<EnforceAdmins> AddAdminEnforcement(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Post<EnforceAdmins>(ApiUrls.RepoProtectedBranchAdminEnforcement(repositoryId, branch), new object(), AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Remove admin enforcement on protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public async Task<bool> RemoveAdminEnforcement(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            var endpoint = ApiUrls.RepoProtectedBranchAdminEnforcement(owner, name, branch);

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
        /// Remove admin enforcement on protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public async Task<bool> RemoveAdminEnforcement(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            var endpoint = ApiUrls.RepoProtectedBranchAdminEnforcement(repositoryId, branch);

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
        /// Get restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<BranchProtectionPushRestrictions> GetProtectedBranchRestrictions(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<BranchProtectionPushRestrictions>(ApiUrls.RepoRestrictions(owner, name, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Get restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<BranchProtectionPushRestrictions> GetProtectedBranchRestrictions(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<BranchProtectionPushRestrictions>(ApiUrls.RepoRestrictions(repositoryId, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Remove restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public async Task<bool> DeleteProtectedBranchRestrictions(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            var endpoint = ApiUrls.RepoRestrictions(owner, name, branch);

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
        /// Remove restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public async Task<bool> DeleteProtectedBranchRestrictions(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            var endpoint = ApiUrls.RepoRestrictions(repositoryId, branch);

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
        /// Get team restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-team-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<IReadOnlyList<Team>> GetProtectedBranchTeamRestrictions(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<IReadOnlyList<Team>>(ApiUrls.RepoRestrictionsTeams(owner, name, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Get team restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-team-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<IReadOnlyList<Team>> GetProtectedBranchTeamRestrictions(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<IReadOnlyList<Team>>(ApiUrls.RepoRestrictionsTeams(repositoryId, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Replace team restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#replace-team-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="teams">List of teams with push access</param>
        public Task<IReadOnlyList<Team>> UpdateProtectedBranchTeamRestrictions(string owner, string name, string branch, BranchProtectionTeamCollection teams)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(teams, "teams");

            return ApiConnection.Put<IReadOnlyList<Team>>(ApiUrls.RepoRestrictionsTeams(owner, name, branch), teams, null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Replace team restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#replace-team-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="teams">List of teams with push access</param>
        public Task<IReadOnlyList<Team>> UpdateProtectedBranchTeamRestrictions(long repositoryId, string branch, BranchProtectionTeamCollection teams)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(teams, "teams");

            return ApiConnection.Put<IReadOnlyList<Team>>(ApiUrls.RepoRestrictionsTeams(repositoryId, branch), teams, null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Add team restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#add-team-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="teams">List of teams with push access to add</param>
        public Task<IReadOnlyList<Team>> AddProtectedBranchTeamRestrictions(string owner, string name, string branch, BranchProtectionTeamCollection teams)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(teams, "teams");

            return ApiConnection.Post<IReadOnlyList<Team>>(ApiUrls.RepoRestrictionsTeams(owner, name, branch), teams, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Add team restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#add-team-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="teams">List of teams with push access to add</param>
        public Task<IReadOnlyList<Team>> AddProtectedBranchTeamRestrictions(long repositoryId, string branch, BranchProtectionTeamCollection teams)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(teams, "teams");

            return ApiConnection.Post<IReadOnlyList<Team>>(ApiUrls.RepoRestrictionsTeams(repositoryId, branch), teams, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Remove team restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-team-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="teams">List of teams to remove</param>
        public Task<IReadOnlyList<Team>> DeleteProtectedBranchTeamRestrictions(string owner, string name, string branch, BranchProtectionTeamCollection teams)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(teams, "teams");

            return ApiConnection.Delete<IReadOnlyList<Team>>(ApiUrls.RepoRestrictionsTeams(owner, name, branch), teams, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Remove team restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-team-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="teams">List of teams to remove</param>
        public Task<IReadOnlyList<Team>> DeleteProtectedBranchTeamRestrictions(long repositoryId, string branch, BranchProtectionTeamCollection teams)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(teams, "teams");

            return ApiConnection.Delete<IReadOnlyList<Team>>(ApiUrls.RepoRestrictionsTeams(repositoryId, branch), teams, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Get user restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-user-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<IReadOnlyList<User>> GetProtectedBranchUserRestrictions(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<IReadOnlyList<User>>(ApiUrls.RepoRestrictionsUsers(owner, name, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Get user restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-user-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public Task<IReadOnlyList<User>> GetProtectedBranchUserRestrictions(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return ApiConnection.Get<IReadOnlyList<User>>(ApiUrls.RepoRestrictionsUsers(repositoryId, branch), null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Replace user restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#replace-user-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="users">List of users with push access</param>
        public Task<IReadOnlyList<User>> UpdateProtectedBranchUserRestrictions(string owner, string name, string branch, BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(users, "users");

            return ApiConnection.Put<IReadOnlyList<User>>(ApiUrls.RepoRestrictionsUsers(owner, name, branch), users, null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Replace user restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#replace-user-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="users">List of users with push access</param>
        public Task<IReadOnlyList<User>> UpdateProtectedBranchUserRestrictions(long repositoryId, string branch, BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(users, "users");

            return ApiConnection.Put<IReadOnlyList<User>>(ApiUrls.RepoRestrictionsUsers(repositoryId, branch), users, null, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Add user restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#add-user-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="users">List of users with push access to add</param>
        public Task<IReadOnlyList<User>> AddProtectedBranchUserRestrictions(string owner, string name, string branch, BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(users, "users");

            return ApiConnection.Post<IReadOnlyList<User>>(ApiUrls.RepoRestrictionsUsers(owner, name, branch), users, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Add user restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#add-user-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="users">List of users with push access to add</param>
        public Task<IReadOnlyList<User>> AddProtectedBranchUserRestrictions(long repositoryId, string branch, BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(users, "users");

            return ApiConnection.Post<IReadOnlyList<User>>(ApiUrls.RepoRestrictionsUsers(repositoryId, branch), users, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Remove user restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-user-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="users">List of users with push access to remove</param>
        public Task<IReadOnlyList<User>> DeleteProtectedBranchUserRestrictions(string owner, string name, string branch, BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(users, "users");

            return ApiConnection.Delete<IReadOnlyList<User>>(ApiUrls.RepoRestrictionsUsers(owner, name, branch), users, AcceptHeaders.ProtectedBranchesApiPreview);
        }

        /// <summary>
        /// Remove user restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-user-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="users">List of users with push access to remove</param>
        public Task<IReadOnlyList<User>> DeleteProtectedBranchUserRestrictions(long repositoryId, string branch, BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(users, "users");

            return ApiConnection.Delete<IReadOnlyList<User>>(ApiUrls.RepoRestrictionsUsers(repositoryId, branch), users, AcceptHeaders.ProtectedBranchesApiPreview);
        }
    }
}
