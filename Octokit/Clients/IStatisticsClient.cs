using System.Collections.Generic;
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
        /// Returns a list of last year of commit activity by <see cref="WeeklyCommitActivity"/>. 
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>A list of <see cref="WeeklyCommitActivity"/></returns>
        Task<IEnumerable<WeeklyCommitActivity>> GetCommitActivityForTheLastYear(string owner, string repositoryName);
    }
}