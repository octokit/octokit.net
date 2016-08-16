using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Repository Commits API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/commits/">Repository Commits API documentation</a> for more information.
    /// </remarks>
    public interface IObservableRepositoryCommitsClient
    {
        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "base")]
        IObservable<CompareResult> Compare(string owner, string name, string @base, string head);

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "base")]
        IObservable<CompareResult> Compare(int repositoryId, string @base, string head);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference for the commit</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        IObservable<GitHubCommit> Get(string owner, string name, string reference);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference for the commit</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        IObservable<GitHubCommit> Get(int repositoryId, string reference);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        IObservable<GitHubCommit> GetAll(string owner, string name);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        IObservable<GitHubCommit> GetAll(int repositoryId);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<GitHubCommit> GetAll(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<GitHubCommit> GetAll(int repositoryId, ApiOptions options);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        IObservable<GitHubCommit> GetAll(string owner, string name, CommitRequest request);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        IObservable<GitHubCommit> GetAll(int repositoryId, CommitRequest request);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<GitHubCommit> GetAll(string owner, string name, CommitRequest request, ApiOptions options);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        /// <param name="options">Options for changing the API response</param>
        IObservable<GitHubCommit> GetAll(int repositoryId, CommitRequest request, ApiOptions options);

        /// <summary>
        /// Get the SHA-1 of a commit reference
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The repository reference</param>
        IObservable<string> GetSha1(string owner, string name, string reference);

        /// <summary>
        /// Get the SHA-1 of a commit reference
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The repository reference</param>
        IObservable<string> GetSha1(int repositoryId, string reference);
    }
}
