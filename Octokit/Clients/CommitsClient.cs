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
        [ManualRoute("GET", "/repos/{owner}/{repo}/git/commits/{commit_sha}")]
        public Task<Commit> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<Commit>(ApiUrls.Commit(owner, name, reference));
        }

        /// <summary>
        /// Gets a commit for a given repository by sha reference
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/commits/#get-a-commit
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">Tha sha reference of the commit</param>
        [ManualRoute("GET", "/repositories/{id}/git/commits/{commit_sha}")]
        public Task<Commit> Get(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<Commit>(ApiUrls.Commit(repositoryId, reference));
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
        [ManualRoute("POST", "/repos/{owner}/{repo}/git/commits")]
        public Task<Commit> Create(string owner, string name, NewCommit commit)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(commit, nameof(commit));

            return ApiConnection.Post<Commit>(ApiUrls.CreateCommit(owner, name), commit);
        }

        /// <summary>
        /// Create a commit for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/commits/#create-a-commit
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="commit">The commit to create</param>
        [ManualRoute("POST", "/repositories/{id}/git/commits")]
        public Task<Commit> Create(long repositoryId, NewCommit commit)
        {
            Ensure.ArgumentNotNull(commit, nameof(commit));

            return ApiConnection.Post<Commit>(ApiUrls.CreateCommit(repositoryId), commit);
        }
    }
}
