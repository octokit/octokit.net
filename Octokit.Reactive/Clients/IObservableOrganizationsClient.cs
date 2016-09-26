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
        [Obsolete("Please use IObservableOrganizationsClient.GetAllForUser() instead. This method will be removed in a future version")]
        IObservable<Organization> GetAll(string user);

        /// <summary>
        /// Returns all the organizations for the specified user
        /// </summary>
        /// <param name="user">The login for the user</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        [Obsolete("Please use IObservableOrganizationsClient.GetAllForUser() instead. This method will be removed in a future version")]  
        IObservable<Organization> GetAll(string user, ApiOptions options);

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
        /// <param name="organizationName">The name of the organization to update.</param>
        /// <param name="updateRequest"></param>
        /// <exception cref="AuthorizationException">Thrown if the client is not authenticated.</exception>
        /// <returns>A <see cref="Organization"/></returns>
        IObservable<Organization> Update(string organizationName, OrganizationUpdate updateRequest);
    }
}
