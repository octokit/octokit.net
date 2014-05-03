using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    public class StatisticsClient : ApiClient, IStatisticsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Statistics API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public StatisticsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Returns a list of <see cref="Contributor"/> for the given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>A list of <see cref="Contributor"/></returns>
        public Task<IEnumerable<Contributor>> GetContributors(string owner, string repositoryName)
        {
            return GetContributors(owner, repositoryName, CancellationToken.None);
        }

        /// <summary>
        /// Returns a list of <see cref="Contributor"/> for the given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        /// <returns>A list of <see cref="Contributor"/></returns>
        public async Task<IEnumerable<Contributor>> GetContributors(string owner, string repositoryName, CancellationToken cancellationToken)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            var endpoint = "/repos/{0}/{1}/stats/contributors".FormatUri(owner, repositoryName);
            return await ApiConnection.GetQueuedOperation<IEnumerable<Contributor>>(endpoint, cancellationToken);
        }

        /// <summary>
        /// Returns the last year of commit activity grouped by week.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>The last year of  <see cref="CommitActivity"/></returns>
        public Task<CommitActivity> GetCommitActivity(string owner, string repositoryName)
        {
            return GetCommitActivity(owner, repositoryName, CancellationToken.None);
        }

        /// <summary>
        /// Returns the last year of commit activity grouped by week.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        /// <returns>The last year of  <see cref="CommitActivity"/></returns>
        public async Task<CommitActivity> GetCommitActivity(string owner, string repositoryName, CancellationToken cancellationToken)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            var endpoint = "/repos/{0}/{1}/stats/commit_activity".FormatUri(owner, repositoryName);
            var activity = await ApiConnection.GetQueuedOperation<IEnumerable<WeeklyCommitActivity>>(endpoint,cancellationToken);
            return new CommitActivity(activity);
        }

        /// <summary>
        /// Returns a weekly aggregate of the number of additions and deletions pushed to a repository. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>Returns a weekly aggregate of the number additions and deletion</returns>
        public Task<CodeFrequency> GetCodeFrequency(string owner, string repositoryName)
        {
            return GetCodeFrequency(owner, repositoryName, CancellationToken.None);
        }

        /// <summary>
        /// Returns a weekly aggregate of the number of additions and deletions pushed to a repository. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        /// <returns>Returns a weekly aggregate of the number additions and deletion</returns>
        public async Task<CodeFrequency> GetCodeFrequency(string owner, string repositoryName, CancellationToken cancellationToken)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            var endpoint = "/repos/{0}/{1}/stats/code_frequency".FormatUri(owner, repositoryName);
            var rawFrequencies = await ApiConnection.GetQueuedOperation<IEnumerable<long[]>>(endpoint,cancellationToken);
            return new CodeFrequency(rawFrequencies);
        }

        /// <summary>
        /// Returns the total commit counts for the owner and total commit counts in total. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>Returns <see cref="Participation"/>from oldest week to now</returns>
        public Task<Participation> GetParticipation(string owner, string repositoryName)
        {
            return GetParticipation(owner, repositoryName, CancellationToken.None);
        }

        /// <summary>
        /// Returns the total commit counts for the owner and total commit counts in total. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        /// <returns>Returns <see cref="Participation"/>from oldest week to now</returns>
        public async Task<Participation> GetParticipation(string owner, string repositoryName, CancellationToken cancellationToken)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            var endpoint = "/repos/{0}/{1}/stats/participation".FormatUri(owner, repositoryName);
            return await ApiConnection.GetQueuedOperation<Participation>(endpoint,cancellationToken);
        }

        /// <summary>
        /// Returns a list of the number of commits per hour in each day
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>Returns commit counts per hour in each day</returns>
        public Task<PunchCard> GetPunchCard(string owner, string repositoryName)
        {
            return GetPunchCard(owner, repositoryName,CancellationToken.None);
        }

        /// <summary>
        /// Returns a list of the number of commits per hour in each day
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        /// <returns>Returns commit counts per hour in each day</returns>
        public async Task<PunchCard> GetPunchCard(string owner, string repositoryName, CancellationToken cancellationToken)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            var endpoint = "/repos/{0}/{1}/stats/punch_card".FormatUri(owner, repositoryName);
            var punchCardData = await ApiConnection.GetQueuedOperation<IEnumerable<int[]>>(endpoint, cancellationToken);
            return new PunchCard(punchCardData);
        }
    }
}