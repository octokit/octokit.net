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
        /// <param name="reference">The reference name</param>
        /// <remarks>
        /// The reference parameter supports either the fully-qualified ref
        /// (prefixed with  "refs/", e.g. "refs/heads/main" or
        /// "refs/tags/release-1") or the shortened form (omitting "refs/", e.g.
        /// "heads/main" or "tags/release-1")
        /// </remarks>
        [ManualRoute("GET", "/repos/{owner}/{repo}/git/refs/{ref}")]
        public Task<Reference> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            if (reference.StartsWith("refs/"))
            {
                reference = reference.Replace("refs/", string.Empty);
            }

            return ApiConnection.Get<Reference>(ApiUrls.Reference(owner, name, reference));
        }

        /// <summary>
        /// Gets a reference for a given repository by reference name
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-a-reference
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference name</param>
        /// <remarks>
        /// The reference parameter supports either the fully-qualified ref
        /// (prefixed with  "refs/", e.g. "refs/heads/main" or
        /// "refs/tags/release-1") or the shortened form (omitting "refs/", e.g.
        /// "heads/main" or "tags/release-1")
        /// </remarks>
        [ManualRoute("GET", "/repositories/{id}/git/refs/{ref}")]
        public Task<Reference> Get(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            if (reference.StartsWith("refs/"))
            {
                reference = reference.Replace("refs/", string.Empty);
            }

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
        [ManualRoute("GET", "/repos/{owner}/{repo}/git/refs")]
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/git/refs")]
        public Task<IReadOnlyList<Reference>> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

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
        [ManualRoute("GET", "/repositories/{id}/git/refs")]
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
        [ManualRoute("GET", "/repositories/{id}/git/refs")]
        public Task<IReadOnlyList<Reference>> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

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
        [ManualRoute("GET", "/repos/{owner}/{repo}/git/refs/{ref}")]
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
        /// <remarks>
        /// The reference parameter supports either the fully-qualified ref
        /// (prefixed with  "refs/", e.g. "refs/heads/main" or
        /// "refs/tags/release-1") or the shortened form (omitting "refs/", e.g.
        /// "heads/main" or "tags/release-1")
        /// </remarks>
        [ManualRoute("GET", "/repos/{owner}/{repo}/git/refs/{ref}")]
        public Task<IReadOnlyList<Reference>> GetAllForSubNamespace(string owner, string name, string subNamespace, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(subNamespace, nameof(subNamespace));
            Ensure.ArgumentNotNull(options, nameof(options));

            if (subNamespace.StartsWith("refs/"))
            {
                subNamespace = subNamespace.Replace("refs/", string.Empty);
            }

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
        [ManualRoute("GET", "/repositories/{id}/git/refs/{ref}")]
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
        /// <remarks>
        /// The subNamespace parameter supports either the fully-qualified ref
        /// (prefixed with  "refs/", e.g. "refs/heads/main" or
        /// "refs/tags/release-1") or the shortened form (omitting "refs/", e.g.
        /// "heads/main" or "tags/release-1")
        /// </remarks>
        [ManualRoute("GET", "/repositories/{id}/git/refs/{ref}")]
        public Task<IReadOnlyList<Reference>> GetAllForSubNamespace(long repositoryId, string subNamespace, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(subNamespace, nameof(subNamespace));
            Ensure.ArgumentNotNull(options, nameof(options));

            if (subNamespace.StartsWith("refs/"))
            {
                subNamespace = subNamespace.Replace("refs/", string.Empty);
            }

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
        [ManualRoute("POST", "/repos/{owner}/{repo}/git/refs")]
        public Task<Reference> Create(string owner, string name, NewReference reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(reference, nameof(reference));

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
        [ManualRoute("POST", "/repositories/{id}/git/refs")]
        public Task<Reference> Create(long repositoryId, NewReference reference)
        {
            Ensure.ArgumentNotNull(reference, nameof(reference));

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
        /// <param name="reference">The reference name</param>
        /// <param name="referenceUpdate">The updated reference data</param>
        /// <remarks>
        /// The reference parameter supports either the fully-qualified ref
        /// (prefixed with  "refs/", e.g. "refs/heads/main" or
        /// "refs/tags/release-1") or the shortened form (omitting "refs/", e.g.
        /// "heads/main" or "tags/release-1")
        /// </remarks>
        [ManualRoute("PATCH", "/repos/{owner}/{repo}/git/refs/{ref}")]
        public Task<Reference> Update(string owner, string name, string reference, ReferenceUpdate referenceUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(referenceUpdate, nameof(referenceUpdate));

            if (reference.StartsWith("refs/"))
            {
                reference = reference.Replace("refs/", string.Empty);
            }

            return ApiConnection.Patch<Reference>(ApiUrls.Reference(owner, name, reference), referenceUpdate);
        }

        /// <summary>
        /// Updates a reference for a given repository by reference name
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#update-a-reference
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference name</param>
        /// <param name="referenceUpdate">The updated reference data</param>
        /// <remarks>
        /// The reference parameter supports either the fully-qualified ref
        /// (prefixed with  "refs/", e.g. "refs/heads/main" or
        /// "refs/tags/release-1") or the shortened form (omitting "refs/", e.g.
        /// "heads/main" or "tags/release-1")
        /// </remarks>
        [ManualRoute("PATCH", "/repositories/{id}/git/refs/{ref}")]
        public Task<Reference> Update(long repositoryId, string reference, ReferenceUpdate referenceUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(referenceUpdate, nameof(referenceUpdate));

            if (reference.StartsWith("refs/"))
            {
                reference = reference.Replace("refs/", string.Empty);
            }

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
        /// <param name="reference">The reference name</param>
        /// <remarks>
        /// The reference parameter supports either the fully-qualified ref
        /// (prefixed with  "refs/", e.g. "refs/heads/main" or
        /// "refs/tags/release-1") or the shortened form (omitting "refs/", e.g.
        /// "heads/main" or "tags/release-1")
        /// </remarks>
        [ManualRoute("PATCH", "/repos/{owner}/{repo}/git/refs/{ref}")]
        public Task Delete(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            if (reference.StartsWith("refs/"))
            {
                reference = reference.Replace("refs/", string.Empty);
            }

            return ApiConnection.Delete(ApiUrls.Reference(owner, name, reference));
        }

        /// <summary>
        /// Deletes a reference for a given repository by reference name
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#delete-a-reference
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference name</param>
        /// <remarks>
        /// The reference parameter supports either the fully-qualified ref
        /// (prefixed with  "refs/", e.g. "refs/heads/main" or
        /// "refs/tags/release-1") or the shortened form (omitting "refs/", e.g.
        /// "heads/main" or "tags/release-1")
        /// </remarks>
        [ManualRoute("DELETE", "/repositories/{id}/git/refs/{ref}")]
        public Task Delete(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            if (reference.StartsWith("refs/"))
            {
                reference = reference.Replace("refs/", string.Empty);
            }

            return ApiConnection.Delete(ApiUrls.Reference(repositoryId, reference));
        }
    }
}
