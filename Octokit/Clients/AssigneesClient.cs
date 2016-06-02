using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Issue Assignees API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/assignees/">Issue Assignees API documentation</a> for more information.
    /// </remarks>
    public class AssigneesClient : ApiClient, IAssigneesClient
    {
        /// <summary>
        /// Instantiates a new GitHub Issue Assignees API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public AssigneesClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all the available assignees (owner + collaborators) to which issues may be assigned.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public Task<IReadOnlyList<User>> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAllForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the available assignees (owner + collaborators) to which issues may be assigned.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">The options to change API's response.</param>
        /// <returns></returns>
        public Task<IReadOnlyList<User>> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            var endpoint = ApiUrls.Assignees(owner, name);

            return ApiConnection.GetAll<User>(endpoint, null, AcceptHeaders.StableVersion, options);
        }

        /// <summary>
        /// Checks to see if a user is an assignee for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="assignee">Username of the prospective assignee</param>
        /// <returns></returns>
        public async Task<bool> CheckAssignee(string owner, string name, string assignee)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(assignee, "assignee");

            try
            {
                var response = await Connection.Get<object>(ApiUrls.CheckAssignee(owner, name, assignee), null, null).ConfigureAwait(false);
                return response.HttpResponse.IsTrue();
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Add assignees to a specified Issue.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="assignees">List of names of assignees to add</param>
        /// <returns></returns>
        public Task<Issue> AddAssigness(string owner, string name, int number, NewAssignees assignees)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Post<Issue>(ApiUrls.AddAssignees(owner, name, number), assignees, AcceptHeaders.MultipleIssueAssigneesPreview);
        }
    }
}
