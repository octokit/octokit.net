using System;
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
        IObservable<CodespacesCollection> GetAll();
        IObservable<CodespacesCollection> GetForRepository(string owner, string repo);
        IObservable<Codespace> Get(string codespaceName);
        IObservable<Codespace> Start(string codespaceName);
        IObservable<Codespace> Stop(string codespaceName);
        IObservable<MachinesCollection> GetAvailableMachinesForRepo(string repoOwner, string repoName, string reference = null);
        IObservable<Codespace> Create(string owner, string repo, NewCodespace newCodespace);
    }
}