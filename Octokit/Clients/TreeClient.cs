using System.Threading.Tasks;

namespace Octokit
{
    public class TreeClient : ApiClient
    {
        public TreeClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets a Tree Response for a given SHA.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/comments/#get-a-single-comment
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The SHA that references the tree</param>
        /// <returns>The <see cref="IssueComment"/>s for the specified Issue Comment.</returns>
        public Task<TreeResponse> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return ApiConnection.Get<TreeResponse>(ApiUrls.Tree(owner, name, reference));
        }
    }
}
