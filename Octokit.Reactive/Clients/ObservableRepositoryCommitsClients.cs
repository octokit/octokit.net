using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

using System.Threading;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Repository Commits API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/commits/">Repository Commits API documentation</a> for more information.
    /// </remarks>
    public class ObservableRepositoryCommitsClient : IObservableRepositoryCommitsClient
    {
        readonly IConnection _connection;
        readonly IRepositoryCommitsClient _commit;

        public ObservableRepositoryCommitsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _connection = client.Connection;
            _commit = client.Repository.Commit;
        }

        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha1">Used to find all branches where the given commit SHA is the HEAD, or latest commit for the branch</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{commit_sha}/branches-where-head")]
        public IObservable<Branch> BranchesWhereHead(long repositoryId, string sha1, CancellationToken cancellationToken = default)
        {
            return BranchesWhereHead(repositoryId, sha1, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha1">Used to find all branches where the given commit SHA is the HEAD, or latest commit for the branch</param>
        /// /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{commit_sha}/branches-where-head")]
        public IObservable<Branch> BranchesWhereHead(long repositoryId, string sha1, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha1, nameof(sha1));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Branch>(ApiUrls.RepositoryCommitsBranchesWhereHead(repositoryId, sha1), null, options, cancellationToken);
        }

        /// <summary>
        /// List pull requests associated with a commit
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha1">Used to find all branches where the given commit SHA is the HEAD, or latest commit for the branch</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{commit_sha}/branches-where-head")]
        public IObservable<Branch> BranchesWhereHead(string owner, string name, string sha1, CancellationToken cancellationToken = default)
        {
            return BranchesWhereHead(owner, name, sha1, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha1">Used to find all branches where the given commit SHA is the HEAD, or latest commit for the branch</param>
        /// /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{commit_sha}/branches-where-head")]
        public IObservable<Branch> BranchesWhereHead(string owner, string name, string sha1, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(sha1, nameof(sha1));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Branch>(ApiUrls.RepositoryCommitsBranchesWhereHead(owner, name, sha1), null, options, cancellationToken);
        }

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        public IObservable<CompareResult> Compare(string owner, string name, string @base, string head, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(@base, "base");
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));

            return _commit.Compare(owner, name, @base, head).ToObservable();
        }

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        public IObservable<CompareResult> Compare(long repositoryId, string @base, string head, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(@base, "base");
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));

            return _commit.Compare(repositoryId, @base, head).ToObservable();
        }

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<CompareResult> Compare(string owner, string name, string @base, string head, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(@base, "base");
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _commit.Compare(owner, name, @base, head, options).ToObservable();
        }

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<CompareResult> Compare(long repositoryId, string @base, string head, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(@base, "base");
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _commit.Compare(repositoryId, @base, head, options).ToObservable();
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference for the commit</param>
        public IObservable<GitHubCommit> Get(string owner, string name, string reference, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return _commit.Get(owner, name, reference).ToObservable();
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference for the commit</param>
        public IObservable<GitHubCommit> Get(long repositoryId, string reference, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return _commit.Get(repositoryId, reference).ToObservable();
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<GitHubCommit> GetAll(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, new CommitRequest(), ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<GitHubCommit> GetAll(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAll(repositoryId, new CommitRequest(), ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<GitHubCommit> GetAll(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAll(owner, name, new CommitRequest(), options, cancellationToken);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<GitHubCommit> GetAll(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAll(repositoryId, new CommitRequest(), options, cancellationToken);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        public IObservable<GitHubCommit> GetAll(string owner, string name, CommitRequest request, CancellationToken cancellationToken = default)
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
        public IObservable<GitHubCommit> GetAll(long repositoryId, CommitRequest request, CancellationToken cancellationToken = default)
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
        public IObservable<GitHubCommit> GetAll(string owner, string name, CommitRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<GitHubCommit>(ApiUrls.RepositoryCommits(owner, name), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<GitHubCommit> GetAll(long repositoryId, CommitRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<GitHubCommit>(ApiUrls.RepositoryCommits(repositoryId), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Get the SHA-1 of a commit reference
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The repository reference</param>
        public IObservable<string> GetSha1(string owner, string name, string reference, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return _commit.GetSha1(owner, name, reference).ToObservable();
        }

        /// <summary>
        /// Get the SHA-1 of a commit reference
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The repository reference</param>
        public IObservable<string> GetSha1(long repositoryId, string reference, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return _commit.GetSha1(repositoryId, reference).ToObservable();
        }

        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha1">Used to find all pull requests containing the provided commit SHA, which can be from any point in the commit history</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{commit_sha}/pulls")]
        public IObservable<CommitPullRequest> PullRequests(long repositoryId, string sha1, CancellationToken cancellationToken = default)
        {
            return PullRequests(repositoryId, sha1, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="sha1">Used to find all pull requests containing the provided commit SHA, which can be from any point in the commit history</param>
        /// /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{commit_sha}/pulls")]
        public IObservable<CommitPullRequest> PullRequests(long repositoryId, string sha1, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(sha1, nameof(sha1));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<CommitPullRequest>(ApiUrls.RepositoryCommitsPull(repositoryId, sha1), null, options, cancellationToken);
        }

        /// <summary>
        /// List pull requests associated with a commit
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha1">Used to find all pull requests containing the provided commit SHA, which can be from any point in the commit history</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{commit_sha}/pulls")]
        public IObservable<CommitPullRequest> PullRequests(string owner, string name, string sha1, CancellationToken cancellationToken = default)
        {
            return PullRequests(owner, name, sha1, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all pull requests for a given commit
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="sha1">Used to find all pull requests containing the provided commit SHA, which can be from any point in the commit history</param>
        /// /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{commit_sha}/pulls")]
        public IObservable<CommitPullRequest> PullRequests(string owner, string name, string sha1, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(sha1, nameof(sha1));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<CommitPullRequest>(ApiUrls.RepositoryCommitsPull(owner, name, sha1), null, options, cancellationToken);
        }
    }
}
