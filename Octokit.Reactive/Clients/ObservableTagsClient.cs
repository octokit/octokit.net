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

            this._client = client.Tag;
        }

        public IObservable<Tag> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return this._client.Get(owner, name, reference).ToObservable();
        }

        public IObservable<Tag> Create(string owner, string name, NewTag tag)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(tag, "tag");

            return this._client.Create(owner, name, tag).ToObservable();
        }
    }
}