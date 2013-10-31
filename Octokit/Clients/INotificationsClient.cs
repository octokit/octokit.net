using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    public interface INotificationsClient
    {
        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Notification}"/> of <see cref="Notification"/>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Task<IReadOnlyList<Notification>> GetAllForCurrent();

        /// <summary>
        /// Retrieves all of the <see cref="Notification"/>s for the current user specific to the specified repository.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Notification}"/> of <see cref="Notification"/>.</returns>
        Task<IReadOnlyList<Notification>> GetAllForRepository(string owner, string name);
    }
}
