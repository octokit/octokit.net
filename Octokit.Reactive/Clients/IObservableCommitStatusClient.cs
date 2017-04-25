using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Git Repository Status API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/statuses/">Repository Statuses API documentation</a> for more information.
    /// </remarks>
    public interface IObservableCommitStatusClient
    {
        /// <summary>
        /// Retrieves commit statuses for the specified reference. A reference can be a commit SHA, a branch name, or
        /// a tag name.
        /// </summary>
        /// <remarks>Only users with pull access can see this.</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        IObservable<CommitStatus> GetAll(string owner, string name, string reference);

        /// <summary>
        /// Retrieves commit statuses for the specified reference. A reference can be a commit SHA, a branch name, or
        /// a tag name.
        /// </summary>
        /// <remarks>Only users with pull access can see this.</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        IObservable<CommitStatus> GetAll(long repositoryId, string reference);

        /// <summary>
        /// Retrieves commit statuses for the specified reference. A reference can be a commit SHA, a branch name, or
        /// a tag name.
        /// </summary>
        /// <remarks>Only users with pull access can see this.</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>        
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<CommitStatus> GetAll(string owner, string name, string reference, ApiOptions options);

        /// <summary>
        /// Retrieves commit statuses for the specified reference. A reference can be a commit SHA, a branch name, or
        /// a tag name.
        /// </summary>
        /// <remarks>Only users with pull access can see this.</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<CommitStatus> GetAll(long repositoryId, string reference, ApiOptions options);

        /// <summary>
        /// Retrieves a combined view of statuses for the specified reference. A reference can be a commit SHA, a branch name, or
        /// a tag name.
        /// </summary>
        /// <remarks>Only users with pull access can see this.</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        IObservable<CombinedCommitStatus> GetCombined(string owner, string name, string reference);

        /// <summary>
        /// Retrieves a combined view of statuses for the specified reference. A reference can be a commit SHA, a branch name, or
        /// a tag name.
        /// </summary>
        /// <remarks>Only users with pull access can see this.</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        IObservable<CombinedCommitStatus> GetCombined(long repositoryId, string reference);

        /// <summary>
        /// Creates a commit status for the specified ref.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <param name="newCommitStatus">The commit status to create</param>
        IObservable<CommitStatus> Create(string owner, string name, string reference, NewCommitStatus newCommitStatus);

        /// <summary>
        /// Creates a commit status for the specified ref.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference (SHA, branch name, or tag name) to list commits for</param>
        /// <param name="newCommitStatus">The commit status to create</param>
        IObservable<CommitStatus> Create(long repositoryId, string reference, NewCommitStatus newCommitStatus);
    }
}
