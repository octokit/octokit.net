using System;
using System.Threading;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Git Trees API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/git/trees/">Git Trees API documentation</a> for more information.
    /// </remarks>
    public class ObservableTreesClient : IObservableTreesClient
    {
        readonly ITreesClient _client;

        public ObservableTreesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Git.Tree;
        }

        /// <summary>
        /// Gets a Tree Response for a given SHA.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/trees/#get-a-tree
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The SHA that references the tree</param>
        public IObservable<TreeResponse> Get(string owner, string name, string reference, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return _client.Get(owner, name, reference, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets a Tree Response for a given SHA.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/trees/#get-a-tree
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The SHA that references the tree</param>
        public IObservable<TreeResponse> Get(long repositoryId, string reference, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return _client.Get(repositoryId, reference, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets a Tree Response for a given SHA.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/git/trees/#get-a-tree-recursively
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The SHA that references the tree</param>
        public IObservable<TreeResponse> GetRecursive(string owner, string name, string reference, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return _client.GetRecursive(owner, name, reference, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Gets a Tree Response for a given SHA.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/git/trees/#get-a-tree-recursively
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The SHA that references the tree</param>
        public IObservable<TreeResponse> GetRecursive(long repositoryId, string reference, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return _client.GetRecursive(repositoryId, reference, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Creates a new Tree in the specified repo
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/trees/#create-a-tree
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newTree">The value of the new tree</param>
        public IObservable<TreeResponse> Create(string owner, string name, NewTree newTree, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newTree, nameof(newTree));

            return _client.Create(owner, name, newTree, cancellationToken).ToObservable();
        }

        /// <summary>
        /// Creates a new Tree in the specified repo
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/trees/#create-a-tree
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newTree">The value of the new tree</param>
        public IObservable<TreeResponse> Create(long repositoryId, NewTree newTree, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(newTree, nameof(newTree));

            return _client.Create(repositoryId, newTree, cancellationToken).ToObservable();
        }
    }
}
