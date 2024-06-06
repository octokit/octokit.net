using System;
using System.Reactive.Threading.Tasks;

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

        public IObservable<Codespace> Get(string codespaceName)
        {
            Ensure.ArgumentNotNull(codespaceName, nameof(codespaceName));
            return _client.Get(codespaceName).ToObservable();
        }

        public IObservable<CodespacesCollection> GetAll()
        {
            return _client.GetAll().ToObservable();
        }

        public IObservable<MachinesCollection> GetAvailableMachinesForRepo(string repoOwner, string repoName, string reference = null)
        {
            Ensure.ArgumentNotNull(repoOwner, nameof(repoOwner));
            Ensure.ArgumentNotNull(repoName, nameof(repoName));
            return _client.GetAvailableMachinesForRepo(repoOwner, repoName, reference).ToObservable();
        }

        public IObservable<CodespacesCollection> GetForRepository(string owner, string repo)
        {
            Ensure.ArgumentNotNull(owner, nameof(owner));
            Ensure.ArgumentNotNull(repo, nameof(repo));
            return _client.GetForRepository(owner, repo).ToObservable();
        }

        public IObservable<Codespace> Start(string codespaceName)
        {
            Ensure.ArgumentNotNull(codespaceName, nameof(codespaceName));
            return _client.Start(codespaceName).ToObservable();
        }

        public IObservable<Codespace> Stop(string codespaceName)
        {
            Ensure.ArgumentNotNull(codespaceName, nameof(codespaceName));
            return _client.Stop(codespaceName).ToObservable();
        }

        public IObservable<Codespace> Create(string owner, string repo, NewCodespace newCodespace)
        {
            Ensure.ArgumentNotNull(owner, nameof(owner));
            Ensure.ArgumentNotNull(repo, nameof(repo));
            Ensure.ArgumentNotNull(newCodespace, nameof(newCodespace));
            return _client.Create(owner, repo, newCodespace).ToObservable();
        }
    }
}