using System.Threading.Tasks;

namespace Octokit
{
    public interface IReferencesClient
    {
        Task<Reference> Get(string owner, string name, string reference);
    }
}