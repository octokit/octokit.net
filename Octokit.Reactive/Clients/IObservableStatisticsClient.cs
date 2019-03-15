using System;
using System.Collections.Generic;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Repository Statistics API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/statistics/">Repository Statistics API documentation</a> for more information.
    /// </remarks>
    /// <remarks>
    /// Computing repository statistics is an expensive operation, so we try to return cached data whenever possible.
    /// If the data hasn't been cached when you query a repository's statistics, you'll receive a 202 response; a background job is also fired to start compiling these statistics.
    /// Give the job a few moments to complete, and then submit the request again.
    /// If the job has completed, that request will receive a 200 response with the statistics in the response body.
    /// Repository statistics are cached by the SHA of the repository's default branch, which is usually master; pushing to the default branch resets the statistics cache.
    /// See <a href="https://developer.github.com/v3/repos/statistics/#a-word-about-caching">a word about caching</a> for more information.
    /// </remarks>
    public interface IObservableStatisticsClient
    {
        /// <summary>
        /// Returns a list of <see cref="Contributor"/> for the given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        IObservable<IEnumerable<Contributor>> GetContributors(string owner, string name);

        /// <summary>
        /// Returns a list of <see cref="Contributor"/> for the given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        IObservable<IEnumerable<Contributor>> GetContributors(long repositoryId);

        /// <summary>
        /// Returns the last year of commit activity grouped by week.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        IObservable<CommitActivity> GetCommitActivity(string owner, string name);

        /// <summary>
        /// Returns the last year of commit activity grouped by week.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        IObservable<CommitActivity> GetCommitActivity(long repositoryId);

        /// <summary>
        /// Returns a weekly aggregate of the number of additions and deletions pushed to a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        IObservable<CodeFrequency> GetCodeFrequency(string owner, string name);

        /// <summary>
        /// Returns a weekly aggregate of the number of additions and deletions pushed to a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        IObservable<CodeFrequency> GetCodeFrequency(long repositoryId);

        /// <summary>
        /// Returns the total commit counts for the owner and total commit counts in total. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        IObservable<Participation> GetParticipation(string owner, string name);

        /// <summary>
        /// Returns the total commit counts for the owner and total commit counts in total. 
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        IObservable<Participation> GetParticipation(long repositoryId);

        /// <summary>
        /// Returns a list of the number of commits per hour in each day
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        IObservable<PunchCard> GetPunchCard(string owner, string name);

        /// <summary>
        /// Returns a list of the number of commits per hour in each day
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        IObservable<PunchCard> GetPunchCard(long repositoryId);
    }
}