using Octokit.Reactive.Internal;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableRepositoryInvitationsClient : IObservableRepositoryInvitationsClient
    {
        readonly IRepositoryInvitationsClient _client;
        readonly IConnection _connection;

        public ObservableRepositoryInvitationsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Repository.Invitation;
            _connection = client.Connection;
        }

        /// <summary>
        /// Accept a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#accept-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="invitationId">The id of the invitation.</param>   
        public IObservable<bool> Accept(int invitationId)
        {
            return _client.Accept(invitationId).ToObservable();
        }

        /// <summary>
        /// Decline a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#decline-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="invitationId">The id of the invitation.</param>   
        public IObservable<bool> Decline(int invitationId)
        {
            return _client.Decline(invitationId).ToObservable();
        }

        /// <summary>
        /// Deletes a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#delete-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository.</param>
        /// <param name="invitationId">The id of the invitation.</param>        
        public IObservable<bool> Delete(long repositoryId, int invitationId)
        {
            return _client.Delete(repositoryId, invitationId).ToObservable();
        }

        /// <summary>
        /// Updates a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#update-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository.</param>
        /// <param name="invitationId">The id of the invitatio.n</param>   
        /// <param name="permissions">The permission to set.</param>
        public IObservable<RepositoryInvitation> Edit(long repositoryId, int invitationId, InvitationUpdate permissions)
        {
            Ensure.ArgumentNotNull(permissions, "persmissions");

            return _client.Edit(repositoryId, invitationId, permissions).ToObservable();
        }

        /// <summary>
        /// Gets all invitations for the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-a-users-repository-invitations">API documentation</a> for more information.
        /// </remarks>        
        public IObservable<RepositoryInvitation> GetAllForCurrent()
        {
            return GetAllForCurrent(ApiOptions.None);
        }

        /// <summary>
        /// Gets all invitations for the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-a-users-repository-invitations">API documentation</a> for more information.
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>        
        public IObservable<RepositoryInvitation> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");
            return _connection.GetAndFlattenAllPages<RepositoryInvitation>(ApiUrls.UserInvitations(), null, AcceptHeaders.InvitationsApiPreview, options);
        }

        /// <summary>
        /// Gets all the invitations on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-invitations-for-a-repository">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="repositoryId">The id of the repository</param>  
        public IObservable<RepositoryInvitation> GetAllForRepository(long repositoryId)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the invitations on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-invitations-for-a-repository">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<RepositoryInvitation> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");
            return _connection.GetAndFlattenAllPages<RepositoryInvitation>(ApiUrls.RepositoryInvitations(repositoryId), null, AcceptHeaders.InvitationsApiPreview, options);
        }
    }
}
