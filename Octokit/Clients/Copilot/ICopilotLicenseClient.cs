using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for managing licenses for GitHub Copilot for Business
    /// </summary>
    public interface ICopilotLicenseClient
    {
        /// <summary>
        /// Removes a license for a user
        /// </summary>
        /// <param name="organization">The organization name</param>
        /// <param name="userName">The github users profile name to remove a license from</param>
        /// <returns>A <see cref="CopilotSeatAllocation"/> instance with results</returns>
        Task<CopilotSeatAllocation> Remove(string organization, string userName);

        /// <summary>
        /// Removes a license for one or many users
        /// </summary>
        /// <param name="organization">The organization name</param>
        /// <param name="userSeatAllocation">A <see cref="UserSeatAllocation"/> instance, containing the names of the user(s) to remove licenses for</param>
        /// <returns>A <see cref="CopilotSeatAllocation"/> instance with results</returns>
        Task<CopilotSeatAllocation> Remove(string organization, UserSeatAllocation userSeatAllocation);

        /// <summary>
        /// Assigns a license to a user
        /// </summary>
        /// <param name="organization">The organization name</param>
        /// <param name="userName">The github users profile name to add a license to</param>
        /// <returns>A <see cref="CopilotSeatAllocation"/> instance with results</returns>
        Task<CopilotSeatAllocation> Assign(string organization, string userName);

        /// <summary>
        /// Assigns a license for one or many users
        /// </summary>
        /// <param name="organization">The organization name</param>
        /// <param name="userSeatAllocation">A <see cref="UserSeatAllocation"/> instance, containing the names of the user(s) to add licenses to</param>
        /// <returns>A <see cref="CopilotSeatAllocation"/> instance with results</returns>
        Task<CopilotSeatAllocation> Assign(string organization, UserSeatAllocation userSeatAllocation);

        /// <summary>
        /// Gets all of the currently allocated licenses for an organization
        /// </summary>
        /// <param name="organization">The organization</param>
        /// <param name="options">The api options to use when making the API call, such as paging</param>
        /// <returns>A <see cref="CopilotSeats"/> instance containing the currently allocated user licenses</returns>
        Task<IReadOnlyList<CopilotSeats>> GetAll(string organization, ApiOptions options);
    }
}
