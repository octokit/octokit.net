using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Commits API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/commits/">Repository Commits API documentation</a> for more information.
    /// </remarks>
    public class RepositoryCommitsClient : ApiClient, IRepositoryCommitsClient
    {
        public RepositoryCommitsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha1">Used to find all branches where the given commit SHA is the HEAD, or latest commit for the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{commit_sha}/branches-where-head")]
        public Task<IReadOnlyList<Branch>> BranchesWhereHead(long repositoryId, string sha1, CancellationToken cancellationToken = default)
        {
            return BranchesWhereHead(repositoryId, sha1, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha1">Used to find all branches where the given commit SHA is the HEAD, or latest commit for the branch</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{commit_sha}/branches-where-head")]
        public Task<IReadOnlyList<Branch>> BranchesWhereHead(long repositoryId, string sha1, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha1, nameof(sha1));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Branch>(ApiUrls.RepositoryCommitsBranchesWhereHead(repositoryId, sha1), null, options, cancellationToken);
        }

        /// <summary>
        /// List pull requests associated with a commit
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha1">Used to find all branches where the given commit SHA is the HEAD, or latest commit for the branch</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{commit_sha}/branches-where-head")]
        public Task<IReadOnlyList<Branch>> BranchesWhereHead(string owner, string name, string sha1, CancellationToken cancellationToken = default)
        {
            return BranchesWhereHead(owner, name, sha1, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha1">Used to find all branches where the given commit SHA is the HEAD, or latest commit for the branch</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{commit_sha}/branches-where-head")]
        public Task<IReadOnlyList<Branch>> BranchesWhereHead(string owner, string name, string sha1, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(sha1, nameof(sha1));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Branch>(ApiUrls.RepositoryCommitsBranchesWhereHead(owner, name, sha1), null, options, cancellationToken);
        }

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/compare/{base}...{head}")]
        public Task<CompareResult> Compare(string owner, string name, string @base, string head, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(@base, nameof(@base));
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));

            return ApiConnection.Get<CompareResult>(ApiUrls.RepoCompare(owner, name, @base, head), null, cancellationToken);
        }

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/compare/{base}...{head}")]
        public Task<CompareResult> Compare(long repositoryId, string @base, string head, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(@base, nameof(@base));
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));

            return ApiConnection.Get<CompareResult>(ApiUrls.RepoCompare(repositoryId, @base, head), null, cancellationToken);
        }

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/compare/{base}...{head}")]
        public Task<CompareResult> Compare(string owner, string name, string @base, string head, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(@base, nameof(@base));
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));
            Ensure.ArgumentNotNull(options, nameof(options));

            return Compare(ApiUrls.RepoCompare(owner, name, @base, head), options, cancellationToken);
        }

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/compare/{base}...{head}")]
        public Task<CompareResult> Compare(long repositoryId, string @base, string head, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(@base, nameof(@base));
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));
            Ensure.ArgumentNotNull(options, nameof(options));

            return Compare(ApiUrls.RepoCompare(repositoryId, @base, head), options, cancellationToken);
        }

        private async Task<CompareResult> Compare(Uri uri, ApiOptions options, CancellationToken cancellationToken)
        {
            var results = await ApiConnection.GetAll<CompareResult>(uri, options, cancellationToken);
            if (results.Count == 1) return results[0];

            var firstCompareResult = results[0];
            var commits = firstCompareResult.Commits.ToList();
            var files = firstCompareResult.Files.ToList();

            foreach (var compareResult in results.Skip(1))
            {
                commits.AddRange(compareResult.Commits ?? Array.Empty<GitHubCommit>());
                files.AddRange(compareResult.Files ?? Array.Empty<GitHubCommitFile>());
            }

            return new CompareResult(
                firstCompareResult.Url,
                firstCompareResult.HtmlUrl,
                firstCompareResult.PermalinkUrl,
                firstCompareResult.DiffUrl,
                firstCompareResult.PatchUrl,
                firstCompareResult.BaseCommit,
                firstCompareResult.MergeBaseCommit,
                firstCompareResult.Status,
                firstCompareResult.AheadBy,
                firstCompareResult.BehindBy,
                firstCompareResult.TotalCommits,
                commits,
                files);
        }

        /// <summary>
        /// Gets a single commit for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference for the commit (SHA)</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{commit_sha}")]
        public Task<GitHubCommit> Get(string owner, string name, string reference, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<GitHubCommit>(ApiUrls.RepositoryCommit(owner, name, reference), null, cancellationToken);
        }

        /// <summary>
        /// Gets a single commit for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference for the commit (SHA)</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{commit_sha}")]
        public Task<GitHubCommit> Get(long repositoryId, string reference, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<GitHubCommit>(ApiUrls.RepositoryCommit(repositoryId, reference), null, cancellationToken);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits")]
        public Task<IReadOnlyList<GitHubCommit>> GetAll(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, new CommitRequest(), ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/commits")]
        public Task<IReadOnlyList<GitHubCommit>> GetAll(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAll(repositoryId, new CommitRequest(), ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits")]
        public Task<IReadOnlyList<GitHubCommit>> GetAll(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, new CommitRequest(), options, cancellationToken);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/commits")]
        public Task<IReadOnlyList<GitHubCommit>> GetAll(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            return GetAll(repositoryId, new CommitRequest(), options, cancellationToken);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits")]
        public Task<IReadOnlyList<GitHubCommit>> GetAll(string owner, string name, CommitRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAll(owner, name, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/commits")]
        public Task<IReadOnlyList<GitHubCommit>> GetAll(long repositoryId, CommitRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAll(repositoryId, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits")]
        public Task<IReadOnlyList<GitHubCommit>> GetAll(string owner, string name, CommitRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<GitHubCommit>(ApiUrls.RepositoryCommits(owner, name), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/commits")]
        public Task<IReadOnlyList<GitHubCommit>> GetAll(long repositoryId, CommitRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<GitHubCommit>(ApiUrls.RepositoryCommits(repositoryId), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Get the SHA-1 of a commit reference
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The repository reference</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{commit_sha}")]
        public Task<string> GetSha1(string owner, string name, string reference, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<string>(ApiUrls.RepositoryCommit(owner, name, reference), null, cancellationToken);
        }

        /// <summary>
        /// Get the SHA-1 of a commit reference
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The repository reference</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{commit_sha}")]
        public Task<string> GetSha1(long repositoryId, string reference, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<string>(ApiUrls.RepositoryCommit(repositoryId, reference), null, cancellationToken);
        }

        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha1">Used to find all pull requests containing the provided commit SHA, which can be from any point in the commit history</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{commit_sha}/pulls")]
        public Task<IReadOnlyList<CommitPullRequest>> PullRequests(long repositoryId, string sha1, CancellationToken cancellationToken = default)
        {
            return PullRequests(repositoryId, sha1, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha1">Used to find all pull requests containing the provided commit SHA, which can be from any point in the commit history</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{commit_sha}/pulls")]
        public Task<IReadOnlyList<CommitPullRequest>> PullRequests(long repositoryId, string sha1, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha1, nameof(sha1));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<CommitPullRequest>(ApiUrls.RepositoryCommitsPull(repositoryId, sha1), null, options, cancellationToken);
        }

        /// <summary>
        /// List pull requests associated with a commit
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha1">Used to find all pull requests containing the provided commit SHA, which can be from any point in the commit history</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{commit_sha}/pulls")]
        public Task<IReadOnlyList<CommitPullRequest>> PullRequests(string owner, string name, string sha1, CancellationToken cancellationToken = default)
        {
            return PullRequests(owner, name, sha1, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha1">Used to find all pull requests containing the provided commit SHA, which can be from any point in the commit history</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{commit_sha}/pulls")]
        public Task<IReadOnlyList<CommitPullRequest>> PullRequests(string owner, string name, string sha1, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(sha1, nameof(sha1));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<CommitPullRequest>(ApiUrls.RepositoryCommitsPull(owner, name, sha1), null, options, cancellationToken);
        }
    }
}
