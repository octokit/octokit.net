using System;
using System.Collections.Generic;
using System.Net;
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
        /// <param name="invitationId">The id of the invitation</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>        
        public async Task<bool> Accept(int invitationId)
        {
            var endpoint = ApiUrls.UserInvitations(invitationId);

            try
            {
                var httpStatusCode = await Connection.Patch(endpoint, AcceptHeaders.InvitationsApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Decline a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#decline-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="invitationId">The id of the invitation</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public async Task<bool> Decline(int invitationId)
        {
            var endpoint = ApiUrls.UserInvitations(invitationId);

            try
            {
                var httpStatusCode = await Connection.Delete(endpoint, new object(), AcceptHeaders.InvitationsApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#delete-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="invitationId">The id of the invitation</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public async Task<bool> Delete(long repositoryId, int invitationId)
        {
            var endpoint = ApiUrls.RepositoryInvitations(repositoryId, invitationId);

            try
            {
                var httpStatusCode = await Connection.Delete(endpoint, new object(), AcceptHeaders.InvitationsApiPreview).ConfigureAwait(false);
                return httpStatusCode == HttpStatusCode.NoContent;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all invitations for the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-a-users-repository-invitations">API documentation</a> for more information.
        /// </remarks>        
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public Task<IReadOnlyList<RepositoryInvitation>> GetAllForCurrent()
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
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public Task<IReadOnlyList<RepositoryInvitation>> GetAllForCurrent(ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");
            return ApiConnection.GetAll<RepositoryInvitation>(ApiUrls.UserInvitations(), null, AcceptHeaders.InvitationsApiPreview, options);
        }

        /// <summary>
        /// Gets all the invitations on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-invitations-for-a-repository">API documentation</a> for more information.
        /// </remarks>        
        /// <param name="repositoryId">The id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public Task<IReadOnlyList<RepositoryInvitation>> GetAllForRepository(long repositoryId)
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
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public Task<IReadOnlyList<RepositoryInvitation>> GetAllForRepository(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");
            return ApiConnection.GetAll<RepositoryInvitation>(ApiUrls.RepositoryInvitations(repositoryId), null, AcceptHeaders.InvitationsApiPreview, options);
        }

        /// <summary>
        /// Updates a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#update-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="invitationId">The id of the invitation</param>
        /// <param name="permissions">The permission for the collsborator</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public Task<RepositoryInvitation> Edit(long repositoryId, int invitationId, InvitationUpdate permissions)
        {
            Ensure.ArgumentNotNull(permissions, "permissions");

            return ApiConnection.Patch<RepositoryInvitation>(ApiUrls.RepositoryInvitations(repositoryId, invitationId), permissions, AcceptHeaders.InvitationsApiPreview);
        }
    }
}
