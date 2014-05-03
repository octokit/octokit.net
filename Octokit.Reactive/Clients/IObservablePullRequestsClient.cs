using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Reactive
{
    public interface IObservablePullRequestsClient
    {
        /// <summary>
        /// Gets a single Pull Request by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#get-a-single-pull-request
        /// </remarks>
        /// <returns>A <see cref="PullRequest"/> result</returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
        Justification = "Method makes a network request")]
        IObservable<PullRequest> Get(string owner, string name, int number);

        /// <summary>
        /// Gets all open pull requests for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/pulls/#list-pull-requests
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>A collection of <see cref="PullRequest"/> results</returns>
        IObservable<PullRequest> GetForRepository(string owner, string name);

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
        IObservable<PullRequest> GetForRepository(string owner, string name, PullRequestRequest request);

        /// <summary>
        /// Creates a pull request for the specified repository.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#create-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newPullRequest">A <see cref="NewPullRequest"/> instance describing the new PullRequest to create</param>
        /// <returns>A created <see cref="PullRequest"/> result</returns>
        IObservable<PullRequest> Create(string owner, string name, NewPullRequest newPullRequest);

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
        IObservable<PullRequest> Update(string owner, string name, int number, PullRequestUpdate pullRequestUpdate);

        /// <summary>
        /// Merge a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#merge-a-pull-request-merge-buttontrade</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <param name="mergePullRequest">A <see cref="MergePullRequest"/> instance describing a pull request merge</param>
        /// <returns>A <see cref="PullRequestMerge"/> result</returns>
        IObservable<PullRequestMerge> Merge(string owner, string name, int number, MergePullRequest mergePullRequest);

        /// <summary>
        /// Gets the pull request merge status.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#get-if-a-pull-request-has-been-merged</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>A <see cref="bool"/> result - true if the pull request has been merged, false otherwise</returns>
        IObservable<bool> Merged(string owner, string name, int number);

        /// <summary>
        /// Gets the list of commits on a pull request.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/pulls/#list-commits-on-a-pull-request</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The pull request number</param>
        /// <returns>A collection of <see cref="PullRequestCommit"/> results</returns>
        IObservable<PullRequestCommit> Commits(string owner, string name, int number);
    }
}
