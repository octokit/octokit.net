using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Pull Requests API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/activity/notifications/">Pull Requests API documentation</a> for more information.
    /// </remarks>
    public interface IPullRequestsClient
    {
        /// <summary>
        /// Client for managing reviews.
        /// </summary>
        IPullRequestReviewsClient Review { get; }

        /// <summary>
        /// Client for managing review comments.
        /// </summary>
        IPullRequestReviewCommentsClient ReviewComment { get; }

        /// <summary>
        /// Client for managing review requests.
        /// </summary>
        IPullRequestReviewRequestsClient ReviewRequest { get; }

        /// <summary>
        /// Client for locking/unlocking a coversation on a pull request
        /// </summary>
        ILockUnlockClient LockUnlock { get; }

        /// <summary>
        /// Get a pull request by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#get-a-single-pull-request
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
             Justification = "Method makes a network request")]
        Task<PullRequest> Get(string owner, string name, int pullRequestNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a pull request by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#get-a-single-pull-request
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
             Justification = "Method makes a network request")]
        Task<PullRequest> Get(long repositoryId, int pullRequestNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequest>> GetAllForRepository(string owner, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequest>> GetAllForRepository(long repositoryId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequest>> GetAllForRepository(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequest>> GetAllForRepository(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Query pull requests for the repository based on criteria
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter and sort the list of pull requests returned</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequest>> GetAllForRepository(string owner, string name, PullRequestRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Query pull requests for the repository based on criteria
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of pull requests returned</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequest>> GetAllForRepository(long repositoryId, PullRequestRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Query pull requests for the repository based on criteria
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter and sort the list of pull requests returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequest>> GetAllForRepository(string owner, string name, PullRequestRequest request, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Query pull requests for the repository based on criteria
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of pull requests returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequest>> GetAllForRepository(long repositoryId, PullRequestRequest request, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a pull request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#create-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newPullRequest">A <see cref="NewPullRequest"/> instance describing the new PullRequest to create</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequest> Create(string owner, string name, NewPullRequest newPullRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a pull request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#create-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newPullRequest">A <see cref="NewPullRequest"/> instance describing the new PullRequest to create</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequest> Create(long repositoryId, NewPullRequest newPullRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a pull request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#update-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The PullRequest number</param>
        /// <param name="pullRequestUpdate">An <see cref="PullRequestUpdate"/> instance describing the changes to make to the PullRequest
        /// </param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequest> Update(string owner, string name, int pullRequestNumber, PullRequestUpdate pullRequestUpdate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create a pull request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#update-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The PullRequest number</param>
        /// <param name="pullRequestUpdate">An <see cref="PullRequestUpdate"/> instance describing the changes to make to the PullRequest
        /// </param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequest> Update(long repositoryId, int pullRequestNumber, PullRequestUpdate pullRequestUpdate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Merge a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#merge-a-pull-request-merge-buttontrade</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="mergePullRequest">A <see cref="MergePullRequest"/> instance describing a pull request merge</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestMerge> Merge(string owner, string name, int pullRequestNumber, MergePullRequest mergePullRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Merge a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#merge-a-pull-request-merge-buttontrade</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="mergePullRequest">A <see cref="MergePullRequest"/> instance describing a pull request merge</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<PullRequestMerge> Merge(long repositoryId, int pullRequestNumber, MergePullRequest mergePullRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the pull request merge status.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#get-if-a-pull-request-has-been-merged</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<bool> Merged(string owner, string name, int pullRequestNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the pull request merge status.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#get-if-a-pull-request-has-been-merged</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<bool> Merged(long repositoryId, int pullRequestNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the list of commits on a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#list-commits-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestCommit>> Commits(string owner, string name, int pullRequestNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the list of commits on a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#list-commits-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestCommit>> Commits(long repositoryId, int pullRequestNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the list of files on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/#list-pull-requests-files</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestFile>> Files(string owner, string name, int pullRequestNumber, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the list of files on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/#list-pull-requests-files</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestFile>> Files(string owner, string name, int pullRequestNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the list of files on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/#list-pull-requests-files</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestFile>> Files(long repositoryId, int pullRequestNumber, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the list of files on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/#list-pull-requests-files</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<PullRequestFile>> Files(long repositoryId, int pullRequestNumber, CancellationToken cancellationToken = default);
    }
}
