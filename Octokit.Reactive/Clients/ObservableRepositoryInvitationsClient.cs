using Octokit.Reactive.Internal;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Threading;

namespace Octokit.Reactive
{
    public class ObservableRepositoryInvitationsClient : IObservableRepositoryInvitationsClient
    {
        readonly IRepositoryInvitationsClient _client;
        readonly IConnection _connection;

        public ObservableRepositoryInvitationsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

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
        public IObservable<bool> Accept(long invitationId, CancellationToken cancellationToken = default)
        {
            return _client.Accept(invitationId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Decline a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#decline-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="invitationId">The id of the invitation.</param>
        public IObservable<bool> Decline(long invitationId, CancellationToken cancellationToken = default)
        {
            return _client.Decline(invitationId, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Deletes a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#delete-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository.</param>
        /// <param name="invitationId">The id of the invitation.</param>
        public IObservable<bool> Delete(long repositoryId, long invitationId, CancellationToken cancellationToken = default)
        {
            return _client.Delete(repositoryId, invitationId, cancellationToken).ToObservable();
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
        public IObservable<RepositoryInvitation> Edit(long repositoryId, long invitationId, InvitationUpdate permissions, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(permissions, nameof(permissions));

            return _client.Edit(repositoryId, invitationId, permissions, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets all invitations for the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-a-users-repository-invitations">API documentation</a> for more information.
        /// </remarks>
        public IObservable<RepositoryInvitation> GetAllForCurrent(CancellationToken cancellationToken = default)
        {
            return GetAllForCurrent(ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all invitations for the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-a-users-repository-invitations">API documentation</a> for more information.
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<RepositoryInvitation> GetAllForCurrent(ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));
            return _connection.GetAndFlattenAllPages<RepositoryInvitation>(ApiUrls.UserInvitations(), null, options, cancellationToken);
        }

        /// <summary>
        /// Gets all the invitations on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-invitations-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        public IObservable<RepositoryInvitation> GetAllForRepository(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None, cancellationToken);
        }

        /// <summary>
        /// Gets all the invitations on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-invitations-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        public IObservable<RepositoryInvitation> GetAllForRepository(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));
            return _connection.GetAndFlattenAllPages<RepositoryInvitation>(ApiUrls.RepositoryInvitations(repositoryId), null, options, cancellationToken);
        }
    }
}
