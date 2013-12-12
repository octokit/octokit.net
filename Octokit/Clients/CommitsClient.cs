using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Git Commits API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/git/commits/">Git Commits API documentation</a> for more information.
    /// </remarks>
    public class CommitsClient : ApiClient, ICommitsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Git Commits API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public CommitsClient(IApiConnection apiConnection) : 
            base(apiConnection)
        {
        }

        /// <summary>
        /// Gets a commit for a given repository by sha reference
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/commits/#get-a-commit
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">Tha sha reference of the commit</param>
        /// <returns></returns>
        public Task<Commit> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return ApiConnection.Get<Commit>(ApiUrls.Commit(owner, name, reference));
        }

        /// <summary>
        /// Create a commit for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/commits/#create-a-commit
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="commit">The commit to create</param>
        /// <returns></returns>
        public Task<Commit> Create(string owner, string name, NewCommit commit)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(commit, "commit");

            return ApiConnection.Post<Commit>(ApiUrls.CreateCommit(owner, name), commit);
        }
    }
}