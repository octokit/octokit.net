using System;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit.Reactive
{
    /// <summary>
    /// A client for GitHub's Codespaces API.
    /// </summary>
    /// <remarks>
    /// See the codespaces API documentation for more information.
    /// </remarks>
    public interface IObservableCodespacesClient
    {
        IObservable<CodespacesCollection> GetAll(CancellationToken cancellationToken = default);
        IObservable<CodespacesCollection> GetForRepository(string owner, string repo, CancellationToken cancellationToken = default);
        IObservable<Codespace> Get(string codespaceName, CancellationToken cancellationToken = default);
        IObservable<Codespace> Start(string codespaceName, CancellationToken cancellationToken = default);
        IObservable<Codespace> Stop(string codespaceName, CancellationToken cancellationToken = default);
        IObservable<MachinesCollection> GetAvailableMachinesForRepo(string repoOwner, string repoName, string reference = null, CancellationToken cancellationToken = default);
        IObservable<Codespace> Create(string owner, string repo, NewCodespace newCodespace, CancellationToken cancellationToken = default);
    }
}
