using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Git Tags API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/git/tags/">Git Tags API documentation</a> for more information.
    /// </remarks>
    public class TagsClient : ApiClient, ITagsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Git Tags API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public TagsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets a tag for a given repository by sha reference
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/tags/#get-a-tag
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">Tha sha reference of the tag</param>
        [ManualRoute("GET", "/repos/{owner}/{repo}/git/tags/{sha}")]
        public Task<GitTag> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<GitTag>(ApiUrls.Tag(owner, name, reference));
        }

        /// <summary>
        /// Gets a tag for a given repository by sha reference
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/tags/#get-a-tag
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">Tha sha reference of the tag</param>
        [ManualRoute("GET", "/repositories/{id}/git/tags/{sha}")]
        public Task<GitTag> Get(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return ApiConnection.Get<GitTag>(ApiUrls.Tag(repositoryId, reference));
        }

        /// <summary>
        /// Create a tag for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/tags/#create-a-tag-object
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="tag">The tag to create</param>
        [ManualRoute("POST", "/repos/{owner}/{repo}/git/tags")]
        public Task<GitTag> Create(string owner, string name, NewTag tag)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(tag, nameof(tag));

            return ApiConnection.Post<GitTag>(ApiUrls.CreateTag(owner, name), tag);
        }

        /// <summary>
        /// Create a tag for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/tags/#create-a-tag-object
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="tag">The tag to create</param>
        [ManualRoute("POST", "/repositories/{id}/git/tags")]
        public Task<GitTag> Create(long repositoryId, NewTag tag)
        {
            Ensure.ArgumentNotNull(tag, nameof(tag));

            return ApiConnection.Post<GitTag>(ApiUrls.CreateTag(repositoryId), tag);
        }
    }
}
