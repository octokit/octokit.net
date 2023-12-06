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
        /// Adds a license for a user
        /// </summary>
        /// <param name="organization">The organization name</param>
        /// <param name="userName">The github users profile name to add a license to</param>
        /// <returns>A <see cref="CopilotSeatAllocation"/> instance with results</returns>
        Task<CopilotSeatAllocation> Add(string organization, string userName);

        /// <summary>
        /// Adds a license for one or many users
        /// </summary>
        /// <param name="organization">The organization name</param>
        /// <param name="userSeatAllocation">A <see cref="UserSeatAllocation"/> instance, containing the names of the user(s) to add licenses to</param>
        /// <returns>A <see cref="CopilotSeatAllocation"/> instance with results</returns>
        Task<CopilotSeatAllocation> Add(string organization, UserSeatAllocation userSeatAllocation);

        /// <summary>
        /// Gets all of the currently allocated licenses for an organization
        /// </summary>
        /// <param name="organization">The organization</param>
        /// <returns>A <see cref="CopilotSeats"/> instance containing the currently allocated user licenses</returns>
        Task<IReadOnlyList<CopilotSeats>> GetAll(string organization, CopilotSeatsRequest request, CopilotApiOptions copilotApiOptions);
    }
}
