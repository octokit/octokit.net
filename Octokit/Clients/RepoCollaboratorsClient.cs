#if NET_45
using System.Collections.Generic;
using System.Threading.Tasks;
#endif

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
        public Task<IReadOnlyList<User>> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

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
        public Task<IReadOnlyList<User>> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<User>(ApiUrls.RepoCollaborators(owner, name), options);
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
        public Task<IReadOnlyList<User>> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<User>(ApiUrls.RepoCollaborators(repositoryId), options);
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
        public async Task<bool> IsCollaborator(string owner, string name, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

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
        public async Task<bool> IsCollaborator(long repositoryId, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

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
        /// Adds a new collaborator to the repository.
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/repos/collaborators/#add-collaborator">API documentation</a> for more information.
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="user">Username of the new collaborator</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        public Task Add(string owner, string name, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

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
        public async Task<bool> Add(string owner, string name, string user, CollaboratorRequest permission)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            try
            {
                var response = await Connection.Put<object>(ApiUrls.RepoCollaborator(owner, name, user), permission).ConfigureAwait(false);
                return response.HttpResponse.IsTrue();
            }
            catch
            {
                return false;
            }
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
        public Task Add(long repositoryId, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

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
        public async Task<bool> Add(long repositoryId, string user, CollaboratorRequest permission)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            try
            {
                var response = await Connection.Put<object>(ApiUrls.RepoCollaborator(repositoryId, user), permission).ConfigureAwait(false);
                return response.HttpResponse.IsTrue();
            }
            catch
            {
                return false;
            }
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
        public Task<RepositoryInvitation> Invite(string owner, string name, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return ApiConnection.Put<RepositoryInvitation>(ApiUrls.RepoCollaborator(owner, name, user), new object(), null, AcceptHeaders.InvitationsApiPreview);
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
        public Task<RepositoryInvitation> Invite(string owner, string name, string user, CollaboratorRequest permission)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(permission, "permission");

            return ApiConnection.Put<RepositoryInvitation>(ApiUrls.RepoCollaborator(owner, name, user), permission, null, AcceptHeaders.InvitationsApiPreview);
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
        public Task<RepositoryInvitation> Invite(long repositoryId, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return ApiConnection.Put<RepositoryInvitation>(ApiUrls.RepoCollaborator(repositoryId, user), new object(), null, AcceptHeaders.InvitationsApiPreview);
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
        public Task<RepositoryInvitation> Invite(long repositoryId, string user, CollaboratorRequest permission)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");
            Ensure.ArgumentNotNull(permission, "permission");

            return ApiConnection.Put<RepositoryInvitation>(ApiUrls.RepoCollaborator(repositoryId, user), permission, null, AcceptHeaders.InvitationsApiPreview);
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
        public Task Delete(string owner, string name, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

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
        public Task Delete(long repositoryId, string user)
        {
            Ensure.ArgumentNotNullOrEmptyString(user, "user");

            return ApiConnection.Delete(ApiUrls.RepoCollaborator(repositoryId, user));
        }
    }
}