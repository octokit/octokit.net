using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;

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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<Branch> GetAll(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<Branch> GetAll(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAll(repositoryId, ApiOptions.None, cancellationToken);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<Branch> GetAll(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Branch>(ApiUrls.RepoBranches(owner, name), options, cancellationToken);
        }

        /// <summary>
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<Branch> GetAll(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Branch>(ApiUrls.RepoBranches(repositoryId), options, cancellationToken);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        public IObservable<Branch> Get(string owner, string name, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.Get(owner, name, branch, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets the specified branch.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        public IObservable<Branch> Get(long repositoryId, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.Get(repositoryId, branch, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<BranchProtectionSettings> GetBranchProtection(string owner, string name, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetBranchProtection(owner, name, branch, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Get the branch protection settings for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<BranchProtectionSettings> GetBranchProtection(long repositoryId, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetBranchProtection(repositoryId, branch, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<BranchProtectionSettings> UpdateBranchProtection(string owner, string name, string branch, BranchProtectionSettingsUpdate update, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(update, nameof(update));

            return _client.UpdateBranchProtection(owner, name, branch, update, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<BranchProtectionSettings> UpdateBranchProtection(long repositoryId, string branch, BranchProtectionSettingsUpdate update, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(update, nameof(update));

            return _client.UpdateBranchProtection(repositoryId, branch, update, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<bool> DeleteBranchProtection(string owner, string name, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.DeleteBranchProtection(owner, name, branch, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Remove the branch protection settings for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<bool> DeleteBranchProtection(long repositoryId, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.DeleteBranchProtection(repositoryId, branch, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<BranchProtectionRequiredStatusChecks> GetRequiredStatusChecks(string owner, string name, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetRequiredStatusChecks(owner, name, branch, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Get the required status checks for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<BranchProtectionRequiredStatusChecks> GetRequiredStatusChecks(long repositoryId, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetRequiredStatusChecks(repositoryId, branch, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<BranchProtectionRequiredStatusChecks> UpdateRequiredStatusChecks(string owner, string name, string branch, BranchProtectionRequiredStatusChecksUpdate update, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(update, nameof(update));

            return _client.UpdateRequiredStatusChecks(owner, name, branch, update, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<BranchProtectionRequiredStatusChecks> UpdateRequiredStatusChecks(long repositoryId, string branch, BranchProtectionRequiredStatusChecksUpdate update, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(update, nameof(update));

            return _client.UpdateRequiredStatusChecks(repositoryId, branch, update, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<bool> DeleteRequiredStatusChecks(string owner, string name, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.DeleteRequiredStatusChecks(owner, name, branch, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Remove required status checks for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<bool> DeleteRequiredStatusChecks(long repositoryId, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.DeleteRequiredStatusChecks(repositoryId, branch, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<string> GetAllRequiredStatusChecksContexts(string owner, string name, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetAllRequiredStatusChecksContexts(owner, name, branch, cancellationToken).ToObservable().SelectMany(x => x);
        }

        /// <summary>
        /// Get the required status checks contexts for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<string> GetAllRequiredStatusChecksContexts(long repositoryId, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetAllRequiredStatusChecksContexts(repositoryId, branch, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<string> UpdateRequiredStatusChecksContexts(string owner, string name, string branch, IReadOnlyList<string> contexts, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(contexts, nameof(contexts));

            return _client.UpdateRequiredStatusChecksContexts(owner, name, branch, contexts, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<string> UpdateRequiredStatusChecksContexts(long repositoryId, string branch, IReadOnlyList<string> contexts, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(contexts, nameof(contexts));

            return _client.UpdateRequiredStatusChecksContexts(repositoryId, branch, contexts, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<string> AddRequiredStatusChecksContexts(string owner, string name, string branch, IReadOnlyList<string> contexts, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(contexts, nameof(contexts));

            return _client.AddRequiredStatusChecksContexts(owner, name, branch, contexts, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<string> AddRequiredStatusChecksContexts(long repositoryId, string branch, IReadOnlyList<string> contexts, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(contexts, nameof(contexts));

            return _client.AddRequiredStatusChecksContexts(repositoryId, branch, contexts, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<string> DeleteRequiredStatusChecksContexts(string owner, string name, string branch, IReadOnlyList<string> contexts, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(contexts, nameof(contexts));

            return _client.DeleteRequiredStatusChecksContexts(owner, name, branch, contexts, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<string> DeleteRequiredStatusChecksContexts(long repositoryId, string branch, IReadOnlyList<string> contexts, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(contexts, nameof(contexts));

            return _client.DeleteRequiredStatusChecksContexts(repositoryId, branch, contexts, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<BranchProtectionRequiredReviews> GetReviewEnforcement(string owner, string name, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetReviewEnforcement(owner, name, branch, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Get required pull request review enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<BranchProtectionRequiredReviews> GetReviewEnforcement(long repositoryId, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetReviewEnforcement(repositoryId, branch, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<BranchProtectionRequiredReviews> UpdateReviewEnforcement(string owner, string name, string branch, BranchProtectionRequiredReviewsUpdate update, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(update, nameof(update));

            return _client.UpdateReviewEnforcement(owner, name, branch, update, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<BranchProtectionRequiredReviews> UpdateReviewEnforcement(long repositoryId, string branch, BranchProtectionRequiredReviewsUpdate update, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(update, nameof(update));

            return _client.UpdateReviewEnforcement(repositoryId, branch, update, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<bool> RemoveReviewEnforcement(string owner, string name, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.RemoveReviewEnforcement(owner, name, branch, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Remove required pull request review enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-pull-request-review-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<bool> RemoveReviewEnforcement(long repositoryId, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.RemoveReviewEnforcement(repositoryId, branch, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<EnforceAdmins> GetAdminEnforcement(string owner, string name, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetAdminEnforcement(owner, name, branch, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Get admin enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<EnforceAdmins> GetAdminEnforcement(long repositoryId, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetAdminEnforcement(repositoryId, branch, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<EnforceAdmins> AddAdminEnforcement(string owner, string name, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.AddAdminEnforcement(owner, name, branch, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Add admin enforcement to protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#add-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<EnforceAdmins> AddAdminEnforcement(long repositoryId, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.AddAdminEnforcement(repositoryId, branch, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<bool> RemoveAdminEnforcement(string owner, string name, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.RemoveAdminEnforcement(owner, name, branch, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Remove admin enforcement on protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<bool> RemoveAdminEnforcement(long repositoryId, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.RemoveAdminEnforcement(repositoryId, branch, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<BranchProtectionPushRestrictions> GetProtectedBranchRestrictions(string owner, string name, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetProtectedBranchRestrictions(owner, name, branch, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Get the restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<BranchProtectionPushRestrictions> GetProtectedBranchRestrictions(long repositoryId, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetProtectedBranchRestrictions(repositoryId, branch, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<bool> DeleteProtectedBranchRestrictions(string owner, string name, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.DeleteProtectedBranchRestrictions(owner, name, branch, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Remove restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<bool> DeleteProtectedBranchRestrictions(long repositoryId, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.DeleteProtectedBranchRestrictions(repositoryId, branch, cancellationToken).ToObservable();
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<Team> GetAllProtectedBranchTeamRestrictions(string owner, string name, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetAllProtectedBranchTeamRestrictions(owner, name, branch, cancellationToken).ToObservable().SelectMany(x => x);
        }

        /// <summary>
        /// Get team restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-team-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<Team> GetAllProtectedBranchTeamRestrictions(long repositoryId, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetAllProtectedBranchTeamRestrictions(repositoryId, branch, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<Team> UpdateProtectedBranchTeamRestrictions(string owner, string name, string branch, BranchProtectionTeamCollection teams, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(teams, nameof(teams));

            return _client.UpdateProtectedBranchTeamRestrictions(owner, name, branch, teams, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<Team> UpdateProtectedBranchTeamRestrictions(long repositoryId, string branch, BranchProtectionTeamCollection teams, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(teams, nameof(teams));

            return _client.UpdateProtectedBranchTeamRestrictions(repositoryId, branch, teams, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<Team> AddProtectedBranchTeamRestrictions(string owner, string name, string branch, BranchProtectionTeamCollection teams, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(teams, nameof(teams));

            return _client.AddProtectedBranchTeamRestrictions(owner, name, branch, teams, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<Team> AddProtectedBranchTeamRestrictions(long repositoryId, string branch, BranchProtectionTeamCollection teams, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(teams, nameof(teams));

            return _client.AddProtectedBranchTeamRestrictions(repositoryId, branch, teams, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<Team> DeleteProtectedBranchTeamRestrictions(string owner, string name, string branch, BranchProtectionTeamCollection teams, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(teams, nameof(teams));

            return _client.DeleteProtectedBranchTeamRestrictions(owner, name, branch, teams, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<Team> DeleteProtectedBranchTeamRestrictions(long repositoryId, string branch, BranchProtectionTeamCollection teams, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(teams, nameof(teams));

            return _client.DeleteProtectedBranchTeamRestrictions(repositoryId, branch, teams, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<User> GetAllProtectedBranchUserRestrictions(string owner, string name, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetAllProtectedBranchUserRestrictions(owner, name, branch, cancellationToken).ToObservable().SelectMany(x => x);
        }

        /// <summary>
        /// Get user restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-user-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<User> GetAllProtectedBranchUserRestrictions(long repositoryId, string branch, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));

            return _client.GetAllProtectedBranchUserRestrictions(repositoryId, branch, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<User> UpdateProtectedBranchUserRestrictions(string owner, string name, string branch, BranchProtectionUserCollection users, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(users, nameof(users));

            return _client.UpdateProtectedBranchUserRestrictions(owner, name, branch, users, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<User> UpdateProtectedBranchUserRestrictions(long repositoryId, string branch, BranchProtectionUserCollection users, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(users, nameof(users));

            return _client.UpdateProtectedBranchUserRestrictions(repositoryId, branch, users, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<User> AddProtectedBranchUserRestrictions(string owner, string name, string branch, BranchProtectionUserCollection users, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(users, nameof(users));

            return _client.AddProtectedBranchUserRestrictions(owner, name, branch, users, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<User> AddProtectedBranchUserRestrictions(long repositoryId, string branch, BranchProtectionUserCollection users, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(users, nameof(users));

            return _client.AddProtectedBranchUserRestrictions(repositoryId, branch, users, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<User> DeleteProtectedBranchUserRestrictions(string owner, string name, string branch, BranchProtectionUserCollection users, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(users, nameof(users));

            return _client.DeleteProtectedBranchUserRestrictions(owner, name, branch, users, cancellationToken).ToObservable().SelectMany(x => x);
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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<User> DeleteProtectedBranchUserRestrictions(long repositoryId, string branch, BranchProtectionUserCollection users, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNull(users, nameof(users));

            return _client.DeleteProtectedBranchUserRestrictions(repositoryId, branch, users, cancellationToken).ToObservable().SelectMany(x => x);
        }

        /// <summary>
        /// Renames a branch in a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/branches/branches?apiVersion=2022-11-28#rename-a-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repository">The name of the repository</param>
        /// <param name="branch">The name of the branch to rename</param>
        /// <param name="newName">The new name of the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        public IObservable<Branch> RenameBranch(string owner, string repository, string branch, string newName, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(repository, nameof(repository));
            Ensure.ArgumentNotNullOrEmptyString(branch, nameof(branch));
            Ensure.ArgumentNotNullOrEmptyString(newName, nameof(newName));

            return _client.RenameBranch(owner, repository, branch, newName, cancellationToken).ToObservable();
        }
    }
}
