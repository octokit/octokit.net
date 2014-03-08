using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableStatisticsClient : IObservableStatisticsClient
    {
        readonly IGitHubClient _client;

        public ObservableStatisticsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");
            _client = client;
        }

        /// <summary>
        /// Returns a list of <see cref="Contributor"/> for the given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>A list of <see cref="Contributor"/></returns>
        public IObservable<IEnumerable<Contributor>> GetContributors(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.Repository.Statistics.GetContributors(owner, repositoryName).ToObservable();
        }

        /// <summary>
        /// Returns the last year of commit activity grouped by week.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>The last year of  <see cref="CommitActivity"/></returns>
        public IObservable<CommitActivity> GetCommitActivity(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.Repository.Statistics.GetCommitActivity(owner, repositoryName).ToObservable();
        }

        /// <summary>
        /// Returns a weekly aggregate of the number of additions and deletions pushed to a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>Returns a weekly aggregate of the number additions and deletion</returns>
        public IObservable<CodeFrequency> GetCodeFrequency(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.Repository.Statistics.GetCodeFrequency(owner, repositoryName).ToObservable();
        }

        /// <summary>
        /// Returns the total commit counts for the owner and total commit counts in total. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>Returns <see cref="Participation"/>from oldest week to now</returns>
        public IObservable<Participation> GetParticipation(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.Repository.Statistics.GetParticipation(owner, repositoryName).ToObservable();
        }

        /// <summary>
        /// Returns a list of the number of commits per hour in each day
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>Returns commit counts per hour in each day</returns>
        public IObservable<PunchCard> GetPunchCard(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            return _client.Repository.Statistics.GetPunchCard(owner, repositoryName).ToObservable();
        }
    }
}