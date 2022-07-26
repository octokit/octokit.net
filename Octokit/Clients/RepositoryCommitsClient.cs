using System.Collections.Generic;
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
        /// Compare two references in a repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/compare/{base}...{head}")]
        public Task<CompareResult> Compare(string owner, string name, string @base, string head)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(@base, nameof(@base));
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));

            return ApiConnection.Get<CompareResult>(ApiUrls.RepoCompare(owner, name, @base, head));
        }

        /// <summary>
        /// Compare two references in a repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="base">The reference to use as the base commit</param>
        /// <param name="head">The reference to use as the head commit</param>
        [ManualRoute("GET", "/repositories/{id}/compare/{base}...{head}")]
        public Task<CompareResult> Compare(long repositoryId, string @base, string head)
        {
            Ensure.ArgumentNotNullOrEmptyString(@base, nameof(@base));
            Ensure.ArgumentNotNullOrEmptyString(head, nameof(head));

            return ApiConnection.Get<CompareResult>(ApiUrls.RepoCompare(repositoryId, @base, head));
        }

        /// <summary>
        /// Gets a single commit for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference for the commit (SHA)</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{commit_sha}")]
        public Task<GitHubCommit> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<GitHubCommit>(ApiUrls.RepositoryCommit(owner, name, reference));
        }

        /// <summary>
        /// Gets a single commit for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference for the commit (SHA)</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{commit_sha}")]
        public Task<GitHubCommit> Get(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<GitHubCommit>(ApiUrls.RepositoryCommit(repositoryId, reference));
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits")]
        public Task<IReadOnlyList<GitHubCommit>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, new CommitRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        [ManualRoute("GET", "/repositories/{id}/commits")]
        public Task<IReadOnlyList<GitHubCommit>> GetAll(long repositoryId)
        {
            return GetAll(repositoryId, new CommitRequest(), ApiOptions.None);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits")]
        public Task<IReadOnlyList<GitHubCommit>> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, new CommitRequest(), options);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/commits")]
        public Task<IReadOnlyList<GitHubCommit>> GetAll(long repositoryId, ApiOptions options)
        {
            return GetAll(repositoryId, new CommitRequest(), options);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits")]
        public Task<IReadOnlyList<GitHubCommit>> GetAll(string owner, string name, CommitRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAll(owner, name, request, ApiOptions.None);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        [ManualRoute("GET", "/repositories/{id}/commits")]
        public Task<IReadOnlyList<GitHubCommit>> GetAll(long repositoryId, CommitRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAll(repositoryId, request, ApiOptions.None);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits")]
        public Task<IReadOnlyList<GitHubCommit>> GetAll(string owner, string name, CommitRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<GitHubCommit>(ApiUrls.RepositoryCommits(owner, name), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Gets all commits for a given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter list of commits returned</param>
        /// <param name="options">Options for changing the API response</param>
        [ManualRoute("GET", "/repositories/{id}/commits")]
        public Task<IReadOnlyList<GitHubCommit>> GetAll(long repositoryId, CommitRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<GitHubCommit>(ApiUrls.RepositoryCommits(repositoryId), request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Get the SHA-1 of a commit reference
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The repository reference</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/commits/{commit_sha}")]
        public Task<string> GetSha1(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<string>(ApiUrls.RepositoryCommit(owner, name, reference), null);
        }

        /// <summary>
        /// Get the SHA-1 of a commit reference
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The repository reference</param>
        [ManualRoute("GET", "/repositories/{id}/commits/{commit_sha}")]
        public Task<string> GetSha1(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<string>(ApiUrls.RepositoryCommit(repositoryId, reference), null);
        }
    }
}
