using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Git Merging API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/merging/">Git Merging API documentation</a> for more information.
    /// </remarks>
    public class MergingClient : ApiClient, IMergingClient
    {
        public MergingClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Create a merge for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/merging/#perform-a-merge
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="baseBranch">The name of the base branch that the head will be merged into</param>
        /// <param name="head">The head to merge. This can be a branch name or a commit SHA1</param>
        /// <param name="merge">The merge to create</param>
        /// <returns></returns>
        public Task<Commit> Create(string owner, string name, string baseBranch, string head, NewMerge merge)
        {
            Ensure.ArgumentNotNullOrEmptyString(baseBranch, "baseBranch");
            Ensure.ArgumentNotNullOrEmptyString(head, "head");
            
            return ApiConnection.Post<Commit>(ApiUrls.CreateMerge(owner, name), merge);
        }
    }
}