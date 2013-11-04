using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit.Clients
{
    interface IStarredClient
    {
        /// <summary>
        /// Retrieves all of the starred <see cref="Repository"/>(ies) for the authenticated user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        Task<IReadOnlyList<Repository>> GetAllForCurrent();

        /// <summary>
        /// Retrieves all of the <see cref="Repository"/>(ies) starred by the passed user.
        /// </summary>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Star"/>.</returns>
        Task<IReadOnlyList<Repository>> GetAllForUser(string user);
    }
}
