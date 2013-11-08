#if NET_45
using System.Collections.Generic;
#endif
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Orgs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/orgs/">Orgs API documentation</a> for more information.
    /// </remarks>
    public interface IOrganizationsClient
    {
        /// <summary>
        /// Returns a client to manage members of an organization.
        /// </summary>
        IOrganizationMembersClient Member { get; }

        /// <summary>
        /// Returns a client to manage teams of an organization.
        /// </summary>
        ITeamsClient Team { get; }

        /// <summary>
        /// Returns the specified <see cref="Organization"/>.
        /// </summary>
        /// <param name="org">login of the organization to get</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The specified <see cref="Organization"/>.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get"
            , Justification = "It's fine. Trust us.")]
        Task<Organization> Get(string org);

        /// <summary>
        /// Returns all <see cref="Organization" />s for the current user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the current user's <see cref="Organization"/>s.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Method makes a network request")]
        Task<IReadOnlyList<Organization>> GetAllForCurrent();

        /// <summary>
        /// Returns all <see cref="Organization" />s for the specified user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the specified user's <see cref="Organization"/>s.</returns>
        Task<IReadOnlyList<Organization>> GetAll(string user);
    }
}
