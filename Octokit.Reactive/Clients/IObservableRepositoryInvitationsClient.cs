using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Threading;

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
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<bool> Accept(long invitationId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Decline a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#decline-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="invitationId">The id of the invitation.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<bool> Decline(long invitationId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#delete-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository.</param>
        /// <param name="invitationId">The id of the invitation.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<bool> Delete(long repositoryId, long invitationId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all invitations for the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-a-users-repository-invitations">API documentation</a> for more information.
        /// </remarks>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<RepositoryInvitation> GetAllForCurrent(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all invitations for the current user.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-a-users-repository-invitations">API documentation</a> for more information.
        /// </remarks>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        IObservable<RepositoryInvitation> GetAllForCurrent(ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all the invitations on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-invitations-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<RepositoryInvitation> GetAllForRepository(long repositoryId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all the invitations on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#list-invitations-for-a-repository">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        IObservable<RepositoryInvitation> GetAllForRepository(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a repository invitation.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/invitations/#update-a-repository-invitation">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository.</param>
        /// <param name="invitationId">The id of the invitation.</param>
        /// <param name="permissions">The permission to set.</param>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        /// <returns><see cref="RepositoryInvitation"/></returns>
        IObservable<RepositoryInvitation> Edit(long repositoryId, long invitationId, InvitationUpdate permissions, CancellationToken cancellationToken = default);
    }
}
