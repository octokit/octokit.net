using Octokit.Reactive.Internal;
using System;
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
        /// <param name="id">The id of the invitation</param>   
        public IObservable<Unit> Accept(int id)
        {
            return _client.Accept(id).ToObservable();
        }

        /// <summary>
        /// Decline a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#decline-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="id">The id of the invitation</param>   
        public IObservable<Unit> Decline(int id)
        {
            return _client.Decline(id).ToObservable();
        }

        /// <summary>
        /// Deletes a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#delete-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id ot the repository</param>
        /// <param name="invitationId">The id of the invitation</param>        
        public IObservable<Unit> Delete(int repositoryId, int invitationId)
        {
            return _client.Delete(repositoryId, invitationId).ToObservable();
        }

        /// <summary>
        /// Updates a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#update-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id ot the repository</param>
        /// <param name="invitationId">The id of the invitation</param>   
        /// <param name="permissions">The permission for the collsborator</param>
        public IObservable<RepositoryInvitation> Edit(int repositoryId, int invitationId, InvitationUpdate permissions)
        {
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
            return _connection.GetAndFlattenAllPages<RepositoryInvitation>(ApiUrls.UserInvitations(), null, AcceptHeaders.InvitationsApiPreview, null);
        }

        /// <summary>
        /// Gets all the invitations on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-invitations-for-a-repository">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="id">The id of the repository</param>  
        public IObservable<RepositoryInvitation> GetAllForRepository(int id)
        {
            return _connection.GetAndFlattenAllPages<RepositoryInvitation>(ApiUrls.RepositoryInvitations(id), null, AcceptHeaders.InvitationsApiPreview, null);
        }
    }
}
