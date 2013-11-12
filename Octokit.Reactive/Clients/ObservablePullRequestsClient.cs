using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive.Clients
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
        /// <returns></returns>
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
        /// <returns></returns>
        public IObservable<PullRequest> GetForRepository(string owner, string name)
        {
            return _connection.GetAndFlattenAllPages<PullRequest>(ApiUrls.PullRequests(owner, name));
        }

        /// <summary>
        /// Gets all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter the list of Pull Requests returned</param>
        /// <returns></returns>
        public IObservable<PullRequest> GetForRepository(string owner, string name, PullRequestRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(request, "request");

            return _connection.GetAndFlattenAllPages<PullRequest>(ApiUrls.PullRequests(owner, name),
                request.ToParametersDictionary());
        }

        /// <summary>
        /// Creates a Pull Request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#create-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newPullRequest">A <see cref="NewPullRequest"/> instance describing the new PullRequest to create</param>
        /// <returns></returns>
        public IObservable<PullRequest> Create(string owner, string name, NewPullRequest newPullRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newPullRequest, "newPullRequest");

            return _client.Create(owner, name, newPullRequest).ToObservable();
        }

        /// <summary>
        /// Creates a Pull Request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#update-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The PullRequest number</param>
        /// <param name="pullRequestUpdate">An <see cref="PullRequestUpdate"/> instance describing the changes to make to the PullRequest
        /// </param>
        /// <returns></returns>
        public IObservable<PullRequest> Update(string owner, string name, int number, PullRequestUpdate pullRequestUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(pullRequestUpdate, "pullRequestUpdate");

            return _client.Update(owner, name, number, pullRequestUpdate).ToObservable();
        }

        /// <summary>
        /// Merges a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#merge-a-pull-request-merge-buttontrade</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="mergePullRequest">A <see cref="MergePullRequest"/> instance describing a pull request merge</param>
        /// <returns></returns>
        public IObservable<PullRequestMerge> Merge(string owner, string name, int number, MergePullRequest mergePullRequest) 
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(mergePullRequest, "mergePullRequest");

            return _client.Merge(owner, name, number, mergePullRequest).ToObservable();
        }
    }
}