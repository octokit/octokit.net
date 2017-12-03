using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace Octokit.Reactive
{
    public interface IObservableRepositoryInvitationsClient
    {
        /// <summary>
        /// Accept a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#accept-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="invitationId">The id of the invitation.</param>        
        IObservable<bool> Accept(int invitationId);

        /// <summary>
        /// Decline a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#decline-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="invitationId">The id of the invitation.</param>        
        IObservable<bool> Decline(int invitationId);

        /// <summary>
        /// Deletes a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#delete-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository.</param>
        /// <param name="invitationId">The id of the invitation.</param>        
        IObservable<bool> Delete(long repositoryId, int invitationId);

        /// <summary>
        /// Gets all invitations for the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-a-users-repository-invitations">API documentation</a> for more information.
        /// </remarks>        
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<RepositoryInvitation> GetAllForCurrent();

        /// <summary>
        /// Gets all invitations for the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-a-users-repository-invitations">API documentation</a> for more information.
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>        
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<RepositoryInvitation> GetAllForCurrent(ApiOptions options);

        /// <summary>
        /// Gets all the invitations on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-invitations-for-a-repository">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="repositoryId">The id of the repository</param>         
        IObservable<RepositoryInvitation> GetAllForRepository(long repositoryId);

        /// <summary>
        /// Gets all the invitations on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-invitations-for-a-repository">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="repositoryId">The id of the repository</param>
        /// /// <param name="options">Options for changing the API response</param>        
        IObservable<RepositoryInvitation> GetAllForRepository(long repositoryId, ApiOptions options);

        /// <summary>
        /// Updates a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#update-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository.</param>
        /// <param name="invitationId">The id of the invitation.</param>   
        /// <param name="permissions">The permission to set.</param>
        /// <returns><see cref="RepositoryInvitation"/></returns>
        IObservable<RepositoryInvitation> Edit(long repositoryId, int invitationId, InvitationUpdate permissions);
    }
}
