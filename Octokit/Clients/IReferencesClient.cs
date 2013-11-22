using System.Threading.Tasks;

namespace Octokit
{
    public interface IReferencesClient
    {
        Task<GitReference> Get(string owner, string repo, string reference);
    }
}