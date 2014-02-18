using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Git Blobs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/git/blobs/">Git Blobs API documentation</a> for more information.
    /// </remarks>
    public class BlobsClient : ApiClient, IBlobsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Git Blobs API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public BlobsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets a single Blob by SHA.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/blobs/#get-a-blob
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="reference">The SHA of the blob</param>
        /// <returns>The <see cref="Blob"/> for the specified SHA.</returns>
        public Task<Blob> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return ApiConnection.Get<Blob>(ApiUrls.Blob(owner, name, reference));
        }

        /// <summary>
        /// Creates a new Blob
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/blobs/#create-a-blob
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newBlob">The new Blob</param>
        /// <returns>The <see cref="Blob"/> that was just created.</returns>
        public Task<BlobReference> Create(string owner, string name, NewBlob newBlob)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newBlob, "newBlob");

            return ApiConnection.Post<BlobReference>(ApiUrls.Blob(owner, name), newBlob);
        }
    }
}
