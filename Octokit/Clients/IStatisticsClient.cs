using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IStatisticsClient
    {
        /// <summary>
        /// Returns a list of <see cref="Contributor"/> for the given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>A list of <see cref="Contributor"/></returns>
        Task<IEnumerable<Contributor>> GetContributors(string owner, string repositoryName);

        /// <summary>
        /// Returns a list of <see cref="Contributor"/> for the given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        /// <returns>A list of <see cref="Contributor"/></returns>
        Task<IEnumerable<Contributor>> GetContributors(string owner, string repositoryName, CancellationToken cancellationToken);

        /// <summary>
        /// Returns the last year of commit activity grouped by week.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>The last year of  <see cref="CommitActivity"/></returns>
        Task<CommitActivity> GetCommitActivity(string owner, string repositoryName);

        /// <summary>
        /// Returns the last year of commit activity grouped by week.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        /// <returns>The last year of  <see cref="CommitActivity"/></returns>
        Task<CommitActivity> GetCommitActivity(string owner, string repositoryName, CancellationToken cancellationToken);

        /// <summary>
        /// Returns a weekly aggregate of the number of additions and deletions pushed to a repository. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>Returns a weekly aggregate of the number additions and deletion</returns>
        Task<CodeFrequency> GetCodeFrequency(string owner, string repositoryName);

        /// <summary>
        /// Returns a weekly aggregate of the number of additions and deletions pushed to a repository. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        /// <returns>Returns a weekly aggregate of the number additions and deletion</returns>
        Task<CodeFrequency> GetCodeFrequency(string owner, string repositoryName, CancellationToken cancellationToken);

        /// <summary>
        /// Returns the total commit counts for the owner and total commit counts in total. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>Returns <see cref="Participation"/>from oldest week to now</returns>
        Task<Participation> GetParticipation(string owner, string repositoryName);

        /// <summary>
        /// Returns the total commit counts for the owner and total commit counts in total. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        /// <returns>Returns <see cref="Participation"/>from oldest week to now</returns>
        Task<Participation> GetParticipation(string owner, string repositoryName, CancellationToken cancellationToken);

        /// <summary>
        /// Returns a list of the number of commits per hour in each day
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>Returns commit counts per hour in each day</returns>
        Task<PunchCard> GetPunchCard(string owner, string repositoryName);

        /// <summary>
        /// Returns a list of the number of commits per hour in each day
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        /// <returns>Returns commit counts per hour in each day</returns>
        Task<PunchCard> GetPunchCard(string owner, string repositoryName, CancellationToken cancellationToken);
    }
}