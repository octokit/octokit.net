using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Repository Statistics API.
    /// Note that the GitHub API uses caching on these endpoints,
    /// see <a href="https://developer.github.com/v3/repos/statistics/#a-word-about-caching">a word about caching</a> for more details.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/statistics/">Repository Statistics API documentation</a> for more information.
    /// </remarks>
    public interface IStatisticsClient
    {
        /// <summary>
        /// Returns a list of <see cref="Contributor"/> for the given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        Task<IReadOnlyList<Contributor>> GetContributors(string owner, string name);

        /// <summary>
        /// Returns a list of <see cref="Contributor"/> for the given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        Task<IReadOnlyList<Contributor>> GetContributors(long repositoryId);

        /// <summary>
        /// Returns a list of <see cref="Contributor"/> for the given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        Task<IReadOnlyList<Contributor>> GetContributors(string owner, string name, CancellationToken cancellationToken);

        /// <summary>
        /// Returns a list of <see cref="Contributor"/> for the given repository
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        Task<IReadOnlyList<Contributor>> GetContributors(long repositoryId, CancellationToken cancellationToken);

        /// <summary>
        /// Returns the last year of commit activity grouped by week.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        Task<CommitActivity> GetCommitActivity(string owner, string name);

        /// <summary>
        /// Returns the last year of commit activity grouped by week.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        Task<CommitActivity> GetCommitActivity(long repositoryId);

        /// <summary>
        /// Returns the last year of commit activity grouped by week.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        Task<CommitActivity> GetCommitActivity(string owner, string name, CancellationToken cancellationToken);

        /// <summary>
        /// Returns the last year of commit activity grouped by week.
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        Task<CommitActivity> GetCommitActivity(long repositoryId, CancellationToken cancellationToken);

        /// <summary>
        /// Returns a weekly aggregate of the number of additions and deletions pushed to a repository. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        Task<CodeFrequency> GetCodeFrequency(string owner, string name);

        /// <summary>
        /// Returns a weekly aggregate of the number of additions and deletions pushed to a repository. 
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        Task<CodeFrequency> GetCodeFrequency(long repositoryId);

        /// <summary>
        /// Returns a weekly aggregate of the number of additions and deletions pushed to a repository. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        Task<CodeFrequency> GetCodeFrequency(string owner, string name, CancellationToken cancellationToken);

        /// <summary>
        /// Returns a weekly aggregate of the number of additions and deletions pushed to a repository. 
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        Task<CodeFrequency> GetCodeFrequency(long repositoryId, CancellationToken cancellationToken);

        /// <summary>
        /// Returns the total commit counts for the owner and total commit counts in total. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        Task<Participation> GetParticipation(string owner, string name);

        /// <summary>
        /// Returns the total commit counts for the owner and total commit counts in total. 
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        Task<Participation> GetParticipation(long repositoryId);

        /// <summary>
        /// Returns the total commit counts for the owner and total commit counts in total. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        Task<Participation> GetParticipation(string owner, string name, CancellationToken cancellationToken);

        /// <summary>
        /// Returns the total commit counts for the owner and total commit counts in total. 
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        Task<Participation> GetParticipation(long repositoryId, CancellationToken cancellationToken);

        /// <summary>
        /// Returns a list of the number of commits per hour in each day
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        Task<PunchCard> GetPunchCard(string owner, string name);

        /// <summary>
        /// Returns a list of the number of commits per hour in each day
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        Task<PunchCard> GetPunchCard(long repositoryId);

        /// <summary>
        /// Returns a list of the number of commits per hour in each day
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        Task<PunchCard> GetPunchCard(string owner, string name, CancellationToken cancellationToken);

        /// <summary>
        /// Returns a list of the number of commits per hour in each day
        /// </summary>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="cancellationToken">A token used to cancel this potentially long running request</param>
        Task<PunchCard> GetPunchCard(long repositoryId, CancellationToken cancellationToken);
    }
}