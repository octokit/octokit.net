using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Git Trees API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/git/trees/">Git Trees API documentation</a> for more information.
    /// </remarks>
    public interface ITreesClient
    {
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        Task<TreeResponse> Get(string owner, string name, string reference);

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
        Task<TreeResponse> Create(string owner, string name, NewTree newTree);
    }
}