using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's References API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/git/refs/">References API documentation</a> for more information.
    /// </remarks>
    public interface IObservableReferencesClient
    {
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
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        IObservable<Reference> Get(string owner, string name, string reference);
        
        /// <summary>
        /// Gets a reference for a given repository by reference name
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-a-reference
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The name of the reference</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
            Justification = "Method makes a network request")]
        IObservable<Reference> Get(int repositoryId, string reference);

        /// <summary>
        /// Gets all references for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-all-references
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        IObservable<Reference> GetAll(string owner, string name);

        /// <summary>
        /// Gets all references for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-all-references
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns></returns>
        IObservable<Reference> GetAll(int repositoryId);

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
        IObservable<Reference> GetAllForSubNamespace(string owner, string name, string subNamespace);

        /// <summary>
        /// Gets references for a given repository by sub-namespace, i.e. "tags" or "heads"
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-all-references
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="subNamespace">The sub-namespace to get references for</param>
        /// <returns></returns>
        IObservable<Reference> GetAllForSubNamespace(int repositoryId, string subNamespace);

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
        IObservable<Reference> Create(string owner, string name, NewReference reference);

        /// <summary>
        /// Creates a reference for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#create-a-reference
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The reference to create</param>
        /// <returns></returns>
        IObservable<Reference> Create(int repositoryId, NewReference reference);

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
        IObservable<Reference> Update(string owner, string name, string reference, ReferenceUpdate referenceUpdate);

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
        IObservable<Reference> Update(int repositoryId, string reference, ReferenceUpdate referenceUpdate);

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
        IObservable<Unit> Delete(string owner, string name, string reference);

        /// <summary>
        /// Deletes a reference for a given repository by reference name
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#delete-a-reference
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The name of the reference</param>
        /// <returns></returns>
        IObservable<Unit> Delete(int repositoryId, string reference);
    }
}