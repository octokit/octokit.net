using System.Threading.Tasks;

namespace Octokit
{
    public interface IStatisticsClient
    {
        /// <summary>
        /// Returns a <see cref="User"/> for the current authenticated user.
        /// </summary>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="User"/></returns>
        Task<Contributor> Contributors();
    }
}