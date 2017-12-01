using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's References API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/git/refs/">References API documentation</a> for more information.
    /// </remarks>
    public class ReferencesClient : ApiClient, IReferencesClient
    {
        /// <summary>
        /// Instantiates a new GitHub References API client
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public ReferencesClient(IApiConnection apiConnection) :
            base(apiConnection)
        {
        }

        /// <summary>
        /// Gets a reference for a given repository by reference name
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-a-reference
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The name of the reference</param>
        /// <returns></returns>
        public Task<Reference> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return ApiConnection.Get<Reference>(ApiUrls.Reference(owner, name, reference));
        }

        /// <summary>
        /// Gets a reference for a given repository by reference name
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-a-reference
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The name of the reference</param>
        /// <returns></returns>
        public Task<Reference> Get(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return ApiConnection.Get<Reference>(ApiUrls.Reference(repositoryId, reference));
        }

        /// <summary>
        /// Gets all references for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-all-references
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        public Task<IReadOnlyList<Reference>> GetAll(string owner, string name)
        {
            return GetAll(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all references for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-all-references
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        public Task<IReadOnlyList<Reference>> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Reference>(ApiUrls.Reference(owner, name), options);
        }

        /// <summary>
        /// Gets all references for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-all-references
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns></returns>
        public Task<IReadOnlyList<Reference>> GetAll(long repositoryId)
        {
            return GetAll(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all references for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-all-references
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        public Task<IReadOnlyList<Reference>> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<Reference>(ApiUrls.Reference(repositoryId), options);
        }

        /// <summary>
        /// Gets references for a given repository by sub-namespace, i.e. "tags" or "heads"
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-all-references
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="subNamespace">The sub-namespace to get references for</param>
        /// <returns></returns>
        public Task<IReadOnlyList<Reference>> GetAllForSubNamespace(string owner, string name, string subNamespace)
        {
            return GetAllForSubNamespace(owner, name, subNamespace, ApiOptions.None);
        }

        /// <summary>
        /// Gets references for a given repository by sub-namespace, i.e. "tags" or "heads"
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-all-references
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="subNamespace">The sub-namespace to get references for</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        public Task<IReadOnlyList<Reference>> GetAllForSubNamespace(string owner, string name, string subNamespace, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(subNamespace, "subNamespace");
            Ensure.ArgumentNotNull(options, "options");

            // TODO: Handle 404 when subNamespace cannot be found

            return ApiConnection.GetAll<Reference>(ApiUrls.Reference(owner, name, subNamespace), options);
        }

        /// <summary>
        /// Gets references for a given repository by sub-namespace, i.e. "tags" or "heads"
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-all-references
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="subNamespace">The sub-namespace to get references for</param>
        /// <returns></returns>
        public Task<IReadOnlyList<Reference>> GetAllForSubNamespace(long repositoryId, string subNamespace)
        {
            return GetAllForSubNamespace(repositoryId, subNamespace, ApiOptions.None);
        }

        /// <summary>
        /// Gets references for a given repository by sub-namespace, i.e. "tags" or "heads"
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-all-references
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="subNamespace">The sub-namespace to get references for</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        public Task<IReadOnlyList<Reference>> GetAllForSubNamespace(long repositoryId, string subNamespace, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(subNamespace, "subNamespace");
            Ensure.ArgumentNotNull(options, "options");

            // TODO: Handle 404 when subNamespace cannot be found

            return ApiConnection.GetAll<Reference>(ApiUrls.Reference(repositoryId, subNamespace), options);
        }

        /// <summary>
        /// Creates a reference for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#create-a-reference
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference to create</param>
        /// <returns></returns>
        public Task<Reference> Create(string owner, string name, NewReference reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(reference, "reference");

            return ApiConnection.Post<Reference>(ApiUrls.Reference(owner, name), reference);
        }

        /// <summary>
        /// Creates a reference for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#create-a-reference
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference to create</param>
        /// <returns></returns>
        public Task<Reference> Create(long repositoryId, NewReference reference)
        {
            Ensure.ArgumentNotNull(reference, "reference");

            return ApiConnection.Post<Reference>(ApiUrls.Reference(repositoryId), reference);
        }

        /// <summary>
        /// Updates a reference for a given repository by reference name
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#update-a-reference
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The name of the reference</param>
        /// <param name="referenceUpdate">The updated reference data</param>
        /// <returns></returns>
        public Task<Reference> Update(string owner, string name, string reference, ReferenceUpdate referenceUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");
            Ensure.ArgumentNotNull(referenceUpdate, "update");

            return ApiConnection.Patch<Reference>(ApiUrls.Reference(owner, name, reference), referenceUpdate);
        }

        /// <summary>
        /// Updates a reference for a given repository by reference name
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#update-a-reference
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The name of the reference</param>
        /// <param name="referenceUpdate">The updated reference data</param>
        /// <returns></returns>
        public Task<Reference> Update(long repositoryId, string reference, ReferenceUpdate referenceUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");
            Ensure.ArgumentNotNull(referenceUpdate, "update");

            return ApiConnection.Patch<Reference>(ApiUrls.Reference(repositoryId, reference), referenceUpdate);
        }

        /// <summary>
        /// Deletes a reference for a given repository by reference name
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#delete-a-reference
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The name of the reference</param>
        /// <returns></returns>
        public Task Delete(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return ApiConnection.Delete(ApiUrls.Reference(owner, name, reference));
        }

        /// <summary>
        /// Deletes a reference for a given repository by reference name
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#delete-a-reference
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The name of the reference</param>
        /// <returns></returns>
        public Task Delete(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return ApiConnection.Delete(ApiUrls.Reference(repositoryId, reference));
        }
    }
}
