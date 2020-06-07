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
        [ManualRoute("GET", "/repos/{owner}/{repo}/milestones/{milestone_number}")]
        public Task<Milestone> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<Milestone>(ApiUrls.Milestone(owner, name, number));
        }

        /// <summary>
        /// Gets a single Milestone by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#get-a-single-milestone
        /// </remarks>
        /// <returns></returns>
        [ManualRoute("GET", "/repositories/{id}/milestones/{milestone_number}")]
        public Task<Milestone> Get(long repositoryId, int number)
        {
            return ApiConnection.Get<Milestone>(ApiUrls.Milestone(repositoryId, number));
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/milestones")]
        public Task<IReadOnlyList<Milestone>> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAllForRepository(owner, name, new MilestoneRequest());
        }

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#list-milestones-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns></returns>
        [ManualRoute("GET", "/repositories/{id}/milestones")]
        public Task<IReadOnlyList<Milestone>> GetAllForRepository(long repositoryId)
        {
            return GetAllForRepository(repositoryId, new MilestoneRequest());
        }

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#list-milestones-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        [ManualRoute("GET", "/repos/{owner}/{repo}/milestones")]
        public Task<IReadOnlyList<Milestone>> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForRepository(owner, name, new MilestoneRequest(), options);
        }

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#list-milestones-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        [ManualRoute("GET", "/repositories/{id}/milestones")]
        public Task<IReadOnlyList<Milestone>> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAllForRepository(repositoryId, new MilestoneRequest(), options);
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/milestones")]
        public Task<IReadOnlyList<Milestone>> GetAllForRepository(string owner, string name, MilestoneRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(owner, name, request, ApiOptions.None);
        }

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#list-milestones-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of Milestones returned</param>
        /// <returns></returns>
        [ManualRoute("GET", "/repositories/{id}/milestones")]
        public Task<IReadOnlyList<Milestone>> GetAllForRepository(long repositoryId, MilestoneRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAllForRepository(repositoryId, request, ApiOptions.None);
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
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        [ManualRoute("GET", "/repos/{owner}/{repo}/milestones")]
        public Task<IReadOnlyList<Milestone>> GetAllForRepository(string owner, string name, MilestoneRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Milestone>(ApiUrls.Milestones(owner, name),
                request.ToParametersDictionary(), options);
        }

        /// <summary>
        /// Gets all open milestones for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/milestones/#list-milestones-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="request">Used to filter and sort the list of Milestones returned</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        [ManualRoute("GET", "/repositories/{id}/milestones")]
        public Task<IReadOnlyList<Milestone>> GetAllForRepository(long repositoryId, MilestoneRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Milestone>(ApiUrls.Milestones(repositoryId),
                request.ToParametersDictionary(), options);
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
        [ManualRoute("POST", "/repos/{owner}/{repo}/milestones")]
        public Task<Milestone> Create(string owner, string name, NewMilestone newMilestone)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newMilestone, nameof(newMilestone));

            return ApiConnection.Post<Milestone>(ApiUrls.Milestones(owner, name), newMilestone);
        }

        /// <summary>
        /// Creates a milestone for the specified repository. Any user with pull access to a repository can create an
        /// Milestone.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/milestones/#create-a-milestone</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newMilestone">A <see cref="NewMilestone"/> instance describing the new Milestone to create</param>
        /// <returns></returns>
        [ManualRoute("POST", "/repositories/{id}/milestones")]
        public Task<Milestone> Create(long repositoryId, NewMilestone newMilestone)
        {
            Ensure.ArgumentNotNull(newMilestone, nameof(newMilestone));

            return ApiConnection.Post<Milestone>(ApiUrls.Milestones(repositoryId), newMilestone);
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
        [ManualRoute("PATCH", "/repos/{owner}/{repo}/milestones/{milestone_number}")]
        public Task<Milestone> Update(string owner, string name, int number, MilestoneUpdate milestoneUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(milestoneUpdate, nameof(milestoneUpdate));

            return ApiConnection.Patch<Milestone>(ApiUrls.Milestone(owner, name, number), milestoneUpdate);
        }

        /// <summary>
        /// Creates a milestone for the specified repository. Any user with pull access to a repository can create an
        /// Milestone.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/milestones/#update-a-milestone</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The Milestone number</param>
        /// <param name="milestoneUpdate">An <see cref="MilestoneUpdate"/> instance describing the changes to make to the Milestone
        /// </param>
        /// <returns></returns>
        [ManualRoute("PATCH", "/repositories/{id}/milestones/{milestone_number}")]
        public Task<Milestone> Update(long repositoryId, int number, MilestoneUpdate milestoneUpdate)
        {
            Ensure.ArgumentNotNull(milestoneUpdate, nameof(milestoneUpdate));

            return ApiConnection.Patch<Milestone>(ApiUrls.Milestone(repositoryId, number), milestoneUpdate);
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
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/milestones/{milestone_number}")]
        public Task Delete(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Delete(ApiUrls.Milestone(owner, name, number));
        }

        /// <summary>
        /// Deletes a milestone for the specified repository. Any user with pull access to a repository can create an
        /// Milestone.
        /// </summary>
        /// <remarks>http://developer.github.com/v3/issues/milestones/#delete-a-milestone</remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="number">The milestone number</param>
        /// <returns></returns>
        [ManualRoute("DELETE", "/repositories/{id}/milestones/{milestone_number}")]
        public Task Delete(long repositoryId, int number)
        {
            return ApiConnection.Delete(ApiUrls.Milestone(repositoryId, number));
        }
    }
}
