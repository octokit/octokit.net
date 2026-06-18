using System;
using System.Reactive.Threading.Tasks;
using System.Threading;

namespace Octokit.Reactive
{
    public class ObservableCodespacesClient : IObservableCodespacesClient
    {
        private readonly ICodespacesClient _client;
        private readonly IConnection _connection;

        public ObservableCodespacesClient(IGitHubClient githubClient)
        {
            _client = githubClient.Codespaces;
            _connection = githubClient.Connection;
        }

        public IObservable<Codespace> Get(string codespaceName, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(codespaceName, nameof(codespaceName));
            return _client.Get(codespaceName, cancellationToken).ToObservable();
        }

        public IObservable<CodespacesCollection> GetAll(CancellationToken cancellationToken = default)
        {
            return _client.GetAll(cancellationToken).ToObservable();
        }

        public IObservable<MachinesCollection> GetAvailableMachinesForRepo(string repoOwner, string repoName, string reference = null, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(repoOwner, nameof(repoOwner));
            Ensure.ArgumentNotNull(repoName, nameof(repoName));
            return _client.GetAvailableMachinesForRepo(repoOwner, repoName, reference, cancellationToken).ToObservable();
        }

        public IObservable<CodespacesCollection> GetForRepository(string owner, string repo, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(owner, nameof(owner));
            Ensure.ArgumentNotNull(repo, nameof(repo));
            return _client.GetForRepository(owner, repo, cancellationToken).ToObservable();
        }

        public IObservable<Codespace> Start(string codespaceName, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(codespaceName, nameof(codespaceName));
            return _client.Start(codespaceName, cancellationToken).ToObservable();
        }

        public IObservable<Codespace> Stop(string codespaceName, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(codespaceName, nameof(codespaceName));
            return _client.Stop(codespaceName, cancellationToken).ToObservable();
        }

        public IObservable<Codespace> Create(string owner, string repo, NewCodespace newCodespace, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(owner, nameof(owner));
            Ensure.ArgumentNotNull(repo, nameof(repo));
            Ensure.ArgumentNotNull(newCodespace, nameof(newCodespace));
            return _client.Create(owner, repo, newCodespace, cancellationToken).ToObservable();
        }
    }
}
