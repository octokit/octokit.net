using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's References API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/git/refs/">References API documentation</a> for more information.
    /// </remarks>
    public class ObservableReferencesClient : IObservableReferencesClient
    {
        readonly IReferencesClient _reference;
        readonly IConnection _connection;

        public ObservableReferencesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _reference = client.Git.Reference;
            _connection = client.Connection;
        }

        /// <summary>
        /// Gets a reference for a given repository by reference name
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-a-reference
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The canonical name of the reference without the 'refs/' prefix. e.g. "heads/main" or "tags/release-1"</param>
        /// <returns></returns>
        public IObservable<Reference> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return _reference.Get(owner, name, reference).ToObservable();
        }

        /// <summary>
        /// Gets a reference for a given repository by reference name
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-a-reference
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The canonical name of the reference without the 'refs/' prefix. e.g. "heads/main" or "tags/release-1"</param>
        /// <returns></returns>
        public IObservable<Reference> Get(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return _reference.Get(repositoryId, reference).ToObservable();
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
        public IObservable<Reference> GetAll(string owner, string name)
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
        public IObservable<Reference> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Reference>(ApiUrls.Reference(owner, name), options);
        }

        /// <summary>
        /// Gets all references for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#get-all-references
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <returns></returns>
        public IObservable<Reference> GetAll(long repositoryId)
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
        public IObservable<Reference> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Reference>(ApiUrls.Reference(repositoryId), options);
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
        public IObservable<Reference> GetAllForSubNamespace(string owner, string name, string subNamespace)
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
        /// The subNamespace parameter supports either the fully-qualified ref
        /// (prefixed with  "refs/", e.g. "refs/heads/main" or
        /// "refs/tags/release-1") or the shortened form (omitting "refs/", e.g.
        /// "heads/main" or "tags/release-1")
        /// </remarks>
        public IObservable<Reference> GetAllForSubNamespace(string owner, string name, string subNamespace, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(subNamespace, nameof(subNamespace));
            Ensure.ArgumentNotNull(options, nameof(options));

            if (subNamespace.StartsWith("refs/"))
            {
                subNamespace = subNamespace.Replace("refs/", string.Empty);
            }

            return _connection.GetAndFlattenAllPages<Reference>(ApiUrls.Reference(owner, name, subNamespace), options);
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
        public IObservable<Reference> GetAllForSubNamespace(long repositoryId, string subNamespace)
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
        public IObservable<Reference> GetAllForSubNamespace(long repositoryId, string subNamespace, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(subNamespace, nameof(subNamespace));
            Ensure.ArgumentNotNull(options, nameof(options));

            if (subNamespace.StartsWith("refs/"))
            {
                subNamespace = subNamespace.Replace("refs/", string.Empty);
            }

            return _connection.GetAndFlattenAllPages<Reference>(ApiUrls.Reference(repositoryId, subNamespace), options);
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
        public IObservable<Reference> Create(string owner, string name, NewReference reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(reference, nameof(reference));

            return _reference.Create(owner, name, reference).ToObservable();
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
        public IObservable<Reference> Create(long repositoryId, NewReference reference)
        {
            Ensure.ArgumentNotNull(reference, nameof(reference));

            return _reference.Create(repositoryId, reference).ToObservable();
        }

        /// <summary>
        /// Updates a reference for a given repository by reference name
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#update-a-reference
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The canonical name of the reference without the 'refs/' prefix. e.g. "heads/main" or "tags/release-1"</param>
        /// <param name="referenceUpdate">The updated reference data</param>
        /// <returns></returns>
        public IObservable<Reference> Update(string owner, string name, string reference, ReferenceUpdate referenceUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(referenceUpdate, nameof(referenceUpdate));

            return _reference.Update(owner, name, reference, referenceUpdate).ToObservable();
        }

        /// <summary>
        /// Updates a reference for a given repository by reference name
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#update-a-reference
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The canonical name of the reference without the 'refs/' prefix. e.g. "heads/main" or "tags/release-1"</param>
        /// <param name="referenceUpdate">The updated reference data</param>
        /// <returns></returns>
        public IObservable<Reference> Update(long repositoryId, string reference, ReferenceUpdate referenceUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));
            Ensure.ArgumentNotNull(referenceUpdate, nameof(referenceUpdate));

            return _reference.Update(repositoryId, reference, referenceUpdate).ToObservable();
        }

        /// <summary>
        /// Deletes a reference for a given repository by reference name
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#delete-a-reference
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The canonical name of the reference without the 'refs/' prefix. e.g. "heads/main" or "tags/release-1"</param>
        /// <returns></returns>
        public IObservable<Unit> Delete(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return _reference.Delete(owner, name, reference).ToObservable();
        }

        /// <summary>
        /// Deletes a reference for a given repository by reference name
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/refs/#delete-a-reference
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The canonical name of the reference without the 'refs/' prefix. e.g. "heads/main" or "tags/release-1"</param>
        /// <returns></returns>
        public IObservable<Unit> Delete(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return _reference.Delete(repositoryId, reference).ToObservable();
        }
    }
}
