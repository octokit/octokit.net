using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class RepositoryInvitationsClient : ApiClient, IRepositoryInvitationsClient
    {
        public RepositoryInvitationsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Accept a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#accept-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="id">The id of the invitation</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>        
        /// <returns><see cref="Task"/></returns>
        public Task Accept(int id)
        {
            return ApiConnection.Patch(ApiUrls.UserInvitations(id), AcceptHeaders.InvitationsApiPreview);
        }

        /// <summary>
        /// Decline a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#decline-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="id">The id of the invitation</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>        
        /// <returns><see cref="Task"/></returns>
        public Task Decline(int id)
        {
            return ApiConnection.Delete(ApiUrls.UserInvitations(id), null, AcceptHeaders.InvitationsApiPreview);
        }

        /// <summary>
        /// Deletes a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#delete-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id ot the repository</param>
        /// <param name="invitationId">The id of the invitation</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns><see cref="Task"/></returns>
        public Task Delete(int repositoryId, int invitationId)
        {
            return ApiConnection.Delete(ApiUrls.RepositoryInvitations(repositoryId, invitationId), null, AcceptHeaders.InvitationsApiPreview);
        }

        /// <summary>
        /// Gets all invitations for the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-a-users-repository-invitations">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="id">The id of the invitation</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{User}"/> of <see cref="RepositoryInvitation"/>.</returns>
        public Task<IReadOnlyList<RepositoryInvitation>> GetAllForCurrent(int id)
        {
            return ApiConnection.GetAll<RepositoryInvitation>(ApiUrls.UserInvitations(id), null, AcceptHeaders.InvitationsApiPreview);
        }

        /// <summary>
        /// Gets all the invitations on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-invitations-for-a-repository">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="id">The id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{User}"/> of <see cref="RepositoryInvitation"/>.</returns>
        public Task<IReadOnlyList<RepositoryInvitation>> GetAllForRepository(int id)
        {
            return ApiConnection.GetAll<RepositoryInvitation>(ApiUrls.RepositoryInvitations(id), null, AcceptHeaders.InvitationsApiPreview);
        }

        /// <summary>
        /// Updates a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#update-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoId">The id ot the repository</param>
        /// <param name="invitationId">The id of the invitation</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns><see cref="Task"/></returns>
        public Task<RepositoryInvitation> Edit(int repositoryId, int invitationId, InvitationUpdate permissions)
        {
            return ApiConnection.Patch<RepositoryInvitation>(ApiUrls.RepositoryInvitations(repositoryId, invitationId), permissions, AcceptHeaders.InvitationsApiPreview);
        }
    }
}
