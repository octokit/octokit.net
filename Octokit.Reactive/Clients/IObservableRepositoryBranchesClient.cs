using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Repository Branches API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/branches">Repository Branches API documentation</a> for more details.
    /// </remarks>
    public interface IObservableRepositoryBranchesClient
    {
        /// <summary>
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        IObservable<Branch> GetAll(string owner, string name);

        /// <summary>
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        IObservable<Branch> GetAll(long repositoryId);

        /// <summary>
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<Branch> GetAll(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all the branches for the specified repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-branches">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<Branch> GetAll(long repositoryId, ApiOptions options);

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
        IObservable<Branch> Get(string owner, string name, string branch);

        /// <summary>
        /// Gets the specified branch.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="branch">The name of the branch</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        IObservable<Branch> Get(long repositoryId, string branch);

        /// <summary>
        /// Get the branch protection settings for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<BranchProtectionSettings> GetBranchProtection(string owner, string name, string branch);

        /// <summary>
        /// Get the branch protection settings for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<BranchProtectionSettings> GetBranchProtection(long repositoryId, string branch);

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
        IObservable<BranchProtectionSettings> UpdateBranchProtection(string owner, string name, string branch, BranchProtectionSettingsUpdate update);

        /// <summary>
        /// Update the branch protection settings for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#update-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">Branch protection settings</param>
        IObservable<BranchProtectionSettings> UpdateBranchProtection(long repositoryId, string branch, BranchProtectionSettingsUpdate update);

        /// <summary>
        /// Remove the branch protection settings for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<bool> DeleteBranchProtection(string owner, string name, string branch);

        /// <summary>
        /// Remove the branch protection settings for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<bool> DeleteBranchProtection(long repositoryId, string branch);

        /// <summary>
        /// Get the required status checks for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<BranchProtectionRequiredStatusChecks> GetRequiredStatusChecks(string owner, string name, string branch);

        /// <summary>
        /// Get the required status checks for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<BranchProtectionRequiredStatusChecks> GetRequiredStatusChecks(long repositoryId, string branch);

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
        IObservable<BranchProtectionRequiredStatusChecks> UpdateRequiredStatusChecks(string owner, string name, string branch, BranchProtectionRequiredStatusChecksUpdate update);

        /// <summary>
        /// Replace required status checks for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#update-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">Required status checks</param>
        IObservable<BranchProtectionRequiredStatusChecks> UpdateRequiredStatusChecks(long repositoryId, string branch, BranchProtectionRequiredStatusChecksUpdate update);

        /// <summary>
        /// Remove required status checks for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<bool> DeleteRequiredStatusChecks(string owner, string name, string branch);

        /// <summary>
        /// Remove required status checks for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-required-status-checks-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<bool> DeleteRequiredStatusChecks(long repositoryId, string branch);

        /// <summary>
        /// Get the required status checks contexts for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<string> GetAllRequiredStatusChecksContexts(string owner, string name, string branch);

        /// <summary>
        /// Get the required status checks contexts for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<string> GetAllRequiredStatusChecksContexts(long repositoryId, string branch);

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
        IObservable<string> UpdateRequiredStatusChecksContexts(string owner, string name, string branch, IReadOnlyList<string> contexts);

        /// <summary>
        /// Replace the required status checks contexts for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#replace-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="contexts">The contexts to replace</param>
        IObservable<string> UpdateRequiredStatusChecksContexts(long repositoryId, string branch, IReadOnlyList<string> contexts);

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
        IObservable<string> AddRequiredStatusChecksContexts(string owner, string name, string branch, IReadOnlyList<string> contexts);

        /// <summary>
        /// Add the required status checks contexts for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#add-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="contexts">The contexts to add</param>
        IObservable<string> AddRequiredStatusChecksContexts(long repositoryId, string branch, IReadOnlyList<string> contexts);

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
        IObservable<string> DeleteRequiredStatusChecksContexts(string owner, string name, string branch, IReadOnlyList<string> contexts);

        /// <summary>
        /// Remove the required status checks contexts for the specified branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-required-status-checks-contexts-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="contexts">The contexts to remove</param>
        IObservable<string> DeleteRequiredStatusChecksContexts(long repositoryId, string branch, IReadOnlyList<string> contexts);

        /// <summary>
        /// Get required pull request review enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-pull-request-review-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<BranchProtectionRequiredReviews> GetReviewEnforcement(string owner, string name, string branch);

        /// <summary>
        /// Get required pull request review enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<BranchProtectionRequiredReviews> GetReviewEnforcement(long repositoryId, string branch);

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
        IObservable<BranchProtectionRequiredReviews> UpdateReviewEnforcement(string owner, string name, string branch, BranchProtectionRequiredReviewsUpdate update);

        /// <summary>
        /// Update required pull request review enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#update-pull-request-review-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="update">The required pull request review settings</param>
        IObservable<BranchProtectionRequiredReviews> UpdateReviewEnforcement(long repositoryId, string branch, BranchProtectionRequiredReviewsUpdate update);

        /// <summary>
        /// Remove required pull request review enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-pull-request-review-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<bool> RemoveReviewEnforcement(string owner, string name, string branch);

        /// <summary>
        /// Remove required pull request review enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-pull-request-review-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<bool> RemoveReviewEnforcement(long repositoryId, string branch);

        /// <summary>
        /// Get admin enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<EnforceAdmins> GetAdminEnforcement(string owner, string name, string branch);

        /// <summary>
        /// Get admin enforcement of protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<EnforceAdmins> GetAdminEnforcement(long repositoryId, string branch);

        /// <summary>
        /// Add admin enforcement to protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#add-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<EnforceAdmins> AddAdminEnforcement(string owner, string name, string branch);

        /// <summary>
        /// Add admin enforcement to protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#add-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<EnforceAdmins> AddAdminEnforcement(long repositoryId, string branch);

        /// <summary>
        /// Remove admin enforcement on protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<bool> RemoveAdminEnforcement(string owner, string name, string branch);

        /// <summary>
        /// Remove admin enforcement on protected branch
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-admin-enforcement-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<bool> RemoveAdminEnforcement(long repositoryId, string branch);

        /// <summary>
        /// Get the restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<BranchProtectionPushRestrictions> GetProtectedBranchRestrictions(string owner, string name, string branch);

        /// <summary>
        /// Get the restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<BranchProtectionPushRestrictions> GetProtectedBranchRestrictions(long repositoryId, string branch);

        /// <summary>
        /// Remove restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<bool> DeleteProtectedBranchRestrictions(string owner, string name, string branch);

        /// <summary>
        /// Remove restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<bool> DeleteProtectedBranchRestrictions(long repositoryId, string branch);

        /// <summary>
        /// Get team restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-team-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<Team> GetAllProtectedBranchTeamRestrictions(string owner, string name, string branch);

        /// <summary>
        /// Get team restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-team-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<Team> GetAllProtectedBranchTeamRestrictions(long repositoryId, string branch);

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
        IObservable<Team> UpdateProtectedBranchTeamRestrictions(string owner, string name, string branch, BranchProtectionTeamCollection teams);

        /// <summary>
        /// Replace team restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#replace-team-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="teams">List of teams with push access</param>
        IObservable<Team> UpdateProtectedBranchTeamRestrictions(long repositoryId, string branch, BranchProtectionTeamCollection teams);

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
        IObservable<Team> AddProtectedBranchTeamRestrictions(string owner, string name, string branch, BranchProtectionTeamCollection teams);

        /// <summary>
        /// Add team restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#add-team-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="teams">List of teams with push access</param>
        IObservable<Team> AddProtectedBranchTeamRestrictions(long repositoryId, string branch, BranchProtectionTeamCollection teams);

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
        IObservable<Team> DeleteProtectedBranchTeamRestrictions(string owner, string name, string branch, BranchProtectionTeamCollection teams);

        /// <summary>
        /// Remove team restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-team-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="teams">List of teams to remove</param>
        IObservable<Team> DeleteProtectedBranchTeamRestrictions(long repositoryId, string branch, BranchProtectionTeamCollection teams);

        /// <summary>
        /// Get user restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-user-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<User> GetAllProtectedBranchUserRestrictions(string owner, string name, string branch);

        /// <summary>
        /// Get user restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#list-user-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        IObservable<User> GetAllProtectedBranchUserRestrictions(long repositoryId, string branch);

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
        IObservable<User> UpdateProtectedBranchUserRestrictions(string owner, string name, string branch, BranchProtectionUserCollection users);

        /// <summary>
        /// Replace user restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#replace-user-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="users">List of users with push access</param>
        IObservable<User> UpdateProtectedBranchUserRestrictions(long repositoryId, string branch, BranchProtectionUserCollection users);

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
        IObservable<User> AddProtectedBranchUserRestrictions(string owner, string name, string branch, BranchProtectionUserCollection users);

        /// <summary>
        /// Add user restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#add-user-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="users">List of users with push access to add</param>
        IObservable<User> AddProtectedBranchUserRestrictions(long repositoryId, string branch, BranchProtectionUserCollection users);

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
        IObservable<User> DeleteProtectedBranchUserRestrictions(string owner, string name, string branch, BranchProtectionUserCollection users);

        /// <summary>
        /// Remove user restrictions for the specified branch (applies only to Organization owned repositories)
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-user-restrictions-of-protected-branch">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        /// <param name="users">List of users with push access to remove</param>
        IObservable<User> DeleteProtectedBranchUserRestrictions(long repositoryId, string branch, BranchProtectionUserCollection users);
    }
}
