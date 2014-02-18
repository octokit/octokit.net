using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{

    public class ObservableTreesClient : IObservableTreesClient
    {
        readonly ITreesClient _client;

        public ObservableTreesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.GitDatabase.Tree;
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
        /// <returns>The <see cref="TreeResponse"/> for the specified Tree.</returns>
        public IObservable<TreeResponse> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return _client.Get(owner, name, reference).ToObservable();
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
        /// <returns>The <see cref="TreeResponse"/> that was just created.</returns>
        public IObservable<TreeResponse> Create(string owner, string name, NewTree newTree)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newTree, "newTree");

            return _client.Create(owner, name, newTree).ToObservable();
        }
    }
}
