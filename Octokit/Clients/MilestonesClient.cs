using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Issue Milestones API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/milestones/">Issue Milestones API documentation</a> for more information.
    /// </remarks>
    public class MilestonesClient : ApiClient, IMilestonesClient
    {
        /// <summary>
        /// Instantiates a new GitHub Issue Milestones API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public MilestonesClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets a single Milestone by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#get-a-single-milestone
        /// </remarks>
        /// <returns></returns>
        public Task<Milestone> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<Milestone>(ApiUrls.Milestone(owner, name, number));
        }

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#list-milestones-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public Task<IReadOnlyList<Milestone>> GetForRepository(string owner, string name)
        {
            return GetForRepository(owner, name, new MilestoneRequest());
        }

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
        public Task<IReadOnlyList<Milestone>> GetForRepository(string owner, string name, MilestoneRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(request, "request");

            return ApiConnection.GetAll<Milestone>(ApiUrls.Milestones(owner, name),
                request.ToParametersDictionary());
        }

        /// <summary>
        /// Creates a milestone for the specified repository. Any user with pull access to a repository can create an
        /// Milestone.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/milestones/#create-a-milestone</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newMilestone">A <see cref="NewMilestone"/> instance describing the new Milestone to create</param>
        /// <returns></returns>
        public Task<Milestone> Create(string owner, string name, NewMilestone newMilestone)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newMilestone, "newMilestone");

            return ApiConnection.Post<Milestone>(ApiUrls.Milestones(owner, name), newMilestone);
        }

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
        public Task<Milestone> Update(string owner, string name, int number, MilestoneUpdate milestoneUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(milestoneUpdate, "milestoneUpdate");

            return ApiConnection.Patch<Milestone>(ApiUrls.Milestone(owner, name, number), milestoneUpdate);
        }

        /// <summary>
        /// Deletes a milestone for the specified repository. Any user with pull access to a repository can create an
        /// Milestone.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/milestones/#delete-a-milestone</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The milestone number</param>
        /// <returns></returns>
        public Task Delete(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Delete(ApiUrls.Milestone(owner, name, number));
        }
    }
}
