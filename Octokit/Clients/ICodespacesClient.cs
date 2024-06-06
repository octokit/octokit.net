using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface ICodespacesClient
    {
        Task<CodespacesCollection> GetAll();
        Task<CodespacesCollection> GetForRepository(string owner, string repo);
        Task<Codespace> Get(string codespaceName);
        Task<Codespace> Start(string codespaceName);
        Task<Codespace> Stop(string codespaceName);
        Task<MachinesCollection> GetAvailableMachinesForRepo(string repoOwner, string repoName, string reference = null);
        Task<Codespace> Create(string owner, string repo, NewCodespace newCodespace);
    }
}