using System;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            Hook = new OrganizationHooksClient(apiConnection);
            OutsideCollaborator = new OrganizationOutsideCollaboratorsClient(apiConnection);
            Actions = new OrganizationActionsClient(apiConnection);
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
        /// Returns a client to manage organization actions.
        /// </summary>
        public IOrganizationActionsClient Actions { get; private set; }

        /// <summary>
        /// Returns a client to manage outside collaborators of an organization.
        /// </summary>
        public IOrganizationOutsideCollaboratorsClient OutsideCollaborator { get; private set; }

        /// <summary>
        /// Returns the specified <see cref="Organization"/>.
        /// </summary>
        /// <param name="org">login of the organization to get</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>The specified <see cref="Organization"/>.</returns>
        [ManualRoute("GET", "/orgs/{org}")]
        public Task<Organization> Get(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            return ApiConnection.Get<Organization>(ApiUrls.Organization(org));
        }

        /// <summary>
        /// A client for GitHub's Organization Hooks API.
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/">Hooks API documentation</a> for more information.</remarks>
        public IOrganizationHooksClient Hook { get; private set; }

        /// <summary>
        /// Returns all <see cref="Organization" />s for the current user.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the current user's <see cref="Organization"/>s.</returns>
        [ManualRoute("GET", "/user/orgs")]
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
        [ManualRoute("GET", "/user/orgs")]
        public Task<IReadOnlyList<Organization>> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Organization>(ApiUrls.UserOrganizations(), options);
        }

        /// <summary>
        /// Returns <see cref="Organization" />s which the specified user is a member of,
        /// where the user hasn't marked their membership as private.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>
        /// A list of the <see cref="Organization"/>s which the specified user is a member
        /// of, where they haven't marked their membership as private
        /// </returns>
        [ManualRoute("GET", "/users/{username}/orgs")]
        public Task<IReadOnlyList<Organization>> GetAllForUser(string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return GetAllForUser(user, ApiOptions.None);
        }

        /// <summary>
        /// Returns <see cref="Organization" />s which the specified user is a member of,
        /// where the user hasn't marked their membership as private.
        /// </summary>
        /// <param name="user">The login of the user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>
        /// A list of the <see cref="Organization"/>s which the specified user is a member
        /// of, where they haven't marked their membership as private
        /// </returns>
        [ManualRoute("GET", "/users/{username}/orgs")]
        public Task<IReadOnlyList<Organization>> GetAllForUser(string user, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Organization>(ApiUrls.UserOrganizations(user), options);
        }


        /// <summary>
        /// Returns all <see cref="Organization" />s.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of <see cref="Organization"/>s.</returns>
        [ManualRoute("GET", "/organizations")]
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
        [ManualRoute("GET", "/organizations")]
        public Task<IReadOnlyList<Organization>> GetAll(OrganizationRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            var url = ApiUrls.AllOrganizations(request.Since);

            return ApiConnection.GetAll<Organization>(url);
        }

        /// <summary>
        /// Update the specified organization with data from <see cref="OrganizationUpdate"/>.
        /// </summary>
        /// <param name="org">The name of the organization to update.</param>
        /// <param name="updateRequest"></param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="Organization"/></returns>
        [ManualRoute("PATCH", "/orgs/{org}")]
        public Task<Organization> Update(string org, OrganizationUpdate updateRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(updateRequest, nameof(updateRequest));

            var updateUri = new Uri("orgs/" + org, UriKind.Relative);

            return ApiConnection.Patch<Organization>(updateUri, updateRequest);
        }

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        [ManualRoute("GET", "/orgs/{org}/credential-authorizations")]
        public Task<IReadOnlyList<OrganizationCredential>> GetAllAuthorizations(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));

            var url = ApiUrls.AllOrganizationCredentials(org);

            return ApiConnection.GetAll<OrganizationCredential>(url);
        }

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        [ManualRoute("GET", "/orgs/{org}/credential-authorizations")]
        public Task<IReadOnlyList<OrganizationCredential>> GetAllAuthorizations(string org, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNull(options, nameof(options));

            var url = ApiUrls.AllOrganizationCredentials(org);

            return ApiConnection.GetAll<OrganizationCredential>(url, options);
        }

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <param name="login">Limits the list of credentials authorizations for an organization to a specific login</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        [ManualRoute("GET", "/orgs/{org}/credential-authorizations")]
        public Task<IReadOnlyList<OrganizationCredential>> GetAllAuthorizations(string org, string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));

            var url = ApiUrls.AllOrganizationCredentials(org, login);

            return ApiConnection.GetAll<OrganizationCredential>(url);
        }

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <param name="login">Limits the list of credentials authorizations for an organization to a specific login</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        [ManualRoute("GET", "/orgs/{org}/credential-authorizations")]
        public Task<IReadOnlyList<OrganizationCredential>> GetAllAuthorizations(string org, string login, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            Ensure.ArgumentNotNullOrEmptyString(login, nameof(login));
            Ensure.ArgumentNotNull(options, nameof(options));

            var url = ApiUrls.AllOrganizationCredentials(org, login);

            return ApiConnection.GetAll<OrganizationCredential>(url, options);
        }
    }
}
