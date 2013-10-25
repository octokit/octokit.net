using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class MilestonesClient : ApiClient, IMilestonesClient
    {
        public MilestonesClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all Milestones across all the authenticated user’s visible repositories including owned repositories, 
        /// member repositories, and organization repositories.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/Milestones/#get-a-single-Milestone
        /// </remarks>
        /// <returns></returns>
        public async Task<Milestone> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return await ApiConnection.Get<Milestone>(ApiUrls.Milestone(owner, name, number));

        }

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/Milestones/#list-Milestones-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Milestone>> GetForRepository(string owner, string name)
        {
            return await GetForRepository(owner, name, new MilestoneRequest());
        }

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/Milestones/#list-Milestones-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to filter and sort the list of Milestones returned</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Milestone>> GetForRepository(string owner, string name, MilestoneRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(request, "request");

            return await ApiConnection.GetAll<Milestone>(ApiUrls.Milestones(owner, name),
                request.ToParametersDictionary());
        }

        /// <summary>
        /// Creates an Milestone for the specified repository. Any user with pull access to a repository can create an
        /// Milestone.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/Milestones/#create-an-Milestone</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newMilestone">A <see cref="NewMilestone"/> instance describing the new Milestone to create</param>
        /// <returns></returns>
        public async Task<Milestone> Create(string owner, string name, NewMilestone newMilestone)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newMilestone, "newMilestone");

            return await ApiConnection.Post<Milestone>(ApiUrls.Milestones(owner, name), newMilestone);
        }

        /// <summary>
        /// Creates an Milestone for the specified repository. Any user with pull access to a repository can create an
        /// Milestone.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/Milestones/#create-an-Milestone</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The Milestone number</param>
        /// <param name="milestoneUpdate">An <see cref="MilestoneUpdate"/> instance describing the changes to make to the Milestone
        /// </param>
        /// <returns></returns>
        public async Task<Milestone> Update(string owner, string name, int number, MilestoneUpdate milestoneUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(milestoneUpdate, "milestoneUpdate");

            return await ApiConnection.Patch<Milestone>(ApiUrls.Milestone(owner, name, number), milestoneUpdate);
        }

        /// <summary>
        /// Deletes a milestone for the specified repository. Any user with pull access to a repository can create an
        /// Milestone.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/Milestones/#delete-a-milestone</remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The milestone number</param>
        /// <returns></returns>
        public async Task Delete(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            await ApiConnection.Delete(ApiUrls.Milestone(owner, name, number));
        }
    }
}
