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
    public class RepoCollaboratorsClient : ApiClient, IRepoCollaboratorsClient
    {
        /// <summary>
        /// Initializes a new GitHub Repo Collaborators API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public RepoCollaboratorsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#list">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/repos/{owner}/{repo}/collaborators")]
        public Task<IReadOnlyList<User>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#list">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/repository/{id}/collaborators")]
        public Task<IReadOnlyList<User>> GetAll(long repositoryId)
        {
            return GetAll(repositoryId, ApiOptions.None);
        }

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
        [ManualRoute("GET", "/repos/{owner}/{repo}/collaborators")]
        public Task<IReadOnlyList<User>> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAll(owner, name, new RepositoryCollaboratorListRequest(), options);
        }

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#list">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/repository/{id}/collaborators")]
        public Task<IReadOnlyList<User>> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return GetAll(repositoryId, new RepositoryCollaboratorListRequest(), options);
        }

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
        [ManualRoute("GET", "/repos/{owner}/{repo}/collaborators")]
        public Task<IReadOnlyList<User>> GetAll(string owner, string name, RepositoryCollaboratorListRequest request)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAll(owner, name, request, ApiOptions.None);
        }

        /// <summary>
        /// Gets all the collaborators on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#list">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="request">Used to request and filter a list of repository collaborators</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/repository/{id}/collaborators")]
        public Task<IReadOnlyList<User>> GetAll(long repositoryId, RepositoryCollaboratorListRequest request)
        {
            Ensure.ArgumentNotNull(request, nameof(request));

            return GetAll(repositoryId, request, ApiOptions.None);
        }

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
        [ManualRoute("GET", "/repos/{owner}/{repo}/collaborators")]
        public Task<IReadOnlyList<User>> GetAll(string owner, string name, RepositoryCollaboratorListRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<User>(ApiUrls.RepoCollaborators(owner, name), request.ToParametersDictionary(), options);

        }

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
        [ManualRoute("GET", "/repository/{id}/collaborators")]
        public Task<IReadOnlyList<User>> GetAll(long repositoryId, RepositoryCollaboratorListRequest request, ApiOptions options)
        {
            Ensure.ArgumentNotNull(request, nameof(request));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<User>(ApiUrls.RepoCollaborators(repositoryId), request.ToParametersDictionary(), options);
        }

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
        [ManualRoute("GET", "/repos/{owner}/{repo}/collaborators/{username}")]
        public async Task<bool> IsCollaborator(string owner, string name, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            try
            {
                var response = await Connection.Get<object>(ApiUrls.RepoCollaborator(owner, name, user), null, null).ConfigureAwait(false);
                return response.HttpResponse.IsTrue();
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if a user is a collaborator on a repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#get">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="user">Username of the prospective collaborator</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/repository/{id}/collaborators/{username}")]
        public async Task<bool> IsCollaborator(long repositoryId, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            try
            {
                var response = await Connection.Get<object>(ApiUrls.RepoCollaborator(repositoryId, user), null, null).ConfigureAwait(false);
                return response.HttpResponse.IsTrue();
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

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
        [ManualRoute("GET", "/repos/{owner}/{repo}/collaborators/{username}/permission")]
        public Task<CollaboratorPermission> ReviewPermission(string owner, string name, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return ApiConnection
                .Get<CollaboratorPermission>(ApiUrls.RepoCollaboratorPermission(owner, name, user), null);
        }

        /// <summary>
        /// Review a user's permission level in a repository
        /// </summary>
        /// <remarks>
        /// See the <a href="https://developer.github.com/v3/repos/collaborators/#review-a-users-permission-level">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="user">Username of the collaborator to check permission for</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("GET", "/repository/{id}/collaborators/{username}/permission")]
        public Task<CollaboratorPermission> ReviewPermission(long repositoryId, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return ApiConnection
                .Get<CollaboratorPermission>(ApiUrls.RepoCollaboratorPermission(repositoryId, user), null);
        }

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
        [ManualRoute("PUT", "/repos/{owner}/{repo}/collaborators/{username}")]
        public Task Add(string owner, string name, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return ApiConnection.Put(ApiUrls.RepoCollaborator(owner, name, user));
        }

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
        [ManualRoute("PUT", "/repos/{owner}/{repo}/collaborators/{username}")]
        public async Task<RepositoryInvitation> Add(string owner, string name, string user, CollaboratorRequest permission)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            
            return await ApiConnection.Put<RepositoryInvitation>(ApiUrls.RepoCollaborator(owner, name, user), permission).ConfigureAwait(false);
        }

        /// <summary>
        /// Adds a new collaborator to the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#add-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="user">Username of the new collaborator</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("PUT", "/repository/{id}/collaborators/{username}")]
        public Task Add(long repositoryId, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return ApiConnection.Put(ApiUrls.RepoCollaborator(repositoryId, user));
        }

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
        [ManualRoute("PUT", "/repository/{id}/collaborators/{username}")]
        public async Task<RepositoryInvitation> Add(long repositoryId, string user, CollaboratorRequest permission)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return await ApiConnection.Put<RepositoryInvitation>(ApiUrls.RepoCollaborator(repositoryId, user), permission).ConfigureAwait(false);
        }

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
        [ManualRoute("PUT", "/repos/{owner}/{repo}/collaborators/{username}")]
        public Task<RepositoryInvitation> Invite(string owner, string name, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));


            return ApiConnection.Put<RepositoryInvitation>(ApiUrls.RepoCollaborator(owner, name, user), new object());
        }

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
        [ManualRoute("PUT", "/repos/{owner}/{repo}/collaborators/{username}")]
        public Task<RepositoryInvitation> Invite(string owner, string name, string user, CollaboratorRequest permission)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(permission, nameof(permission));

            return ApiConnection.Put<RepositoryInvitation>(ApiUrls.RepoCollaborator(owner, name, user), permission);
        }

        /// <summary>
        /// Invites a new collaborator to the repo
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#add-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository.</param>
        /// <param name="user">The name of the user to invite.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("PUT", "/repository/{id}/collaborators/{username}")]
        public Task<RepositoryInvitation> Invite(long repositoryId, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return ApiConnection.Put<RepositoryInvitation>(ApiUrls.RepoCollaborator(repositoryId, user), new object());
        }

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
        [ManualRoute("PUT", "/repository/{id}/collaborators/{username}")]
        public Task<RepositoryInvitation> Invite(long repositoryId, string user, CollaboratorRequest permission)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));
            Ensure.ArgumentNotNull(permission, nameof(permission));

            return ApiConnection.Put<RepositoryInvitation>(ApiUrls.RepoCollaborator(repositoryId, user), permission);
        }

        /// <summary>
        /// Deletes a collaborator from the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#remove-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="user">Username of the deleted collaborator</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("DELETE", "/repos/{owner}/{repo}/collaborators/{username}")]
        public Task Delete(string owner, string name, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return ApiConnection.Delete(ApiUrls.RepoCollaborator(owner, name, user));
        }

        /// <summary>
        /// Deletes a collaborator from the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#remove-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="repositoryId">The id of the repository</param>
        /// <param name="user">Username of the deleted collaborator</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        [ManualRoute("DELETE", "/repository/{id}/collaborators/{username}")]
        public Task Delete(long repositoryId, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, nameof(user));

            return ApiConnection.Delete(ApiUrls.RepoCollaborator(repositoryId, user));
        }
    }
}
