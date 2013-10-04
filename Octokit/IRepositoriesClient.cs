using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IRepositoriesClient
    {
        /// <summary>
        /// Retrieves the <see cref="Repository"/> for the specified owner and name.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <returns>A <see cref="Repository"/></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        Task<Repository> Get(string owner, string name);
        
        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the current user.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Makes a network request")]
        Task<IReadOnlyList<Repository>> GetAllForCurrent();
        
        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the specified user.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Makes a network request")]
        Task<IReadOnlyList<Repository>> GetAllForUser(string login);

        /// <summary>
        /// Retrieves every <see cref="Repository"/> that belongs to the specified organization.
        /// </summary>
        /// <remarks>
        /// The default page size on GitHub.com is 30.
        /// </remarks>
        /// <returns>A <see cref="IReadOnlyPagedCollection{Repository}"/> of <see cref="Repository"/>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Makes a network request")]
        Task<IReadOnlyList<Repository>> GetAllForOrg(string organization);

        /// <summary>
        /// Returns the HTML rendered README.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <returns></returns>
        Task<Readme> GetReadme(string owner, string name);
    }
}
