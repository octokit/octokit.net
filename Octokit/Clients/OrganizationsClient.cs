using System;
#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Orgs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/orgs/">Orgs API documentation</a> for more information.
    /// </remarks>
    public class OrganizationsClient : ApiClient, IOrganizationsClient
    {
        /// <summary>
        /// Initializes a new GitHub Orgs API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public OrganizationsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Returns the specified <see cref="Organization"/>.
        /// </summary>
        /// <param name="org">login of the organization to get.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The specified <see cref="Organization"/>.</returns>
        public async Task<Organization> Get(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            var endpoint = "/orgs/{0}".FormatUri(org);
            return await ApiConnection.Get<Organization>(endpoint);
        }

        /// <summary>
        /// Returns all <see cref="Organization" />s for the current user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the current user's <see cref="Organization"/>s.</returns>
        public async Task<IReadOnlyList<Organization>> GetAllForCurrent()
        {
            return await ApiConnection.GetAll<Organization>(ApiUrls.Organizations());
        }

        /// <summary>
        /// Returns all <see cref="Organization" />s for the specified user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the specified user's <see cref="Organization"/>s.</returns>
        public async Task<IReadOnlyList<Organization>> GetAll(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return await ApiConnection.GetAll<Organization>(ApiUrls.Organizations(user));
        }
    }
}
