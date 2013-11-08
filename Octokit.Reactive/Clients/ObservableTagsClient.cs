using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableTagsClient : IObservableTagsClient
    {
        readonly ITagsClient _client;

        public ObservableTagsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.GitDatabase.Tag;
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
        /// <returns></returns>
        public IObservable<GitTag> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return _client.Get(owner, name, reference).ToObservable();
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
        /// <returns></returns>
        public IObservable<GitTag> Create(string owner, string name, NewTag tag)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(tag, "tag");

            return _client.Create(owner, name, tag).ToObservable();
        }
    }
}