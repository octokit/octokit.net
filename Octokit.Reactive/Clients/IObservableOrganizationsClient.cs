using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

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
        /// Returns a client to manage organization custom properties.
        /// </summary>
        IObservableOrganizationCustomPropertiesClient CustomProperty { get; }

        /// <summary>
        /// Returns the specified organization.
        /// </summary>
        /// <param name="org">The login of the specified organization,</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get"
            , Justification = "It's fine. Trust us.")]
        IObservable<Organization> Get(string org, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all the organizations for the current user.
        /// </summary>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Method makes a network request")]
        IObservable<Organization> GetAllForCurrent(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all the organizations for the current user.
        /// </summary>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
            Justification = "Method makes a network request")]
        IObservable<Organization> GetAllForCurrent(ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all the organizations for the specified user
        /// </summary>
        /// <param name="user">The login for the user</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns></returns>
        IObservable<Organization> GetAllForUser(string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all the organizations for the specified user
        /// </summary>
        /// <param name="user">The login for the user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns></returns>
        IObservable<Organization> GetAllForUser(string user, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all the organizations
        /// </summary>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns></returns>
        IObservable<Organization> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all the organizations
        /// </summary>
        /// <param name="request">Search parameters of the last organization seen</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns></returns>
        IObservable<Organization> GetAll(OrganizationRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update the specified organization with data from <see cref="OrganizationUpdate"/>.
        /// </summary>
        /// <param name="org">The name of the organization to update.</param>
        /// <param name="updateRequest"></param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="Organization"/></returns>
        IObservable<Organization> Update(string org, OrganizationUpdate updateRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        IObservable<OrganizationCredential> GetAllAuthorizations(string org, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        IObservable<OrganizationCredential> GetAllAuthorizations(string org, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <param name="login">Limits the list of credentials authorizations for an organization to a specific login</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        IObservable<OrganizationCredential> GetAllAuthorizations(string org, string login, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns all <see cref="OrganizationCredential" />s.
        /// </summary>
        /// <param name="org">The organization name.</param>
        /// <param name="login">Limits the list of credentials authorizations for an organization to a specific login</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns>A list of <see cref="OrganizationCredential"/>s.</returns>
        IObservable<OrganizationCredential> GetAllAuthorizations(string org, string login, ApiOptions options, CancellationToken cancellationToken = default);
    }
}
