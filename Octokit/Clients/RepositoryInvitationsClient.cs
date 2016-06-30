using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class RepositoryInvitationsClient : IRepositoryInvitationsClient
    {
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#delete-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repoId">The id ot the repository</param>
        /// <param name="invitationId">The id of the invitation</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns><see cref="Task"/></returns>
        public Task Delete(int repoId, int invitationId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all invitations for the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-a-users-repository-invitations">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="id">The id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A <see cref="IReadOnlyPagedCollection{User}"/> of <see cref="RepositoryInvitation"/>.</returns>
        public Task<IReadOnlyList<RepositoryInvitation>> GetAllForCurrent(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
        public Task<RepositoryInvitation> Update(int repoId, int invitationId)
        {
            throw new NotImplementedException();
        }
    }
}
