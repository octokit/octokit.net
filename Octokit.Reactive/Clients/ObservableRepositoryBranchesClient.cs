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
            Ensure.ArgumentNotNull(client, "client");

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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

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
            Ensure.ArgumentNotNull(options, "options");

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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return _client.Get(repositoryId, branch).ToObservable();
        }

        /// <summary>
        /// Edit the specified branch with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">New values to update the branch with</param>
        [Obsolete("This existing implementation will cease to work when the Branch Protection API preview period ends.  Please use other ObservableRepositoryBranchesClient methods instead.")]
        public IObservable<Branch> Edit(string owner, string name, string branch, BranchUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

            return _client.Edit(owner, name, branch, update).ToObservable();
        }

        /// <summary>
        /// Edit the specified branch with the values given in <paramref name="update"/>
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">New values to update the branch with</param>
        [Obsolete("This existing implementation will cease to work when the Branch Protection API preview period ends.  Please use other ObservableRepositoryBranchesClient methods instead.")]
        public IObservable<Branch> Edit(long repositoryId, string branch, BranchUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

            return _client.Edit(repositoryId, branch, update).ToObservable();
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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

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
        public IObservable<string> GetRequiredStatusChecksContexts(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return _client.GetRequiredStatusChecksContexts(owner, name, branch).ToObservable().SelectMany(x => x);
        }

        /// <summary>
        /// Get the required status checks contexts for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<string> GetRequiredStatusChecksContexts(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return _client.GetRequiredStatusChecksContexts(repositoryId, branch).ToObservable().SelectMany(x => x);
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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(contexts, "contexts");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(contexts, "contexts");

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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(contexts, "contexts");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(contexts, "contexts");

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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(contexts, "contexts");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(contexts, "contexts");

            return _client.DeleteRequiredStatusChecksContexts(repositoryId, branch, contexts).ToObservable().SelectMany(x => x);
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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

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
        public IObservable<Team> GetProtectedBranchTeamRestrictions(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return _client.GetProtectedBranchTeamRestrictions(owner, name, branch).ToObservable().SelectMany(x => x);
        }

        /// <summary>
        /// Get team restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-team-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<Team> GetProtectedBranchTeamRestrictions(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return _client.GetProtectedBranchTeamRestrictions(repositoryId, branch).ToObservable().SelectMany(x => x);
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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(teams, "teams");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(teams, "teams");

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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(teams, "teams");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(teams, "teams");

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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(teams, "teams");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(teams, "teams");

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
        public IObservable<User> GetProtectedBranchUserRestrictions(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return _client.GetProtectedBranchUserRestrictions(owner, name, branch).ToObservable().SelectMany(x => x);
        }

        /// <summary>
        /// Get user restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-user-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<User> GetProtectedBranchUserRestrictions(long repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return _client.GetProtectedBranchUserRestrictions(repositoryId, branch).ToObservable().SelectMany(x => x);
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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(users, "users");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(users, "users");

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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(users, "users");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(users, "users");

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
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(users, "users");

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
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(users, "users");

            return _client.DeleteProtectedBranchUserRestrictions(repositoryId, branch, users).ToObservable().SelectMany(x => x);
        }
    }
}
