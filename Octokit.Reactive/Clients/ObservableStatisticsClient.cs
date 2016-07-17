using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Repository Statistics API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/statistics/">Repository Statistics API documentation</a> for more information.
    /// </remarks>
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
        /// <param name="name">The name of the repository</param>
        public IObservable<IEnumerable<Contributor>> GetContributors(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Repository.Statistics.GetContributors(owner, name).ToObservable();
        }

        /// <summary>
        /// Returns a list of <see cref="Contributor"/> for the given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<IEnumerable<Contributor>> GetContributors(int repositoryId)
        {
            return _client.Repository.Statistics.GetContributors(repositoryId).ToObservable();
        }

        /// <summary>
        /// Returns the last year of commit activity grouped by week.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<CommitActivity> GetCommitActivity(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Repository.Statistics.GetCommitActivity(owner, name).ToObservable();
        }

        /// <summary>
        /// Returns the last year of commit activity grouped by week.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<CommitActivity> GetCommitActivity(int repositoryId)
        {
            return _client.Repository.Statistics.GetCommitActivity(repositoryId).ToObservable();
        }

        /// <summary>
        /// Returns a weekly aggregate of the number of additions and deletions pushed to a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<CodeFrequency> GetCodeFrequency(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Repository.Statistics.GetCodeFrequency(owner, name).ToObservable();
        }

        /// <summary>
        /// Returns a weekly aggregate of the number of additions and deletions pushed to a repository.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<CodeFrequency> GetCodeFrequency(int repositoryId)
        {
            return _client.Repository.Statistics.GetCodeFrequency(repositoryId).ToObservable();
        }

        /// <summary>
        /// Returns the total commit counts for the owner and total commit counts in total. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<Participation> GetParticipation(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Repository.Statistics.GetParticipation(owner, name).ToObservable();
        }

        /// <summary>
        /// Returns the total commit counts for the owner and total commit counts in total. 
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<Participation> GetParticipation(int repositoryId)
        {
            return _client.Repository.Statistics.GetParticipation(repositoryId).ToObservable();
        }

        /// <summary>
        /// Returns a list of the number of commits per hour in each day
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        public IObservable<PunchCard> GetPunchCard(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Repository.Statistics.GetPunchCard(owner, name).ToObservable();
        }

        /// <summary>
        /// Returns a list of the number of commits per hour in each day
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        public IObservable<PunchCard> GetPunchCard(int repositoryId)
        {
            return _client.Repository.Statistics.GetPunchCard(repositoryId).ToObservable();
        }
    }
}
