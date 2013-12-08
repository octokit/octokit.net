using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Activity Events API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/activity/events/">Activity Events API documentation</a> for more information
    /// </remarks>
    public interface IEventsClient
    {
        /// <summary>
        /// Gets all the public events
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events
        /// </remarks>
        /// <returns>All the public <see cref="Activity"/>s for the particular user.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<Activity>> GetAll();

        /// <summary>
        /// Gets all the events for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-issue-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All the <see cref="Activity"/>s for the particular repository.</returns>
        Task<IReadOnlyList<Activity>> GetAllForRepository(string owner, string name);

        /// <summary>
        /// Gets all the events for a given repository network
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-for-a-network-of-repositories
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All the <see cref="Activity"/>s for the particular repository network.</returns>
        Task<IReadOnlyList<Activity>> GetAllForRepositoryNetwork(string owner, string name);

        /// <summary>
        /// Gets all the events for a given organization
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-for-an-organization
        /// </remarks>
        /// <param name="organization">The name of the organization</param>
        /// <returns>All the <see cref="Activity"/>s for the particular organization.</returns>
        Task<IReadOnlyList<Activity>> GetAllForOrganization(string organization);

        /// <summary>
        /// Gets all the events that have been received by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-that-a-user-has-received
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <returns>All the <see cref="Activity"/>s that a particular user has received.</returns>
        Task<IReadOnlyList<Activity>> GetUserReceived(string user);

        /// <summary>
        /// Gets all the events that have been received by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-that-a-user-has-received
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <returns>All the <see cref="Activity"/>s that a particular user has received.</returns>
        Task<IReadOnlyList<Activity>> GetUserReceivedPublic(string user);

        /// <summary>
        /// Gets all the events that have been performed by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-performed-by-a-user
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <returns>All the <see cref="Activity"/>s that a particular user has performed.</returns>
        Task<IReadOnlyList<Activity>> GetUserPerformed(string user);

        /// <summary>
        /// Gets all the public events that have been performed by a given user.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events-performed-by-a-user
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <returns>All the public <see cref="Activity"/>s that a particular user has performed.</returns>
        Task<IReadOnlyList<Activity>> GetUserPerformedPublic(string user);

        /// <summary>
        /// Gets all the events that are associated with an organization.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-events-for-an-organization
        /// </remarks>
        /// <param name="user">The login of the user</param>
        /// <param name="organization">The name of the organization</param>
        /// <returns>All the public <see cref="Activity"/>s that are associated with an organization.</returns>
        Task<IReadOnlyList<Activity>> GetForAnOrganization(string user, string organization);
    }
}