using System;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Git Blobs API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/git/blobs/">Git Blobs API documentation</a> for more information.
    /// </remarks>
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        IObservable<Blob> Get(string owner, string name, string reference);

        /// <summary>
        /// Gets a single Blob by SHA.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/blobs/#get-a-blob
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">The SHA of the blob</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        IObservable<Blob> Get(long repositoryId, string reference);

        /// <summary>
        /// Creates a new Blob
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/blobs/#create-a-blob
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newBlob">The new Blob</param>
        IObservable<BlobReference> Create(string owner, string name, NewBlob newBlob);

        /// <summary>
        /// Creates a new Blob
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/blobs/#create-a-blob
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="newBlob">The new Blob</param>
        IObservable<BlobReference> Create(long repositoryId, NewBlob newBlob);
    }
}