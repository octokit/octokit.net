using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's References API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/git/refs/">References API documentation</a> for more information.
    /// </remarks>
    public interface IReferencesClient
    {
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
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
             Justification = "Method makes a network request")]
        Task<Reference> Get(string owner, string name, string reference);

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
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
             Justification = "Method makes a network request")]
        Task<Reference> Get(long repositoryId, string reference);

        /// <summary>
        /// Gets all references for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-all-references
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        Task<IReadOnlyList<Reference>> GetAll(string owner, string name);

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
        Task<IReadOnlyList<Reference>> GetAll(string owner, string name, ApiOptions options);

        /// <summary>
        /// Gets all references for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-all-references
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns></returns>
        Task<IReadOnlyList<Reference>> GetAll(long repositoryId);

        /// <summary>
        /// Gets all references for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-all-references
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns></returns>
        Task<IReadOnlyList<Reference>> GetAll(long repositoryId, ApiOptions options);

        /// <summary>
        /// Gets references for a given repository by sub-namespace, i.e. "tags" or "heads"
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-all-references
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="subNamespace">The sub-namespace to get references for</param>
        /// <remarks>
        /// The subNamespace parameter supports either the fully-qualified ref
        /// (prefixed with  "refs/", e.g. "refs/heads/main" or
        /// "refs/tags/release-1") or the shortened form (omitting "refs/", e.g.
        /// "heads/main" or "tags/release-1")
        /// </remarks>
        Task<IReadOnlyList<Reference>> GetAllForSubNamespace(string owner, string name, string subNamespace);

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
        /// The subNamespace parameter supports either the fully-qualified ref
        /// (prefixed with  "refs/", e.g. "refs/heads/main" or
        /// "refs/tags/release-1") or the shortened form (omitting "refs/", e.g.
        /// "heads/main" or "tags/release-1")
        /// </remarks>
        Task<IReadOnlyList<Reference>> GetAllForSubNamespace(string owner, string name, string subNamespace, ApiOptions options);

        /// <summary>
        /// Gets references for a given repository by sub-namespace, i.e. "tags" or "heads"
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-all-references
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="subNamespace">The sub-namespace to get references for</param>
        /// <remarks>
        /// The subNamespace parameter supports either the fully-qualified ref
        /// (prefixed with  "refs/", e.g. "refs/heads/main" or
        /// "refs/tags/release-1") or the shortened form (omitting "refs/", e.g.
        /// "heads/main" or "tags/release-1")
        /// </remarks>
        Task<IReadOnlyList<Reference>> GetAllForSubNamespace(long repositoryId, string subNamespace);

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
        Task<IReadOnlyList<Reference>> GetAllForSubNamespace(long repositoryId, string subNamespace, ApiOptions options);

        /// <summary>
        /// Creates a reference for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#create-a-reference
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The reference to create</param>
        /// <remarks>
        /// The reference parameter supports either the fully-qualified ref
        /// (prefixed with  "refs/", e.g. "refs/heads/main" or
        /// "refs/tags/release-1") or the shortened form (omitting "refs/", e.g.
        /// "heads/main" or "tags/release-1")
        /// </remarks>
        Task<Reference> Create(string owner, string name, NewReference reference);

        /// <summary>
        /// Creates a reference for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#create-a-reference
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference to create</param>
        /// <returns></returns>
        Task<Reference> Create(long repositoryId, NewReference reference);

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
        Task<Reference> Update(string owner, string name, string reference, ReferenceUpdate referenceUpdate);

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
        Task<Reference> Update(long repositoryId, string reference, ReferenceUpdate referenceUpdate);

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
        Task Delete(string owner, string name, string reference);

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
        Task Delete(long repositoryId, string reference);
    }
}
