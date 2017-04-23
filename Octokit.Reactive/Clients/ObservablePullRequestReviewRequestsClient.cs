using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservablePullRequestReviewRequestsClient : IObservablePullRequestReviewRequestsClient
    {
        readonly IPullRequestReviewRequestsClient _client;
        readonly IConnection _connection;

        public ObservablePullRequestReviewRequestsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.PullRequest.ReviewRequest;
            _connection = client.Connection;
        }

        public IObservable<User> GetAll(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.GetAndFlattenAllPages<User>(ApiUrls.PullRequestReviewRequests(owner, name, number), null, AcceptHeaders.PullRequestReviewsApiPreview);
        }

        public IObservable<PullRequestReviewRequestCreate> Create(string owner, string name, int number, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(users, "users");

            return _client.Create(owner, name, number, users).ToObservable();
        }

        public IObservable<Unit> Delete(string owner, string name, int number, PullRequestReviewRequest users)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(users, "users");

            return _client.Delete(owner, name, number, users).ToObservable();
        }
    }
}