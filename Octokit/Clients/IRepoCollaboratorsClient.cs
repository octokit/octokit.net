using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Collaborators on a Repository.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/repos/collaborators/">Collaborators API documentation</a> for more details.
    /// </remarks>
    public interface IRepoCollaboratorsClient
    {
        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#list">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<IReadOnlyList<User>> GetAll(string owner, string name);

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#list">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<IReadOnlyList<User>> GetAll(long repositoryId);

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#list">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<IReadOnlyList<User>> GetAll(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#list">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<IReadOnlyList<User>> GetAll(long repositoryId, ApiOptions options);

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#list">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to request and filter a list of repository collaborators</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<IReadOnlyList<User>> GetAll(string owner, string name, RepositoryCollaboratorListRequest request);

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#list">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="request">Used to request and filter a list of repository collaborators</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<IReadOnlyList<User>> GetAll(long repositoryId, RepositoryCollaboratorListRequest request);

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#list">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="request">Used to request and filter a list of repository collaborators</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<IReadOnlyList<User>> GetAll(string owner, string name, RepositoryCollaboratorListRequest request, ApiOptions options);

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#list">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="request">Used to request and filter a list of repository collaborators</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<IReadOnlyList<User>> GetAll(long repositoryId, RepositoryCollaboratorListRequest request, ApiOptions options);

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
        Task<bool> IsCollaborator(string owner, string name, string user);

        /// <summary>
        /// Checks if a user is a collaborator on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#get">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="user">Username of the prospective collaborator</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<bool> IsCollaborator(long repositoryId, string user);

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
        Task<CollaboratorPermission> ReviewPermission(string owner, string name, string user);

        /// <summary>
        /// Review a user's permission level in a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/collaborators/#review-a-users-permission-level">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="user">Username of the collaborator to check permission for</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<CollaboratorPermission> ReviewPermission(long repositoryId, string user);

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
        Task Add(string owner, string name, string user);

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
        Task<RepositoryInvitation> Add(string owner, string name, string user, CollaboratorRequest permission);

        /// <summary>
        /// Adds a new collaborator to the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#add-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="user">Username of the new collaborator</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task Add(long repositoryId, string user);

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
        Task<RepositoryInvitation> Add(long repositoryId, string user, CollaboratorRequest permission);

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
        Task<RepositoryInvitation> Invite(string owner, string name, string user);

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
        Task<RepositoryInvitation> Invite(string owner, string name, string user, CollaboratorRequest permission);

        /// <summary>
        /// Invites a new collaborator to the repo
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#add-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository.</param>
        /// <param name="user">The name of the user to invite.</param>        
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<RepositoryInvitation> Invite(long repositoryId, string user);

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
        Task<RepositoryInvitation> Invite(long repositoryId, string user, CollaboratorRequest permission);

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
        Task Delete(string owner, string name, string user);

        /// <summary>
        /// Deletes a collaborator from the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#remove-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="user">Username of the removed collaborator</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task Delete(long repositoryId, string user);
    }
}