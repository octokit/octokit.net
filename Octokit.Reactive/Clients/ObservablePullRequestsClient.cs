using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;
using System.Threading;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Pull Requests API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/pulls/">Pull Requests API documentation</a> for more information.
    /// </remarks>
    public class ObservablePullRequestsClient : IObservablePullRequestsClient
    {
        readonly IPullRequestsClient _client;
        readonly IConnection _connection;

        /// <summary>
        /// Client for managing reviews.
        /// </summary>
        public IObservablePullRequestReviewsClient Review { get; private set; }

        /// <summary>
        /// Client for managing review comments.
        /// </summary>
        public IObservablePullRequestReviewCommentsClient ReviewComment { get; private set; }

        /// <summary>
        /// Client for managing review requests.
        /// </summary>
        public IObservablePullRequestReviewRequestsClient ReviewRequest { get; private set; }

        /// <summary>
        /// Client for locking/unlocking a conversation on a pull request
        /// </summary>
        public IObservableLockUnlockClient LockUnlock { get; private set; }

        public ObservablePullRequestsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Repository.PullRequest;
            _connection = client.Connection;
            Review = new ObservablePullRequestReviewsClient(client);
            ReviewComment = new ObservablePullRequestReviewCommentsClient(client);
            ReviewRequest = new ObservablePullRequestReviewRequestsClient(client);
            LockUnlock = new ObservableLockUnlockClient(client);
        }

        /// <summary>
        /// Gets a single Pull Request by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#get-a-single-pull-request
        /// </remarks>
        public IObservable<PullRequest> Get(string owner, string name, int pullRequestNumber, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, pullRequestNumber, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets a single Pull Request by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#get-a-single-pull-request
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The number of the pull request</param>
        public IObservable<PullRequest> Get(long repositoryId, int pullRequestNumber, CancellationToken cancellationToken = default)
        {
            return _client.Get(repositoryId, pullRequestNumber, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<PullRequest> GetAllForRepository(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<PullRequest> GetAllForRepository(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequest> GetAllForRepository(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PullRequest>(ApiUrls.PullRequests(owner, name), null, options, cancellationToken);
        }

        /// <summary>
        /// Gets all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequest> GetAllForRepository(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PullRequest>(ApiUrls.PullRequests(repositoryId), null, options, cancellationToken);
        }

        /// <summary>
        /// Query pull requests for the repository based on criteria
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter and sort the list of pull requests returned</param>
        public IObservable<PullRequest> GetAllForRepository(string owner, string name, PullRequestRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(owner, name, request, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Query pull requests for the repository based on criteria
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of pull requests returned</param>
        public IObservable<PullRequest> GetAllForRepository(long repositoryId, PullRequestRequest request, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(repositoryId, request, ApiOptions.None, cancellationToken);
        }

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
        public IObservable<PullRequest> GetAllForRepository(string owner, string name, PullRequestRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PullRequest>(ApiUrls.PullRequests(owner, name),
                request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Query pull requests for the repository based on criteria
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of pull requests returned</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequest> GetAllForRepository(long repositoryId, PullRequestRequest request, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<PullRequest>(ApiUrls.PullRequests(repositoryId), request.ToParametersDictionary(), options, cancellationToken);
        }

        /// <summary>
        /// Creates a pull request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#create-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newPullRequest">A <see cref="NewPullRequest"/> instance describing the new PullRequest to create</param>
        public IObservable<PullRequest> Create(string owner, string name, NewPullRequest newPullRequest, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newPullRequest, nameof(newPullRequest));

            return _client.Create(owner, name, newPullRequest, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Creates a pull request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#create-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newPullRequest">A <see cref="NewPullRequest"/> instance describing the new PullRequest to create</param>
        public IObservable<PullRequest> Create(long repositoryId, NewPullRequest newPullRequest, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(newPullRequest, nameof(newPullRequest));

            return _client.Create(repositoryId, newPullRequest, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Update a pull request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#update-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The PullRequest number</param>
        /// <param name="pullRequestUpdate">An <see cref="PullRequestUpdate"/> instance describing the changes to make to the PullRequest
        /// </param>
        public IObservable<PullRequest> Update(string owner, string name, int pullRequestNumber, PullRequestUpdate pullRequestUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(pullRequestUpdate, nameof(pullRequestUpdate));

            return _client.Update(owner, name, pullRequestNumber, pullRequestUpdate, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Update a pull request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#update-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The PullRequest number</param>
        /// <param name="pullRequestUpdate">An <see cref="PullRequestUpdate"/> instance describing the changes to make to the PullRequest
        /// </param>
        public IObservable<PullRequest> Update(long repositoryId, int pullRequestNumber, PullRequestUpdate pullRequestUpdate, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(pullRequestUpdate, nameof(pullRequestUpdate));

            return _client.Update(repositoryId, pullRequestNumber, pullRequestUpdate, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Merge a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#merge-a-pull-request-merge-buttontrade</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="mergePullRequest">A <see cref="MergePullRequest"/> instance describing a pull request merge</param>
        public IObservable<PullRequestMerge> Merge(string owner, string name, int pullRequestNumber, MergePullRequest mergePullRequest, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(mergePullRequest, nameof(mergePullRequest));

            return _client.Merge(owner, name, pullRequestNumber, mergePullRequest, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Merge a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#merge-a-pull-request-merge-buttontrade</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="mergePullRequest">A <see cref="MergePullRequest"/> instance describing a pull request merge</param>
        public IObservable<PullRequestMerge> Merge(long repositoryId, int pullRequestNumber, MergePullRequest mergePullRequest, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(mergePullRequest, nameof(mergePullRequest));

            return _client.Merge(repositoryId, pullRequestNumber, mergePullRequest, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets the pull request merge status.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#get-if-a-pull-request-has-been-merged</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        public IObservable<bool> Merged(string owner, string name, int pullRequestNumber, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Merged(owner, name, pullRequestNumber, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets the pull request merge status.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#get-if-a-pull-request-has-been-merged</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        public IObservable<bool> Merged(long repositoryId, int pullRequestNumber, CancellationToken cancellationToken = default)
        {
            return _client.Merged(repositoryId, pullRequestNumber, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets the list of commits on a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#list-commits-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        public IObservable<PullRequestCommit> Commits(string owner, string name, int pullRequestNumber, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _connection.GetAndFlattenAllPages<PullRequestCommit>(ApiUrls.PullRequestCommits(owner, name, pullRequestNumber), cancellationToken);
        }

        /// <summary>
        /// Gets the list of commits on a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#list-commits-on-a-pull-request</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        public IObservable<PullRequestCommit> Commits(long repositoryId, int pullRequestNumber, CancellationToken cancellationToken = default)
        {
            return _connection.GetAndFlattenAllPages<PullRequestCommit>(ApiUrls.PullRequestCommits(repositoryId, pullRequestNumber), cancellationToken);
        }

        /// <summary>
        /// Get the list of files on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/#list-pull-requests-files</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequestFile> Files(string owner, string name, int pullRequestNumber, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _connection.GetAndFlattenAllPages<PullRequestFile>(ApiUrls.PullRequestFiles(owner, name, pullRequestNumber), options, cancellationToken);
        }

        /// <summary>
        /// Get the list of files on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/#list-pull-requests-files</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        public IObservable<PullRequestFile> Files(string owner, string name, int pullRequestNumber, CancellationToken cancellationToken = default)
        {
            return Files(owner, name, pullRequestNumber, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Get the list of files on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/#list-pull-requests-files</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<PullRequestFile> Files(long repositoryId, int pullRequestNumber, ApiOptions options, CancellationToken cancellationToken = default)
        {
            return _connection.GetAndFlattenAllPages<PullRequestFile>(ApiUrls.PullRequestFiles(repositoryId, pullRequestNumber), options, cancellationToken);
        }

        /// <summary>
        /// Get the list of files on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/#list-pull-requests-files</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="pullRequestNumber">The pull request number</param>
        public IObservable<PullRequestFile> Files(long repositoryId, int pullRequestNumber, CancellationToken cancellationToken = default)
        {
            return Files(repositoryId, pullRequestNumber, ApiOptions.None, cancellationToken);
        }
    }
}
