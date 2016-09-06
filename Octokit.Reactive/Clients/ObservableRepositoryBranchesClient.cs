using System;
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
        public IObservable<Branch> GetAll(int repositoryId)
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
        public IObservable<Branch> GetAll(int repositoryId, ApiOptions options)
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
        public IObservable<Branch> Get(int repositoryId, string branch)
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
        public IObservable<Branch> Edit(int repositoryId, string branch, BranchUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

            return _client.Edit(repositoryId, branch, update).ToObservable();
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
        public IObservable<BranchProtectionSettings> GetBranchProtection(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return _client.GetBranchProtection(owner, name, branch).ToObservable();
        }

        /// <summary>
        /// Get the branch protection settings for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#get-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<BranchProtectionSettings> GetBranchProtection(int repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return _client.GetBranchProtection(repositoryId, branch).ToObservable();
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
        public IObservable<BranchProtectionSettings> UpdateBranchProtection(string owner, string name, string branch, BranchProtectionSettingsUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

            return _client.UpdateBranchProtection(owner, name, branch, update).ToObservable();
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
        public IObservable<BranchProtectionSettings> UpdateBranchProtection(int repositoryId, string branch, BranchProtectionSettingsUpdate update)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");
            Ensure.ArgumentNotNull(update, "update");

            return _client.UpdateBranchProtection(repositoryId, branch, update).ToObservable();
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
        public IObservable<bool> DeleteBranchProtection(string owner, string name, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return _client.DeleteBranchProtection(owner, name, branch).ToObservable();
        }

        /// <summary>
        /// Remove the branch protection settings for the specified branch />
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/branches/#remove-branch-protection">API documentation</a> for more details
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="branch">The name of the branch</param>
        public IObservable<bool> DeleteBranchProtection(int repositoryId, string branch)
        {
            Ensure.ArgumentNotNullOrEmptyString(branch, "branch");

            return _client.DeleteBranchProtection(repositoryId, branch).ToObservable();
        }
    }
}
