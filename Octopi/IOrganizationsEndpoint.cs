using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octopi
{
    public interface IOrganizationsEndpoint
    {
        /// <summary>
        /// Returns the specified organization.
        /// </summary>
        /// <param name="org">The login of the specified organization,</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get"
            , Justification = "It's fine. Trust us.")]
        Task<Organization> Get(string org);

        /// <summary>
        /// Returns all the organizations for the current user.
        /// </summary>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Method makes a network request")]
        Task<IReadOnlyCollection<Organization>> GetAllForCurrent();

        /// <summary>
        /// Returns all the organizations for the specified user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<Organization>> GetAll(string user);
    }
}
