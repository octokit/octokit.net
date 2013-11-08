using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableCommitsClient : IObservableCommitsClient
    {
        readonly ICommitsClient _client;

        public ObservableCommitsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");
            _client = client.GitDatabase.Commit;
        }

        public IObservable<Commit> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return _client.Get(owner, name, reference).ToObservable();            
        }

        public IObservable<Commit> Create(string owner, string name, NewCommit commit)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(commit, "commit");

            return _client.Create(owner, name, commit).ToObservable();
        }
    }
}