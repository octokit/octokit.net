using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Commits API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/commits/">Repository Commits API documentation</a> for more information.
    /// </remarks>
    public interface IRepositoryCommitsClient
    {
        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha1">Used to find all branches where the given commit SHA is the HEAD, or latest commit for the branch</param>
        Task<IReadOnlyList<Branch>> BranchesWhereHead(long repositoryId, string sha1);

        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha1">Used to find all branches where the given commit SHA is the HEAD, or latest commit for the branch</param>
        /// /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<Branch>> BranchesWhereHead(long repositoryId, string sha1, ApiOptions options);

        /// <summary>
        /// List pull requests associated with a commit
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha1">Used to find all branches where the given commit SHA is the HEAD, or latest commit for the branch</param>
        Task<IReadOnlyList<Branch>> BranchesWhereHead(string owner, string name, string sha1);

        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha1">Used to find all branches where the given commit SHA is the HEAD, or latest commit for the branch</param>
        /// /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<Branch>> BranchesWhereHead(string owner, string name, string sha1, ApiOptions options);

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "base")]
        Task<CompareResult> Compare(string owner, string name, string @base, string head);

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "base")]
        Task<CompareResult> Compare(long repositoryId, string @base, string head);

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        /// <param name="options">Options for changing the API response</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "base")]
        Task<CompareResult> Compare(string owner, string name, string @base, string head, ApiOptions options);

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        /// <param name="options">Options for changing the API response</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "base")]
        Task<CompareResult> Compare(long repositoryId, string @base, string head, ApiOptions options);

        /// <summary>
        /// Gets a single commit for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference for the commit (SHA)</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Makes a network request")]
        Task<GitHubCommit> Get(string owner, string name, string reference);

        /// <summary>
        /// Gets a single commit for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference for the commit (SHA)</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Makes a network request")]
        Task<GitHubCommit> Get(long repositoryId, string reference);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        Task<IReadOnlyList<GitHubCommit>> GetAll(string owner, string name);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        Task<IReadOnlyList<GitHubCommit>> GetAll(long repositoryId);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<GitHubCommit>> GetAll(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<GitHubCommit>> GetAll(long repositoryId, ApiOptions options);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        Task<IReadOnlyList<GitHubCommit>> GetAll(string owner, string name, CommitRequest request);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        Task<IReadOnlyList<GitHubCommit>> GetAll(long repositoryId, CommitRequest request);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<GitHubCommit>> GetAll(string owner, string name, CommitRequest request, ApiOptions options);

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<GitHubCommit>> GetAll(long repositoryId, CommitRequest request, ApiOptions options);

        /// <summary>
        /// Get the SHA-1 of a commit reference
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The repository reference</param>
        Task<string> GetSha1(string owner, string name, string reference);

        /// <summary>
        /// Get the SHA-1 of a commit reference
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The repository reference</param>
        Task<string> GetSha1(long repositoryId, string reference);

        /// <summary>
        /// List pull requests associated with a commit
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha1">Used to find all pull requests containing the provided commit SHA, which can be from any point in the commit history</param>
        Task<IReadOnlyList<CommitPullRequest>> PullRequests(string owner, string name, string sha1);

        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha1">Used to find all pull requests containing the provided commit SHA, which can be from any point in the commit history</param>
        Task<IReadOnlyList<CommitPullRequest>> PullRequests(long repositoryId, string sha1);

        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha1">Used to find all pull requests containing the provided commit SHA, which can be from any point in the commit history</param>
        /// /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<CommitPullRequest>> PullRequests(long repositoryId, string sha1, ApiOptions options);

        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha1">Used to find all pull requests containing the provided commit SHA, which can be from any point in the commit history</param>
        /// /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<CommitPullRequest>> PullRequests(string owner, string name, string sha1, ApiOptions options);
    }
}
