using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Octokit
{
    public interface ICodespacesClient
    {
        Task<CodespacesCollection> GetAll(CancellationToken cancellationToken = default);
        Task<CodespacesCollection> GetForRepository(string owner, string repo, CancellationToken cancellationToken = default);
        Task<Codespace> Get(string codespaceName, CancellationToken cancellationToken = default);
        Task<Codespace> Start(string codespaceName, CancellationToken cancellationToken = default);
        Task<Codespace> Stop(string codespaceName, CancellationToken cancellationToken = default);
        Task<MachinesCollection> GetAvailableMachinesForRepo(string repoOwner, string repoName, string reference = null, CancellationToken cancellationToken = default);
        Task<Codespace> Create(string owner, string repo, NewCodespace newCodespace, CancellationToken cancellationToken = default);
    }
}
