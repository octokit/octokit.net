﻿using System.Threading.Tasks;

namespace Octokit
{
    public class TreesClient : ApiClient, ITreesClient
    {
        public TreesClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
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
        public Task<TreeResponse> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return ApiConnection.Get<TreeResponse>(ApiUrls.Tree(owner, name, reference));
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
        public Task<TreeResponse> Create(string owner, string name, NewTree newTree)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newTree, "newTree");

            return ApiConnection.Post<TreeResponse>(ApiUrls.Tree(owner, name), newTree);
        }
    }
}
