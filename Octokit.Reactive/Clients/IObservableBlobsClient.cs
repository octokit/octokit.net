using System;

namespace Octokit.Reactive
{
    public interface IObservableBlobsClient
    {
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        IObservable<Blob> Get(string owner, string name, string reference);

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
        IObservable<BlobReference> Create(string owner, string name, NewBlob newBlob);
    }
}