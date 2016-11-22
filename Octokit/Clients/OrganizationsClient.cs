using System;
using System.Threading.Tasks;
#if NET_45
using System.Collections.Generic;
#endif

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
        /// <param name="apiConnection">An API connection</param>
        public OrganizationsClient(IApiConnection apiConnection) : base(apiConnection)
        {
            Member = new OrganizationMembersClient(apiConnection);
            Team = new TeamsClient(apiConnection);
        }

        /// <summary>
        /// Returns a client to manage members of an organization.
        /// </summary>
        public IOrganizationMembersClient Member { get; private set; }

        /// <summary>
        /// Returns a client to manage teams of an organization.
        /// </summary>
        public ITeamsClient Team { get; private set; }

        /// <summary>
        /// Returns the specified <see cref="Organization"/>.
        /// </summary>
        /// <param name="org">login of the organization to get</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The specified <see cref="Organization"/>.</returns>
        public Task<Organization> Get(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            return ApiConnection.Get<Organization>(ApiUrls.Organization(org));
        }

        /// <summary>
        /// Returns all <see cref="Organization" />s for the current user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the current user's <see cref="Organization"/>s.</returns>
        public Task<IReadOnlyList<Organization>> GetAllForCurrent()
        {
            return GetAllForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// Returns all <see cref="Organization" />s for the current user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the current user's <see cref="Organization"/>s.</returns>
        public Task<IReadOnlyList<Organization>> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Organization>(ApiUrls.UserOrganizations(), options);
        }

        /// <summary>
        /// Returns all <see cref="Organization" />s for the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the specified user's <see cref="Organization"/>s.</returns>
        [Obsolete("Please use OrganizationsClient.GetAllForUser() instead. This method will be removed in a future version")]
        public Task<IReadOnlyList<Organization>> GetAll(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return GetAll(user, ApiOptions.None);
        }

        /// <summary>
        /// Returns all <see cref="Organization" />s for the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the specified user's <see cref="Organization"/>s.</returns>
        [Obsolete("Please use OrganizationsClient.GetAllForUser() instead. This method will be removed in a future version")]
        public Task<IReadOnlyList<Organization>> GetAll(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Organization>(ApiUrls.UserOrganizations(user), options);
        }

        /// <summary>
        /// Returns all <see cref="Organization" />s for the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the specified user's <see cref="Organization"/>s.</returns>
        public Task<IReadOnlyList<Organization>> GetAllForUser(string user)
        {
          Ensure.ArgumentNotNullOrEmptyString(user, "user");

          return GetAllForUser(user, ApiOptions.None);
        }

        /// <summary>
        /// Returns all <see cref="Organization" />s for the specified user.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the specified user's <see cref="Organization"/>s.</returns>
        public Task<IReadOnlyList<Organization>> GetAllForUser(string user, ApiOptions options)
        {
          Ensure.ArgumentNotNullOrEmptyString(user, "user");
          Ensure.ArgumentNotNull(options, "options");

          return ApiConnection.GetAll<Organization>(ApiUrls.UserOrganizations(user), options);
        }


        /// <summary>
        /// Returns all <see cref="Organization" />s.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of <see cref="Organization"/>s.</returns>
        public Task<IReadOnlyList<Organization>> GetAll()
        {
          return ApiConnection.GetAll<Organization>(ApiUrls.AllOrganizations());
        }

        /// <summary>
        /// Returns all <see cref="Organization" />s.
        /// </summary>
        /// <param name="request">Search parameters of the last organization seen</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of <see cref="Organization"/>s.</returns>
        public Task<IReadOnlyList<Organization>> GetAll(OrganizationRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            var url = ApiUrls.AllOrganizations(request.Since);

            return ApiConnection.GetAll<Organization>(url);
        }

        /// <summary>
        /// Update the specified organization with data from <see cref="OrganizationUpdate"/>.
        /// </summary>
        /// <param name="organizationName">The name of the organization to update.</param>
        /// <param name="updateRequest"></param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="Organization"/></returns>
        public Task<Organization> Update(string organizationName, OrganizationUpdate updateRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(organizationName, "organizationName");
            Ensure.ArgumentNotNull(updateRequest, "updateRequest");

            var updateUri = new Uri("orgs/" + organizationName, UriKind.Relative);

            return ApiConnection.Patch<Organization>(updateUri, updateRequest);
        }
    }
}
