using System;
using System.Linq;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Git Trees API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/git/trees/">Git Trees API documentation</a> for more information.
    /// </remarks>
    public class TreesClient : ApiClient, ITreesClient
    {
        /// <summary>
        /// Instantiates a new GitHub Git Trees API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/git/trees/{tree_sha}")]
        public Task<TreeResponse> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<TreeResponse>(ApiUrls.Tree(owner, name, reference));
        }

        /// <summary>
        /// Gets a Tree Response for a given SHA.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/trees/#get-a-tree
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The SHA that references the tree</param>
        [ManualRoute("GET", "/repositories/{id}/git/trees/{tree_sha}")]
        public Task<TreeResponse> Get(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<TreeResponse>(ApiUrls.Tree(repositoryId, reference));
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
        [ManualRoute("GET", "/repos/{owner}/{repo}/git/trees/{tree_sha}?recursive=1")]
        public Task<TreeResponse> GetRecursive(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<TreeResponse>(ApiUrls.TreeRecursive(owner, name, reference));
        }

        /// <summary>
        /// Gets a Tree Response for a given SHA.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/git/trees/#get-a-tree-recursively
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The SHA that references the tree</param>
        [ManualRoute("GET", "/repositories/{id}/git/trees/{tree_sha}?recursive=1")]
        public Task<TreeResponse> GetRecursive(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<TreeResponse>(ApiUrls.TreeRecursive(repositoryId, reference));
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
        [ManualRoute("POST", "/repos/{owner}/{repo}/git/trees")]
        public Task<TreeResponse> Create(string owner, string name, NewTree newTree)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newTree, nameof(newTree));

            if (newTree.Tree.Any(t => string.IsNullOrWhiteSpace(t.Mode)))
            {
                throw new ArgumentException("You have specified items in the tree which do not have a Mode value set.");
            }

            return ApiConnection.Post<TreeResponse>(ApiUrls.Tree(owner, name), newTree);
        }

        /// <summary>
        /// Creates a new Tree in the specified repo
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/trees/#create-a-tree
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newTree">The value of the new tree</param>
        [ManualRoute("POST", "/repos/{id}/git/trees")]
        public Task<TreeResponse> Create(long repositoryId, NewTree newTree)
        {
            Ensure.ArgumentNotNull(newTree, nameof(newTree));

            if (newTree.Tree.Any(t => string.IsNullOrWhiteSpace(t.Mode)))
            {
                throw new ArgumentException("You have specified items in the tree which do not have a Mode value set.");
            }

            return ApiConnection.Post<TreeResponse>(ApiUrls.Tree(repositoryId), newTree);
        }
    }
}
