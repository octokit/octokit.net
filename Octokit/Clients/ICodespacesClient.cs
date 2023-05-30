using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit.Clients
{
    public interface ICodespacesClient
    {
        Task<CodespacesCollection> GetAll();
        Task<CodespacesCollection> GetForRepository(string owner, string repo);
        Task<Codespace> Get(string codespaceName);
        Task<Codespace> Start(string codespaceName);
        Task<Codespace> Stop(string codespaceName);
    }
}