using System;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservablePullRequestsClient : IObservablePullRequestsClient
    {
        readonly IPullRequestsClient _client;
        readonly IConnection _connection;

        public ObservablePullRequestsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Repository.PullRequest;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets a single Pull Request by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#get-a-single-pull-request
        /// </remarks>
        /// <returns>A <see cref="PullRequest"/> result</returns>
        public IObservable<PullRequest> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Get(owner, name, number).ToObservable();
        }

        /// <summary>
        /// Gets all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>A collection of <see cref="PullRequest"/> results</returns>
        public IObservable<PullRequest> GetForRepository(string owner, string name)
        {
            return _connection.GetAndFlattenAllPages<PullRequest>(ApiUrls.PullRequests(owner, name));
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
        /// <returns>A collection of <see cref="PullRequest"/> results</returns>
        public IObservable<PullRequest> GetForRepository(string owner, string name, PullRequestRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(request, "request");

            return _connection.GetAndFlattenAllPages<PullRequest>(ApiUrls.PullRequests(owner, name),
                request.ToParametersDictionary());
        }

        /// <summary>
        /// Creates a pull request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#create-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newPullRequest">A <see cref="NewPullRequest"/> instance describing the new PullRequest to create</param>
        /// <returns>A created <see cref="PullRequest"/> result</returns>
        public IObservable<PullRequest> Create(string owner, string name, NewPullRequest newPullRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newPullRequest, "newPullRequest");

            return _client.Create(owner, name, newPullRequest).ToObservable();
        }

        /// <summary>
        /// Update a pull request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#update-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The PullRequest number</param>
        /// <param name="pullRequestUpdate">An <see cref="PullRequestUpdate"/> instance describing the changes to make to the PullRequest
        /// </param>
        /// <returns>An updated <see cref="PullRequest"/> result</returns>
        public IObservable<PullRequest> Update(string owner, string name, int number, PullRequestUpdate pullRequestUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(pullRequestUpdate, "pullRequestUpdate");

            return _client.Update(owner, name, number, pullRequestUpdate).ToObservable();
        }

        /// <summary>
        /// Merge a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#merge-a-pull-request-merge-buttontrade</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="mergePullRequest">A <see cref="MergePullRequest"/> instance describing a pull request merge</param>
        /// <returns>A <see cref="PullRequestMerge"/> result</returns>
        public IObservable<PullRequestMerge> Merge(string owner, string name, int number, MergePullRequest mergePullRequest) 
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(mergePullRequest, "mergePullRequest");

            return _client.Merge(owner, name, number, mergePullRequest).ToObservable();
        }

        /// <summary>
        /// Gets the pull request merge status.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#get-if-a-pull-request-has-been-merged</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>A <see cref="bool"/> result - true if the pull request has been merged, false otherwise</returns>
        public IObservable<bool> Merged(string owner, string name, int number) 
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Merged(owner, name, number).ToObservable();
        }

        /// <summary>
        /// Gets the list of commits on a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#list-commits-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>A collection of <see cref="PullRequestCommit"/> results</returns>
        public IObservable<PullRequestCommit> Commits(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.GetAndFlattenAllPages<PullRequestCommit>(ApiUrls.PullRequestCommits(owner, name, number));
        }
    }
}