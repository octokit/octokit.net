using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Git Blobs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/git/blobs/">Git Blobs API documentation</a> for more information.
    /// </remarks>
    public class ObservableBlobClient : IObservableBlobsClient
    {
        readonly IBlobsClient _client;

        public ObservableBlobClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Git.Blob;
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
        public IObservable<Blob> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return _client.Get(owner, name, reference).ToObservable();
        }

        /// <summary>
        /// Gets a single Blob by SHA.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/blobs/#get-a-blob
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The SHA of the blob</param>
        public IObservable<Blob> Get(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return _client.Get(repositoryId, reference).ToObservable();
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
        public IObservable<BlobReference> Create(string owner, string name, NewBlob newBlob)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(newBlob, nameof(newBlob));

            return _client.Create(owner, name, newBlob).ToObservable();
        }

        /// <summary>
        /// Creates a new Blob
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/blobs/#create-a-blob
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newBlob">The new Blob</param>
        public IObservable<BlobReference> Create(long repositoryId, NewBlob newBlob)
        {
            Ensure.ArgumentNotNull(newBlob, nameof(newBlob));

            return _client.Create(repositoryId, newBlob).ToObservable();
        }
    }
}
