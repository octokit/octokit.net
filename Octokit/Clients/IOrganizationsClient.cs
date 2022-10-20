using System.Threading.Tasks;
using System.Collections.Generic;

using System.Diagnostics.CodeAnalysis;
using System;

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
        /// A client for GitHub's Organization Hooks API.
        /// </summary>
        /// <remarks>See <a href="https://developer.github.com/v3/orgs/hooks/">Hooks API documentation</a> for more information.</remarks>
        IOrganizationHooksClient Hook { get; }

        /// <summary>
        /// Returns a client to manage outside collaborators of an organization.
        /// </summary>
        IOrganizationOutsideCollaboratorsClient OutsideCollaborator { get; }

        /// <summary>
        /// Returns a client to manage organization actions.
        /// </summary>
        IOrganizationActionsClient Actions { get; }

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
        /// Returns all <see cref="Organization" />s for the current user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the current user's <see cref="Organization"/>s.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Method makes a network request")]
        Task<IReadOnlyList<Organization>> GetAllForCurrent(ApiOptions options);

        /// <summary>
        /// Returns <see cref="Organization" />s which the specified user is a member of,
        /// where the user hasn't marked their membership as private.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>
        /// A list of the <see cref="Organization"/>s which the specified user is a member
        /// of, where they haven't marked their membership as private
        /// </returns>
        Task<IReadOnlyList<Organization>> GetAllForUser(string user);

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
        Task<IReadOnlyList<Organization>> GetAllForUser(string user, ApiOptions options);

        /// <summary>
        /// Returns all <see cref="Organization" />s.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of <see cref="Organization"/>s.</returns>
        [ExcludeFromPaginationApiOptionsConventionTest("This API call uses the OrganizationRequest.Since parameter for pagination")]
        Task<IReadOnlyList<Organization>> GetAll();

        /// <summary>
        /// Returns all <see cref="Organization" />s.
        /// </summary>
        /// <param name="request">Search parameters of the last organization seen</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of <see cref="Organization"/>s.</returns>
        [ExcludeFromPaginationApiOptionsConventionTest("This API call uses the OrganizationRequest.Since parameter for pagination")]
        Task<IReadOnlyList<Organization>> GetAll(OrganizationRequest request);

        /// <summary>
        /// Update the specified organization with data from <see cref="OrganizationUpdate"/>.
        /// </summary>
        /// <param name="org">The name of the organization to update.</param>
        /// <param name="updateRequest"></param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="Organization"/></returns>
        Task<Organization> Update(string org, OrganizationUpdate updateRequest);

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        Task<IReadOnlyList<OrganizationCredential>> GetAllAuthorizations(string org);

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        Task<IReadOnlyList<OrganizationCredential>> GetAllAuthorizations(string org, ApiOptions options);

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <param name="login">Limits the list of credentials authorizations for an organization to a specific login</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        Task<IReadOnlyList<OrganizationCredential>> GetAllAuthorizations(string org, string login);

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <param name="login">Limits the list of credentials authorizations for an organization to a specific login</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        Task<IReadOnlyList<OrganizationCredential>> GetAllAuthorizations(string org, string login, ApiOptions options);
    }
}
