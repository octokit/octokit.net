using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Git Tags API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/git/tags/">Git Tags API documentation</a> for more information.
    /// </remarks>
    public class ObservableTagsClient : IObservableTagsClient
    {
        readonly ITagsClient _client;

        public ObservableTagsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Git.Tag;
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
        public IObservable<GitTag> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return _client.Get(owner, name, reference).ToObservable();
        }

        /// <summary>
        /// Gets a tag for a given repository by sha reference
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/tags/#get-a-tag
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="reference">Tha sha reference of the tag</param>
        public IObservable<GitTag> Get(long repositoryId, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(reference, nameof(reference));

            return _client.Get(repositoryId, reference).ToObservable();
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
        public IObservable<GitTag> Create(string owner, string name, NewTag tag)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(tag, nameof(tag));

            return _client.Create(owner, name, tag).ToObservable();
        }

        /// <summary>
        /// Create a tag for a given repository
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/git/tags/#create-a-tag-object
        /// </remarks>
        /// <param name="repositoryId">The Id of the repository</param>
        /// <param name="tag">The tag to create</param>
        public IObservable<GitTag> Create(long repositoryId, NewTag tag)
        {
            Ensure.ArgumentNotNull(tag, nameof(tag));

            return _client.Create(repositoryId, tag).ToObservable();
        }
    }
}