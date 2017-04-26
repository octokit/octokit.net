using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Pull Request Review Requests API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/pulls/review_requests/">Review Requests API documentation</a> for more information.
    /// </remarks>
    public interface IObservablePullRequestReviewRequestsClient
    {
        /// <summary>
        /// Gets review requests for a specified pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#list-review-requests</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        IObservable<User> GetAll(string owner, string name, int number);

        /// <summary>
        /// Creates review requests on a pull request for specified users.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#create-a-review-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The Pull Request number</param>
        /// <param name="users">List of logins of user will be requested for review</param>
        IObservable<PullRequest> Create(string owner, string name, int number, PullRequestReviewRequest users);

        /// <summary>
        /// Deletes review request for given users on a pull request.
        /// </summary>
        /// <remarks>https://developer.github.com/v3/pulls/review_requests/#delete-a-review-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request review comment number</param>
        /// <param name="users">List of logins of users that will be not longer requested for review</param>
        IObservable<Unit> Delete(string owner, string name, int number, PullRequestReviewRequest users);
    }
}