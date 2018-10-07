using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableRepositoryBranchesClient : IObservableRepositoryBranchesClient
    {
        readonly IRepositoryBranchesClient _client;
        readonly IConnection _connection;

        public ObservableRepositoryBranchesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Repository.Branch;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<Branch> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        public IObservable<Branch> GetAll(long repositoryId)
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
        public IObservable<Branch> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Branch>(ApiUrls.RepoBranches(owner, name), options);
        }

        /// <summary>
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<Branch> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Branch>(ApiUrls.RepoBranches(repositoryId), options);
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
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        public IObservable<Branch> Get(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.Get(owner, name, branch).ToObservable();
        }

        /// <summary>
        /// Gets the specified branch.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="branch">The name of the branch</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        public IObservable<Branch> Get(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.Get(repositoryId, branch).ToObservable();
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
        public IObservable<BranchProtectionSettings> GetBranchProtection(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetBranchProtection(owner, name, branch).ToObservable();
        }

        /// <summary>
        /// Get the branch protection settings for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<BranchProtectionSettings> GetBranchProtection(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetBranchProtection(repositoryId, branch).ToObservable();
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
        public IObservable<BranchProtectionSettings> UpdateBranchProtection(string owner, string name, string branch, BranchProtectionSettingsUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(update, nameof(update));

            return _client.UpdateBranchProtection(owner, name, branch, update).ToObservable();
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
        public IObservable<BranchProtectionSettings> UpdateBranchProtection(long repositoryId, string branch, BranchProtectionSettingsUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(update, nameof(update));

            return _client.UpdateBranchProtection(repositoryId, branch, update).ToObservable();
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
        public IObservable<bool> DeleteBranchProtection(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.DeleteBranchProtection(owner, name, branch).ToObservable();
        }

        /// <summary>
        /// Remove the branch protection settings for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<bool> DeleteBranchProtection(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.DeleteBranchProtection(repositoryId, branch).ToObservable();
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
        public IObservable<BranchProtectionRequiredStatusChecks> GetRequiredStatusChecks(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetRequiredStatusChecks(owner, name, branch).ToObservable();
        }

        /// <summary>
        /// Get the required status checks for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<BranchProtectionRequiredStatusChecks> GetRequiredStatusChecks(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetRequiredStatusChecks(repositoryId, branch).ToObservable();
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
        public IObservable<BranchProtectionRequiredStatusChecks> UpdateRequiredStatusChecks(string owner, string name, string branch, BranchProtectionRequiredStatusChecksUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(update, nameof(update));

            return _client.UpdateRequiredStatusChecks(owner, name, branch, update).ToObservable();
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
        public IObservable<BranchProtectionRequiredStatusChecks> UpdateRequiredStatusChecks(long repositoryId, string branch, BranchProtectionRequiredStatusChecksUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(update, nameof(update));

            return _client.UpdateRequiredStatusChecks(repositoryId, branch, update).ToObservable();
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
        public IObservable<bool> DeleteRequiredStatusChecks(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.DeleteRequiredStatusChecks(owner, name, branch).ToObservable();
        }

        /// <summary>
        /// Remove required status checks for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param> 
        public IObservable<bool> DeleteRequiredStatusChecks(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.DeleteRequiredStatusChecks(repositoryId, branch).ToObservable();
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
        public IObservable<string> GetAllRequiredStatusChecksContexts(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetAllRequiredStatusChecksContexts(owner, name, branch).ToObservable().SelectMany(x => x);
        }

        /// <summary>
        /// Get the required status checks contexts for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<string> GetAllRequiredStatusChecksContexts(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetAllRequiredStatusChecksContexts(repositoryId, branch).ToObservable().SelectMany(x => x);
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
        public IObservable<string> UpdateRequiredStatusChecksContexts(string owner, string name, string branch, IReadOnlyList<string> contexts)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(contexts, nameof(contexts));

            return _client.UpdateRequiredStatusChecksContexts(owner, name, branch, contexts).ToObservable().SelectMany(x => x);
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
        public IObservable<string> UpdateRequiredStatusChecksContexts(long repositoryId, string branch, IReadOnlyList<string> contexts)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(contexts, nameof(contexts));

            return _client.UpdateRequiredStatusChecksContexts(repositoryId, branch, contexts).ToObservable().SelectMany(x => x);
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
        public IObservable<string> AddRequiredStatusChecksContexts(string owner, string name, string branch, IReadOnlyList<string> contexts)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(contexts, nameof(contexts));

            return _client.AddRequiredStatusChecksContexts(owner, name, branch, contexts).ToObservable().SelectMany(x => x);
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
        public IObservable<string> AddRequiredStatusChecksContexts(long repositoryId, string branch, IReadOnlyList<string> contexts)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(contexts, nameof(contexts));

            return _client.AddRequiredStatusChecksContexts(repositoryId, branch, contexts).ToObservable().SelectMany(x => x);
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
        public IObservable<string> DeleteRequiredStatusChecksContexts(string owner, string name, string branch, IReadOnlyList<string> contexts)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(contexts, nameof(contexts));

            return _client.DeleteRequiredStatusChecksContexts(owner, name, branch, contexts).ToObservable().SelectMany(x => x);
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
        public IObservable<string> DeleteRequiredStatusChecksContexts(long repositoryId, string branch, IReadOnlyList<string> contexts)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(contexts, nameof(contexts));

            return _client.DeleteRequiredStatusChecksContexts(repositoryId, branch, contexts).ToObservable().SelectMany(x => x);
        }

        /// <summary>
        /// Get required pull request review enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-pull-request-review-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<BranchProtectionRequiredReviews> GetReviewEnforcement(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetReviewEnforcement(owner, name, branch).ToObservable();
        }

        /// <summary>
        /// Get required pull request review enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<BranchProtectionRequiredReviews> GetReviewEnforcement(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetReviewEnforcement(repositoryId, branch).ToObservable();
        }

        /// <summary>
        /// Update required pull request review enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#update-pull-request-review-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">The required pull request review settings</param>
        public IObservable<BranchProtectionRequiredReviews> UpdateReviewEnforcement(string owner, string name, string branch, BranchProtectionRequiredReviewsUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(update, nameof(update));

            return _client.UpdateReviewEnforcement(owner, name, branch, update).ToObservable();
        }

        /// <summary>
        /// Update required pull request review enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#update-pull-request-review-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">The required pull request review settings</param>
        public IObservable<BranchProtectionRequiredReviews> UpdateReviewEnforcement(long repositoryId, string branch, BranchProtectionRequiredReviewsUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(update, nameof(update));

            return _client.UpdateReviewEnforcement(repositoryId, branch, update).ToObservable();
        }

        /// <summary>
        /// Remove required pull request review enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-pull-request-review-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<bool> RemoveReviewEnforcement(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.RemoveReviewEnforcement(owner, name, branch).ToObservable();
        }

        /// <summary>
        /// Remove required pull request review enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-pull-request-review-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<bool> RemoveReviewEnforcement(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.RemoveReviewEnforcement(repositoryId, branch).ToObservable();
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
        public IObservable<EnforceAdmins> GetAdminEnforcement(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetAdminEnforcement(owner, name, branch).ToObservable();
        }

        /// <summary>
        /// Get admin enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<EnforceAdmins> GetAdminEnforcement(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetAdminEnforcement(repositoryId, branch).ToObservable();
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
        public IObservable<EnforceAdmins> AddAdminEnforcement(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.AddAdminEnforcement(owner, name, branch).ToObservable();
        }

        /// <summary>
        /// Add admin enforcement to protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#add-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<EnforceAdmins> AddAdminEnforcement(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.AddAdminEnforcement(repositoryId, branch).ToObservable();
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
        public IObservable<bool> RemoveAdminEnforcement(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.RemoveAdminEnforcement(owner, name, branch).ToObservable();
        }

        /// <summary>
        /// Remove admin enforcement on protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<bool> RemoveAdminEnforcement(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.RemoveAdminEnforcement(repositoryId, branch).ToObservable();
        }

        /// <summary>
        /// Get the restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<BranchProtectionPushRestrictions> GetProtectedBranchRestrictions(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetProtectedBranchRestrictions(owner, name, branch).ToObservable();
        }

        /// <summary>
        /// Get the restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<BranchProtectionPushRestrictions> GetProtectedBranchRestrictions(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetProtectedBranchRestrictions(repositoryId, branch).ToObservable();
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
        public IObservable<bool> DeleteProtectedBranchRestrictions(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.DeleteProtectedBranchRestrictions(owner, name, branch).ToObservable();
        }

        /// <summary>
        /// Remove restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<bool> DeleteProtectedBranchRestrictions(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.DeleteProtectedBranchRestrictions(repositoryId, branch).ToObservable();
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
        public IObservable<Team> GetAllProtectedBranchTeamRestrictions(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetAllProtectedBranchTeamRestrictions(owner, name, branch).ToObservable().SelectMany(x => x);
        }

        /// <summary>
        /// Get team restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-team-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<Team> GetAllProtectedBranchTeamRestrictions(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetAllProtectedBranchTeamRestrictions(repositoryId, branch).ToObservable().SelectMany(x => x);
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
        public IObservable<Team> UpdateProtectedBranchTeamRestrictions(string owner, string name, string branch, BranchProtectionTeamCollection teams)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(teams, nameof(teams));

            return _client.UpdateProtectedBranchTeamRestrictions(owner, name, branch, teams).ToObservable().SelectMany(x => x);
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
        public IObservable<Team> UpdateProtectedBranchTeamRestrictions(long repositoryId, string branch, BranchProtectionTeamCollection teams)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(teams, nameof(teams));

            return _client.UpdateProtectedBranchTeamRestrictions(repositoryId, branch, teams).ToObservable().SelectMany(x => x);
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
        /// <param name="teams">List of teams with push access</param>
        public IObservable<Team> AddProtectedBranchTeamRestrictions(string owner, string name, string branch, BranchProtectionTeamCollection teams)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(teams, nameof(teams));

            return _client.AddProtectedBranchTeamRestrictions(owner, name, branch, teams).ToObservable().SelectMany(x => x);
        }

        /// <summary>
        /// Add team restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#add-team-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="teams">List of teams with push access</param>
        public IObservable<Team> AddProtectedBranchTeamRestrictions(long repositoryId, string branch, BranchProtectionTeamCollection teams)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(teams, nameof(teams));

            return _client.AddProtectedBranchTeamRestrictions(repositoryId, branch, teams).ToObservable().SelectMany(x => x);
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
        public IObservable<Team> DeleteProtectedBranchTeamRestrictions(string owner, string name, string branch, BranchProtectionTeamCollection teams)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(teams, nameof(teams));

            return _client.DeleteProtectedBranchTeamRestrictions(owner, name, branch, teams).ToObservable().SelectMany(x => x);
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
        public IObservable<Team> DeleteProtectedBranchTeamRestrictions(long repositoryId, string branch, BranchProtectionTeamCollection teams)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(teams, nameof(teams));

            return _client.DeleteProtectedBranchTeamRestrictions(repositoryId, branch, teams).ToObservable().SelectMany(x => x);
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
        public IObservable<User> GetAllProtectedBranchUserRestrictions(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetAllProtectedBranchUserRestrictions(owner, name, branch).ToObservable().SelectMany(x => x);
        }

        /// <summary>
        /// Get user restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-user-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<User> GetAllProtectedBranchUserRestrictions(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetAllProtectedBranchUserRestrictions(repositoryId, branch).ToObservable().SelectMany(x => x);
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
        public IObservable<User> UpdateProtectedBranchUserRestrictions(string owner, string name, string branch, BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(users, nameof(users));

            return _client.UpdateProtectedBranchUserRestrictions(owner, name, branch, users).ToObservable().SelectMany(x => x);
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
        public IObservable<User> UpdateProtectedBranchUserRestrictions(long repositoryId, string branch, BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(users, nameof(users));

            return _client.UpdateProtectedBranchUserRestrictions(repositoryId, branch, users).ToObservable().SelectMany(x => x);
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
        public IObservable<User> AddProtectedBranchUserRestrictions(string owner, string name, string branch, BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(users, nameof(users));

            return _client.AddProtectedBranchUserRestrictions(owner, name, branch, users).ToObservable().SelectMany(x => x);
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
        public IObservable<User> AddProtectedBranchUserRestrictions(long repositoryId, string branch, BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(users, nameof(users));

            return _client.AddProtectedBranchUserRestrictions(repositoryId, branch, users).ToObservable().SelectMany(x => x);
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
        public IObservable<User> DeleteProtectedBranchUserRestrictions(string owner, string name, string branch, BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(users, nameof(users));

            return _client.DeleteProtectedBranchUserRestrictions(owner, name, branch, users).ToObservable().SelectMany(x => x);
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
        public IObservable<User> DeleteProtectedBranchUserRestrictions(long repositoryId, string branch, BranchProtectionUserCollection users)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(users, nameof(users));

            return _client.DeleteProtectedBranchUserRestrictions(repositoryId, branch, users).ToObservable().SelectMany(x => x);
        }
    }
}
