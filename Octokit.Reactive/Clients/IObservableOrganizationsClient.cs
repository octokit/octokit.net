using System;
using System.Diagnostics.CodeAnalysis;

namespace Octokit.Reactive
{
    public interface IObservableOrganizationsClient
    {
        /// <summary>
        /// Returns a client to manage members of an organization.
        /// </summary>
        IObservableOrganizationMembersClient Member { get; }

        /// <summary>
        /// Returns a client to manage teams for an organization.
        /// </summary>
        IObservableTeamsClient Team { get; }

        /// <summary>
        /// A client for GitHub's Organization Hooks API.
        /// </summary>
        /// <remarks>See <a href="http://developer.github.com/v3/orgs/hooks/">Hooks API documentation</a> for more information.</remarks>
        IObservableOrganizationHooksClient Hook { get; }

        /// <summary>
        /// Returns a client to manage outside collaborators of an organization.
        /// </summary>
        IObservableOrganizationOutsideCollaboratorsClient OutsideCollaborator { get; }

        /// <summary>
        /// Returns a client to manage organization actions.
        /// </summary>
        IObservableOrganizationActionsClient Actions { get; }

        /// <summary>
        /// Returns the specified organization.
        /// </summary>
        /// <param name="org">The login of the specified organization,</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get"
            , Justification = "It's fine. Trust us.")]
        IObservable<Organization> Get(string org);

        /// <summary>
        /// Returns all the organizations for the current user.
        /// </summary>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Method makes a network request")]
        IObservable<Organization> GetAllForCurrent();

        /// <summary>
        /// Returns all the organizations for the current user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Method makes a network request")]
        IObservable<Organization> GetAllForCurrent(ApiOptions options);

        /// <summary>
        /// Returns all the organizations for the specified user
        /// </summary>
        /// <param name="user">The login for the user</param>
        /// <returns></returns>
        IObservable<Organization> GetAllForUser(string user);

        /// <summary>
        /// Returns all the organizations for the specified user
        /// </summary>
        /// <param name="user">The login for the user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        IObservable<Organization> GetAllForUser(string user, ApiOptions options);

        /// <summary>
        /// Returns all the organizations
        /// </summary>
        /// <returns></returns>
        IObservable<Organization> GetAll();

        /// <summary>
        /// Returns all the organizations
        /// </summary>
        /// <param name="request">Search parameters of the last organization seen</param>
        /// <returns></returns>
        IObservable<Organization> GetAll(OrganizationRequest request);

        /// <summary>
        /// Update the specified organization with data from <see cref="OrganizationUpdate"/>.
        /// </summary>
        /// <param name="org">The name of the organization to update.</param>
        /// <param name="updateRequest"></param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="Organization"/></returns>
        IObservable<Organization> Update(string org, OrganizationUpdate updateRequest);

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        IObservable<OrganizationCredential> GetAllAuthorizations(string org);

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        IObservable<OrganizationCredential> GetAllAuthorizations(string org, ApiOptions options);

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <param name="login">Limits the list of credentials authorizations for an organization to a specific login</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        IObservable<OrganizationCredential> GetAllAuthorizations(string org, string login);

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <param name="login">Limits the list of credentials authorizations for an organization to a specific login</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        IObservable<OrganizationCredential> GetAllAuthorizations(string org, string login, ApiOptions options);
    }
}
