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

        public IObservable<GitTag> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return _client.Get(owner, name, reference).ToObservable();
        }

        public IObservable<GitTag> Create(string owner, string name, NewTag tag)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(tag, "tag");

            return _client.Create(owner, name, tag).ToObservable();
        }
    }
}