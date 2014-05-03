using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableBlobClient : IObservableBlobsClient
    {
        readonly IBlobsClient _client;

        public ObservableBlobClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.GitDatabase.Blob;
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
        public IObservable<Blob> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return _client.Get(owner, name, reference).ToObservable();
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
        public IObservable<BlobReference> Create(string owner, string name, NewBlob newBlob)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newBlob, "newBlob");

            return _client.Create(owner, name, newBlob).ToObservable();
        }
    }
}
