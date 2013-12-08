using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Issue Milestones API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/milestones/">Issue Milestones API documentation</a> for more information.
    /// </remarks>
    public interface IMilestonesClient
    {
        /// <summary>
        /// Gets a single Milestone by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#get-a-single-milestone
        /// </remarks>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
        Justification = "Method makes a network request")]
        Task<Milestone> Get(string owner, string name, int number);

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#list-milestones-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        Task<IReadOnlyList<Milestone>> GetForRepository(string owner, string name);

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#list-milestones-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter and sort the list of Milestones returned</param>
        /// <returns></returns>
        Task<IReadOnlyList<Milestone>> GetForRepository(string owner, string name, MilestoneRequest request);

        /// <summary>
        /// Creates a milestone for the specified repository. Any user with pull access to a repository can create an
        /// Milestone.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/milestones/#create-a-milestone</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newMilestone">A <see cref="NewMilestone"/> instance describing the new Milestone to create</param>
        /// <returns></returns>
        Task<Milestone> Create(string owner, string name, NewMilestone newMilestone);

        /// <summary>
        /// Creates a milestone for the specified repository. Any user with pull access to a repository can create an
        /// Milestone.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/milestones/#update-a-milestone</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The Milestone number</param>
        /// <param name="milestoneUpdate">An <see cref="MilestoneUpdate"/> instance describing the changes to make to the Milestone
        /// </param>
        /// <returns></returns>
        Task<Milestone> Update(string owner, string name, int number, MilestoneUpdate milestoneUpdate);

        /// <summary>
        /// Deletes a milestone for the specified repository. Any user with pull access to a repository can create an
        /// Milestone.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/milestones/#delete-a-milestone</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The milestone number</param>
        /// <returns></returns>
        Task Delete(string owner, string name, int number);
    }
}
