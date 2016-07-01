﻿using System;
using System.Reactive;

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
        /// <param name="id">The id of the invitation</param>        
        IObservable<Unit> Accept(int id);

        /// <summary>
        /// Decline a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#decline-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="id">The id of the invitation</param>        
        IObservable<Unit> Decline(int id);

        /// <summary>
        /// Deletes a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#delete-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id ot the repository</param>
        /// <param name="invitationId">The id of the invitation</param>        
        IObservable<Unit> Delete(int repositoryId, int invitationId);

        /// <summary>
        /// Gets all invitations for the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-a-users-repository-invitations">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="id">The id of the invitation</param>        
        IObservable<RepositoryInvitation> GetAllForCurrent(int id);

        /// <summary>
        /// Gets all the invitations on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-invitations-for-a-repository">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="id">The id of the repository</param>        
        IObservable<RepositoryInvitation> GetAllForRepository(int id);

        /// <summary>
        /// Updates a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#update-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id ot the repository</param>
        /// <param name="invitationId">The id of the invitation</param>   
        /// <param name="permissions">The permission for the collsborator</param>
        IObservable<RepositoryInvitation> Edit(int repositoryId, int invitationId, InvitationUpdate permissions);
    }
}
