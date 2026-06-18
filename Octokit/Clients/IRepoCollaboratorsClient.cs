using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Collaborators API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/collaborators/collaborators?apiVersion=2022-11-28">Collaborators API documentation</a> for more details.
    /// </remarks>
    public interface IRepoCollaboratorsClient
    {
        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/collaborators/collaborators?apiVersion=2022-11-28#list-repository-collaborators">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<Collaborator>> GetAll(string owner, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/collaborators/collaborators?apiVersion=2022-11-28#list-repository-collaborators">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<Collaborator>> GetAll(long repositoryId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/collaborators/collaborators?apiVersion=2022-11-28#list-repository-collaborators">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<Collaborator>> GetAll(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/collaborators/collaborators?apiVersion=2022-11-28#list-repository-collaborators">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<Collaborator>> GetAll(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/collaborators/collaborators?apiVersion=2022-11-28#list-repository-collaborators">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to request and filter a list of repository collaborators</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<Collaborator>> GetAll(string owner, string name, RepositoryCollaboratorListRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/collaborators/collaborators?apiVersion=2022-11-28#list-repository-collaborators">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="request">Used to request and filter a list of repository collaborators</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<Collaborator>> GetAll(long repositoryId, RepositoryCollaboratorListRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/collaborators/collaborators?apiVersion=2022-11-28#list-repository-collaborators">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to request and filter a list of repository collaborators</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<Collaborator>> GetAll(string owner, string name, RepositoryCollaboratorListRequest request, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/collaborators/collaborators?apiVersion=2022-11-28#list-repository-collaborators">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="request">Used to request and filter a list of repository collaborators</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<IReadOnlyList<Collaborator>> GetAll(long repositoryId, RepositoryCollaboratorListRequest request, ApiOptions options, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a user is a collaborator on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#get">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="user">Username of the prospective collaborator</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<bool> IsCollaborator(string owner, string name, string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a user is a collaborator on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#get">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="user">Username of the prospective collaborator</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<bool> IsCollaborator(long repositoryId, string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Review a user's permission level in a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/collaborators/#review-a-users-permission-level">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="user">Username of the collaborator to check permission for</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<CollaboratorPermissionResponse> ReviewPermission(string owner, string name, string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Review a user's permission level in a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/collaborators/#review-a-users-permission-level">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="user">Username of the collaborator to check permission for</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<CollaboratorPermissionResponse> ReviewPermission(long repositoryId, string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new collaborator to the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#add-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="user">Username of the new collaborator</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task Add(string owner, string name, string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new collaborator to the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#add-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="user">Username of the new collaborator</param>
        /// <param name="permission">The permission to set. Only valid on organization-owned repositories.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<RepositoryInvitation> Add(string owner, string name, string user, CollaboratorRequest permission, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new collaborator to the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#add-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="user">Username of the new collaborator</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task Add(long repositoryId, string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new collaborator to the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#add-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="user">Username of the new collaborator</param>
        /// <param name="permission">The permission to set. Only valid on organization-owned repositories.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<RepositoryInvitation> Add(long repositoryId, string user, CollaboratorRequest permission, CancellationToken cancellationToken = default);

        /// <summary>
        /// Invites a new collaborator to the repo
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#add-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="user">The name of the user to invite.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<RepositoryInvitation> Invite(string owner, string name, string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Invites a new collaborator to the repo
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#add-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="user">The name of the user to invite.</param>
        /// <param name="permission">The permission to set. Only valid on organization-owned repositories.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<RepositoryInvitation> Invite(string owner, string name, string user, CollaboratorRequest permission, CancellationToken cancellationToken = default);

        /// <summary>
        /// Invites a new collaborator to the repo
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#add-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository.</param>
        /// <param name="user">The name of the user to invite.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<RepositoryInvitation> Invite(long repositoryId, string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Invites a new collaborator to the repo
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#add-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository.</param>
        /// <param name="user">The name of the user to invite.</param>
        /// <param name="permission">The permission to set. Only valid on organization-owned repositories.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task<RepositoryInvitation> Invite(long repositoryId, string user, CollaboratorRequest permission, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a collaborator from the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#remove-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="user">Username of the removed collaborator</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task Delete(string owner, string name, string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a collaborator from the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#remove-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="user">Username of the removed collaborator</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <param name="cancellationToken">An optional token to monitor for cancellation requests</param>
        Task Delete(long repositoryId, string user, CancellationToken cancellationToken = default);
    }
}
